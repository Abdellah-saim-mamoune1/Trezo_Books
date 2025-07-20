using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Models.EmployeeModels;
using EcommerceBackend.DTO_s.EmployeeDTO_s;
using EcommerceBackend.DTO_s.EmployeeXClientDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EEmployeeManagementServices
{
    public class EEmployeeManagementValidatorsService(AppDbContext _db)
    {
        public async Task<List<DValidationErorrs>?> ValidateRegister(DEEmployeeSignUp signUp)
        {
            var validator = new RegisterEmployeeValidator();
            var result = await validator.ValidateAsync(signUp);
            if (!result.IsValid)
               return result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            return null;
        }

        public async Task<List<DValidationErorrs>?> ValidateUpdate(DPerson form)
        {
            var validator = new DPersonValidator();
            var result = await validator.ValidateAsync(form);
            if (!result.IsValid)
                return result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            return null;
        }

        public async Task<List<DValidationErorrs>?> ValidateResetPassword(DResetPassword form, int Id)
        {
            List<DValidationErorrs> errors = new();


            
            if (form.OldPassword == form.NewPassword)
                errors.Add(new DValidationErorrs { FieldId = "NewPassword.", Message = " New password must not be equal to old password." });
            if (form.NewPassword.Length < 7 || form.NewPassword.Length > 14)
                errors.Add(new DValidationErorrs { FieldId = "NewPassword.", Message = "Invalid new password." });
            if (!await ValidatePasswordAsync(form.OldPassword, Id))
                errors.Add(new DValidationErorrs { FieldId = "OldPassword.", Message = "Invalid old password." });

            return errors.Count != 0 ? errors : null;

        }

        private async Task<bool> ValidatePasswordAsync(string Password, int Id)
        {


            var Employee = await _db.Employees.Include(c => c.EmployeeAccount).ThenInclude(c => c!.token).FirstOrDefaultAsync(c => c.PersonId == Id);

            if (Employee == null ||
            new PasswordHasher<Employee>()
               .VerifyHashedPassword(Employee!, Employee!.EmployeeAccount!.Password, Password)
               == PasswordVerificationResult.Failed)
            {
                Console.WriteLine(Password);

                return false;

            }

            return true;

        }

    }
}
