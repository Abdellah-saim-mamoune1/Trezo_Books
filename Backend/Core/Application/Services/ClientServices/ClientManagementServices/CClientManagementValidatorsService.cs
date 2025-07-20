using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Application.Validators.ClientValidators;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.ClientManagementServicesInterfaces;
using EcommerceBackend.Core.Domain.Models.ClientModels;
using EcommerceBackend.DTO_s.ClientDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace EcommerceBackend.Core.Application.Services.ClientServices.ClientManagementServices
{
    public class CClientManagementValidatorsService
        (
       AppDbContext _db
        ): ICClientManagementValidatorsService
    {
        public async Task<List<DValidationErorrs>?> ValidateSignUp(DCClientSignUp Form)
        {

            var Validator = new SignUpClientValidator();
            var result = await Validator.ValidateAsync(Form);

            if (await _db.ClientsAccounts.AnyAsync(c=>c.Account==Form.Account_informations!.Account))
            {
                List<DValidationErorrs> errors = new List<DValidationErorrs> { new DValidationErorrs { FieldId = "Account", Message = "Account already exist." } };
                return errors;
            }

            else if(!result.IsValid)
                return result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();


            return null;
        }

        public async Task<List<DValidationErorrs>?> ValidateGet(int ClientId)
        {
            List<DValidationErorrs> errors = new();

            if (!await ValidateClientExistenceByIdAsync(ClientId))
                errors.Add(new DValidationErorrs { FieldId = "ClientId.", Message = "Client not found." });

            return errors.Count!=0?errors:null;

        }


        public async Task<List<DValidationErorrs>?> ValidateResetPassword(DResetPassword form, int ClientId)
        {
            List<DValidationErorrs> errors = new();
          

            if (!await ValidateClientExistenceByIdAsync(ClientId))
                errors.Add(new DValidationErorrs { FieldId = "ClientId.", Message = "Client not found." });
            if(form.OldPassword==form.NewPassword)
                errors.Add(new DValidationErorrs { FieldId = "NewPassword.", Message = " New password must not be equal to old password." });
            if (form.NewPassword.Length<7|| form.NewPassword.Length>14)
                errors.Add(new DValidationErorrs { FieldId = "NewPassword.", Message = "Invalid new password." });
            if (!await ValidatePasswordAsync(form.OldPassword, ClientId))
                errors.Add(new DValidationErorrs { FieldId = "OldPassword.", Message = "Invalid old password." });

            return errors.Count != 0 ? errors : null;

        }

        public async Task<List<DValidationErorrs>?> ValidateUpdate(DCUpdateClientProfileInfo Form,int Id)
        {
            List<DValidationErorrs> errors = new();
            var Validator = new UpdateClientProfileInfoValidator();
            var result = await Validator.ValidateAsync(Form);
            if (!result.IsValid)
                errors= result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            if(!await ValidateClientExistenceByIdAsync(Id))
                errors.Add(new DValidationErorrs { FieldId = "ClientId.", Message = "Client not found." });
            
            return errors.Count != 0 ? errors : null;

        }


        private async Task<bool> ValidateClientExistenceByIdAsync(int Id)
        {
            return await _db.Clients.AnyAsync(c => c.PersonId == Id);
        }

        private async Task<bool> ValidatePasswordAsync(string Password,int Id)
        {
            

            var Client=await _db.Clients.Include(c => c.Account).ThenInclude(c=>c!.Token).FirstOrDefaultAsync(c => c.PersonId == Id);
            
            if (Client==null||
            new PasswordHasher<Client>()
               .VerifyHashedPassword(Client!, Client!.Account!.Password,Password)
               == PasswordVerificationResult.Failed)
            {
                Console.WriteLine(Password);
                
                return false;

            }

            return true;
            
        }

    }
}
