
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Application.Validators.SharedValidators;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces;
using EcommerceBackend.Core.Domain.Models.ClientModels;
using EcommerceBackend.Core.Domain.Models.EmployeeModels;
using EcommerceBackend.DTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Core.Application.Services.AuthenticationServices
{
    public class AuthenticationValidationService(AppDbContext _db): IAuthenticationValidationService
    {


        public async Task<List<DValidationErorrs>?> ValidateLogin(DLogin request)
        {
            List<DValidationErorrs> errors = new();
            var validator = new LoginValidator();
            var validate=validator.Validate(request);
            if (!validate.IsValid)
            {
                errors = validate.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
                return errors;
            }

            else if(!await ValidateEmployeeAccountXPassword(request))
            {
                errors.Add(new DValidationErorrs { FieldId = "Account.", Message = "Account was not found." });
            }

              return errors.Count != 0 ? errors : null;
        }

        public async Task<List<DValidationErorrs>?> ValidateRefreshToken(DRefreshTokenRequest request)
        {
            List<DValidationErorrs> errors = new();
          
            if (!await ValidateRefreshTokenAsync(request))
            
                errors.Add(new DValidationErorrs { FieldId = "DRefreshTokenRequest.RefreshToken.", Message = "Refresh token not found." });
         
            if(request.Role!="Client"&& request.Role !="Admin"&& request.Role !="Seller")
                errors.Add(new DValidationErorrs { FieldId = "DRefreshTokenRequest.Role.", Message = "Invalid role." });

            return errors.Count != 0 ? errors : null;
        }
        private async Task<bool> ValidateEmployeeAccountXPassword(DLogin request)
        {
            if (request.Account.EndsWith("@Trezo.com"))
            {
                var Employee = await _db.Employees.Include(e => e.EmployeeAccount).AsQueryable()
                   .FirstOrDefaultAsync(u => u.EmployeeAccount!.AccountAddress == request.Account);

                if (Employee is null ||
                    new PasswordHasher<Employee>()
                    .VerifyHashedPassword(Employee, Employee.EmployeeAccount!.Password, request.Password)
                    == PasswordVerificationResult.Failed)
                {
                    return false;
                }

                return true;
            }

            var client=await _db.Clients.Include(c=>c.Account).AsQueryable().FirstOrDefaultAsync(u => u.Account!.Account == request.Account);

               if (client is null ||
                new PasswordHasher<Client>()
                .VerifyHashedPassword(client, client.Account!.Password, request.Password)
                == PasswordVerificationResult.Failed)
                {
                return false;
               }

            return true;
        }


        private async Task<bool> ValidateRefreshTokenAsync(DRefreshTokenRequest request)
        {

            if (request.Role != "Client")
            {
                var employee = await _db.EmployeeAccount.Include(t => t.token).Include(e => e.EmployeeAccountType)
                    .Include(e => e!.Employee).AsQueryable().FirstOrDefaultAsync(t => t.token!.RefreshToken == request.RefreshToken);

                if (employee == null || employee!.token is null || employee!.token!.RefreshToken != request.RefreshToken || employee.token.RefreshTokenExpiryTime <= DateTime.UtcNow)
                    return false;

                return true;
                 
            }


            var CRToken = await _db.ClientsAccounts.Include(t => t.Client).Include(a => a.Token).FirstOrDefaultAsync(t => t.Token!.RefreshToken == request.RefreshToken);

            if (CRToken is null || CRToken.Client is null || CRToken.Token is null || CRToken!.Token!.RefreshToken != request.RefreshToken || CRToken.Token.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return false;
          
            return true;
        }

    }
}
