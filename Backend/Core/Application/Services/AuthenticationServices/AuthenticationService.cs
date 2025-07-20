using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.AuthenticationRepositories;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces;
using EcommerceBackend.DTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.UtilityClasses;

namespace EcommerceBackend.Core.Application.Services.AuthenticationServices
{
    public class AuthenticationService(
        IAuthenticationValidationService _Validate,
        ILoginRepository _LoginRepo,
        ITokensRepository _TokensRepo
        ) : IAuthService
    {


        public async Task<DApiResponse<DTokenResponse?>> Login(DLogin form)
        {
            var Errors = await _Validate.ValidateLogin(form);

            if (Errors != null)
            {
                return UApiResponder<DTokenResponse>.Fail("Invalid pieces of information.", Errors, 400);
            }

            var data = await _LoginRepo.LoginAsync(form);
            if (data==null)
                return UApiResponder<DTokenResponse>.Fail("Internal server error.", Errors, 500);

            return UApiResponder<DTokenResponse>.Success(data, "Login was successful.");
        }


        public async Task<DApiResponse<DTokenResponse?>> RefreshTokens(DRefreshTokenRequest form)
        {
            var Errors = await _Validate.ValidateRefreshToken(form);

            if (Errors != null)
            {
                return UApiResponder<DTokenResponse>.Fail("Invalid pieces of information.", Errors, 400);
            }

            var data = await _TokensRepo.RefreshTokensAsync(form);
            if (data == null)
                return UApiResponder<DTokenResponse>.Fail("Internal server error.", Errors, 500);

            return UApiResponder<DTokenResponse>.Success(data, "Tokens were refreshed successfully.");
        }


        /* public async Task<DTokenResponse?> LoginAsync(DLogin request)
         {


             if (request.Account.Contains("@Trezo.com"))
             {
                  var tokens= await HandleEmployeeLogin(request);
                 return tokens;
             }

             else
             {
                   var tokens= await HandleClientLogin(request);
                 return tokens;
             }


         }


         public async Task<DTokenResponse?> HandleEmployeeLogin(DLogin request)
         {
             var Emp = await context.Employees.Include(e=>e.EmployeeAccount).ThenInclude(e => e.token).Include(e=>e.EmployeeAccount!.EmployeeAccountType)
                 .FirstOrDefaultAsync(u => u.EmployeeAccount!.AccountAddress == request.Account);

             if (Emp is null ||
                 new PasswordHasher<Employee>()
                 .VerifyHashedPassword(Emp, Emp.EmployeeAccount!.Password, request.Password)
                 == PasswordVerificationResult.Failed)
             {
                 return null;
             }


             var GTI = new DGenerateTokensInfos
             {
                 Id = Emp.PersonId.ToString(),
                 Role = Emp.EmployeeAccount!.EmployeeAccountType!.TypeName,
                 TokenId = Emp.EmployeeAccount.token!.Id
             };

             return await GenerateAndSaveTokens(GTI);

         }



         public async Task<DTokenResponse?> HandleClientLogin(DLogin request)
         {
             var Cli = await context.Clients.Include(c=>c.Account).ThenInclude(c => c!.Token)
                 .FirstOrDefaultAsync(u => u.Account!.Account == request.Account);

             if (Cli is null ||
                 new PasswordHasher<Client>()
                 .VerifyHashedPassword(Cli,Cli.Account!.Password, request.Password)
                 == PasswordVerificationResult.Failed)
             {
                 Console.WriteLine("wrong password");
                 return null;
             }


             var GTI = new DGenerateTokensInfos
             {
                 Id = Cli.PersonId.ToString(),
                 Role = "Client",
                 TokenId = Cli.Account.Token!.Id
             };

           return await GenerateAndSaveTokens(GTI);


         }



         public async Task <DTokenResponse?> GenerateAndSaveTokens(DGenerateTokensInfos GTI)
         {
             var Tokens = UMethods.CreateTokenResponse(GTI.Id, GTI.Role);
             bool check = await UpdateRefreshToken(Tokens.RefreshToken,GTI.TokenId);

             if (check)
                 return Tokens;

             return null;

         }


         public async Task<bool> UpdateRefreshToken(string RefreshToken,int TokenId)
         {

                 var T = await context.Tokens.FirstOrDefaultAsync(t => t.Id == TokenId);
                 if (T is null) 
                    return false;
                 T.RefreshToken= RefreshToken;
                 T.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30);
                 await context.SaveChangesAsync();

                   return true;
         }


         public async Task<DTokenResponse?> RefreshTokensAsync(DRefreshTokenRequest request)
         {

             if (request == null || string.IsNullOrEmpty(request.RefreshToken))
             {
                 return null;
             }

             var GTI = await ValidateRefreshTokenAsync(request);
             if (GTI is null)
                 return null;

           var tokens= UMethods.CreateTokenResponse(GTI.Id,GTI.Role);

             await UpdateRefreshToken(tokens.RefreshToken, GTI.TokenId);


             return tokens;
         }




         private async Task<DGenerateTokensInfos?> ValidateRefreshTokenAsync(DRefreshTokenRequest request)
         {

             if (request.Role!="Client")
             {
                 var employee= await context.EmployeeAccount.Include(t=>t.token).Include(e=>e.EmployeeAccountType).Include(e=>e!.Employee).FirstOrDefaultAsync(t => t.token!.RefreshToken == request.RefreshToken);

                 if (employee == null || employee!.token is null|| employee!.token!.RefreshToken != request.RefreshToken || employee.token.RefreshTokenExpiryTime <= DateTime.UtcNow)
                     return null;

                 var GTI = new DGenerateTokensInfos
                 {
                     Id = employee.Employee!.PersonId.ToString(),
                     Role =employee.EmployeeAccountType!.TypeName,
                     TokenId = employee.token.Id
                 };
                 return GTI;
             }


             var CRToken = await context.ClientsAccounts.Include(t => t.Client).Include(a=>a.Token).FirstOrDefaultAsync(t => t.Token!.RefreshToken == request.RefreshToken);

             if (CRToken is null || CRToken.Client is null || CRToken.Token is null|| CRToken!.Token!.RefreshToken != request.RefreshToken || CRToken.Token.RefreshTokenExpiryTime <= DateTime.UtcNow)
                 return null;

             var ClientGTI = new DGenerateTokensInfos
             {
                 Id = CRToken.Client.PersonId.ToString(),
                 Role = "Client",
                 TokenId = CRToken.Token.Id
             };
             return ClientGTI;



         }



         */

    }
}
