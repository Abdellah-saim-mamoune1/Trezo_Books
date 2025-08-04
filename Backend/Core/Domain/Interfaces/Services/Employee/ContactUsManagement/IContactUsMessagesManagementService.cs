using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.ContactUsManagementServicesInterfaces
{
    public interface IContactUsMessagesManagementService
    {
       
        public Task<ApiResponseDto<object?>> DeleteAsync(int MessageId);
        public Task<ApiResponseDto<object?>> GetAsync();
        public Task<ApiResponseDto<object?>> CreateAsync(ContactUsSetDto form);
    }
}
