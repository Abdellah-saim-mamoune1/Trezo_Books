using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.ContactUsManagementServicesInterfaces
{
    public interface IEContactUsMessagesManagementService
    {
       
        public Task<DApiResponse<object?>> DeleteAsync(int MessageId);
        public Task<DApiResponse<object?>> GetAsync();
        public Task<DApiResponse<object?>> CreateAsync(DEContactUsSet form);
    }
}
