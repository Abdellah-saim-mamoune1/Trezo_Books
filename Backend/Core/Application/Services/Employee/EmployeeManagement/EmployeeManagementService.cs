using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EEmployeeManagementServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.EmployeeDTO_s;
using EcommerceBackend.DTO_s.EmployeeXClientDTO_s;
using EcommerceBackend.Infrastructure.Data;
using EcommerceBackend.UtilityClasses;
using Microsoft.EntityFrameworkCore;
namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EEmployeeManagementServices
{
    public class EmployeeManagementService
        (
      
       IEmployeeRepository _Repo,
       AppDbContext _db

        ) :IEmployeeManagementService
    {


        public async Task<ApiResponseDto<TokenResponseDto?>> RegisterAsync(EmployeeSignUpDto SignUpInfos)
        {
            List<ValidationErorrsDto> Errors = new();
            var validator = new EmployeeManagementValidatorsService(_db);
            var ValidationErrors = await validator.ValidateRegister(SignUpInfos);

            if (ValidationErrors != null)
            {
                return UApiResponder<TokenResponseDto?>.Fail("Invalid pieces of information", ValidationErrors, 400);
            }
            var data = await _Repo.RegisterAsync(SignUpInfos);
            if (data == null)
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Server", Message = "Internal server error" });
                return UApiResponder<TokenResponseDto?>.Fail("Internal server error", Errors, 500);
            }
            return UApiResponder<TokenResponseDto>.Success(data, "Employee was registered successfully");
        }

        public async Task<ApiResponseDto<object?>> DeleteAsync(int Id)
        {
            List<ValidationErorrsDto> Errors = new();
           
            if (!await ValidateEmployeeExistenceById(Id) )
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Id", Message = "Employee not found." });
                return UApiResponder<object>.Fail("Invalid pieces of information", Errors, 400);
            }
            var data = await _Repo.DeleteAsync(Id);
            if (!data)
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Server", Message = "Internal Server Error" });
                return UApiResponder<object>.Fail("Internal server error", Errors, 500);
            }
            return UApiResponder<object>.Success(null, "Employee was deleted successfully");
        }

        public async Task<ApiResponseDto<object?>> UpdateAsync(PersonDto form,int Id)
        {
            List<ValidationErorrsDto> Errors = new();

            var validator = new EmployeeManagementValidatorsService(_db);
            var ValidationErrors = await validator.ValidateUpdate(form);

            if (ValidationErrors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", ValidationErrors, 400);
            }


            var data = await _Repo.UpdateAsync(form, Id);
            if (!data)
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Server.", Message = "Internal Server Error." });
                return UApiResponder<object>.Fail("Internal server error.", Errors, 500);
            }
            return UApiResponder<object>.Success(null, "Employee was updated successfully.");
        }

        public async Task<ApiResponseDto<object?>> ResetPasswordAsync(ResetPasswordDto form, int Id)
        {
            List<ValidationErorrsDto> Errors = new();

            var validator = new EmployeeManagementValidatorsService(_db);
            var ValidationErrors = await validator.ValidateResetPassword(form,Id);

            if (ValidationErrors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", ValidationErrors, 400);
            }


            var data = await _Repo.ResetPasswordAsync(form, Id);
            if (!data)
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Server.", Message = "Internal Server Error." });
                return UApiResponder<object>.Fail("Internal server error.", Errors, 500);
            }
            return UApiResponder<object>.Success(null, "Password was updated successfully.");
        }

        public async Task<ApiResponseDto<object?>> GetAllAsync()
        {
            var data = await _Repo.GetAllAsync();
            return UApiResponder<object>.Success(data, "Employees were fetched successfully");
        }

        public async Task<ApiResponseDto<object?>> GetByIdAsync(int Id)
        {
            var data = await _Repo.GetByIdAsync(Id);
            return UApiResponder<object>.Success(data, "Employee was fetched successfully");
        }


        private async Task<bool> ValidateEmployeeExistenceById(int Id)
        {
            return await _db.Employees.AnyAsync(e => e.PersonId == Id);
        }

    }
}
