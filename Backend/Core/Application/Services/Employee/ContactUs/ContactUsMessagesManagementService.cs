using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.ContactUsManagementServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.UtilityClasses;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EContactUsServices
{
    public class ContactUsMessagesManagementService
        (
        Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces.IContactUsRepository _Repo,
        Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces.IContactUsRepository _SetMessage,
        IContactUsMessagesManagementValidationService _Validate
        ): IContactUsMessagesManagementService
    {
        public async Task<ApiResponseDto<object?>> CreateAsync(ContactUsSetDto form)
        {
            List<ValidationErorrsDto> Errors = new();
            var errors = _Validate.ValidateSet(form);
            if (errors != null)
            {
                return UApiResponder<object>.Fail("Invalid Pieces of Information.", errors, 400);
            }
            var data = await _SetMessage.Create(form);
            if (!data)
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Server.", Message = "Internal Server Error." });
                return UApiResponder<object>.Fail("Internal server error.", Errors, 500);
            }
            return UApiResponder<object>.Success(null, "Message was created successfully.");
        }

        public async Task<ApiResponseDto<object?>> DeleteAsync(int MessageId)
        {
            List<ValidationErorrsDto> Errors = new();
            var errors = await _Validate.ValidateDelete(MessageId);
            if (errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", new List<ValidationErorrsDto> { errors }, 400);
            }
            var data = await _Repo.Delete(MessageId);
            if (!data)
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Server.", Message = "Internal Server Error." });
                return UApiResponder<object>.Fail("Internal server error.", Errors, 500);
            }
            return UApiResponder<object>.Success(null, "Message was deleted successfully.");
        }

        public async Task<ApiResponseDto<object?>> GetAsync()
        {
            return UApiResponder<object>.Success(await _Repo.Get(), "Messages were successfully.");
        }
    }
}