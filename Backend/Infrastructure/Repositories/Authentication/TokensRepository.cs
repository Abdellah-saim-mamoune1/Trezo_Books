using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.AuthenticationRepositories;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.Infrastructure.Data;
using EcommerceBackend.UtilityClasses;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.AuthenticationRepositories
{
    public class TokensRepository(AppDbContext _db): ITokensRepository
    {

        public async Task<TokenResponseDto?> GenerateAndSaveTokens(GenerateTokensInfosDto GTI)
        {
            TokenResponseDto? Tokens = null;
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    Tokens = UMethods.CreateTokenResponse(GTI.Id, GTI.Role);
                    await UpdateRefreshToken(Tokens.RefreshToken, GTI.TokenId);
                    await transaction.CommitAsync();
                   
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to refresh token: {ex}");
               
            }
            return Tokens;

        }


        private async Task UpdateRefreshToken(string RefreshToken, int TokenId)
        {

            var T = await _db.Tokens.FirstAsync(t => t.Id == TokenId);
        
            T.RefreshToken = RefreshToken;
            T.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30);
            await _db.SaveChangesAsync();

           
        }


        public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
        {

         
            var GTI = await GetRefreshTokenInfoAsync(request);
          
            var tokens = UMethods.CreateTokenResponse(GTI!.Id, GTI.Role);

            await UpdateRefreshToken(tokens.RefreshToken, GTI.TokenId);


            return tokens;
        }




        private async Task<GenerateTokensInfosDto?> GetRefreshTokenInfoAsync(RefreshTokenRequestDto request)
        {

            if (request.Role != "Client")
            {
                var employee = await _db.EmployeeAccount.Include(t => t.token).Include(e => e.EmployeeAccountType)
                    .Include(e => e!.Employee).FirstAsync(t => t.token!.RefreshToken == request.RefreshToken);

                var GTI = new GenerateTokensInfosDto
                {
                    Id = employee!.Employee!.PersonId.ToString(),
                    Role = employee.EmployeeAccountType!.TypeName,
                    TokenId = employee!.token!.Id
                };
                return GTI;
            }
            var CRToken = await _db.ClientsAccounts.Include(t => t.Client).Include(a => a.Token).FirstAsync(t => t.Token!.RefreshToken == request.RefreshToken);

            var ClientGTI = new GenerateTokensInfosDto
            {
                Id = CRToken!.Client!.PersonId.ToString(),
                Role = "Client",
                TokenId = CRToken!.Token!.Id
            };
            return ClientGTI;



        }
    }
}
