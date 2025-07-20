using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.AuthenticationRepositories;
using EcommerceBackend.Core.Domain.Models.ClientModels;
using EcommerceBackend.Core.Domain.Models.EmployeeModels;
using EcommerceBackend.DTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.AuthenticationRepositories
{
    public class LoginRepository(AppDbContext _db, ITokensRepository _Tokens): ILoginRepository
    {

        public async Task<DTokenResponse?> LoginAsync(DLogin request)
        {
          

            if (request.Account.Contains("@Trezo.com"))
            {
                var tokens = await HandleEmployeeLogin(request);
                return tokens;
            }

            else
            {
                var tokens = await HandleClientLogin(request);
               
                return tokens;
            }


        }


        private async Task<DTokenResponse?> HandleEmployeeLogin(DLogin request)
        {
            var Employee = await _db.Employees.Include(e => e.EmployeeAccount).ThenInclude(e => e!.token)
                .Include(e => e.EmployeeAccount!.EmployeeAccountType)
                .AsQueryable().FirstOrDefaultAsync(u => u.EmployeeAccount!.AccountAddress == request.Account);

            var GTI = new DGenerateTokensInfos
            {
                Id = Employee!.PersonId.ToString(),
                Role = Employee.EmployeeAccount!.EmployeeAccountType!.TypeName,
                TokenId = Employee.EmployeeAccount.token!.Id
            };
            

            return await _Tokens.GenerateAndSaveTokens(GTI);

        }



        private async Task<DTokenResponse?> HandleClientLogin(DLogin request)
        {
            var Client = await _db.Clients.Include(c => c.Account).ThenInclude(c => c!.Token)
                .FirstAsync(u => u.Account!.Account == request.Account);


            var GTI = new DGenerateTokensInfos
            {
                Id = Client!.PersonId.ToString(),
                Role = "Client",
                TokenId = Client.Account!.Token!.Id
            };

            return await _Tokens.GenerateAndSaveTokens(GTI);


        }




    }
}
