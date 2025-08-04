using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.ContactUsManagementServicesInterfaces
{
    public interface IContactUsMessagesManagementValidationService
    {
        public List<ValidationErorrsDto>? ValidateSet(ContactUsSetDto form);
        public Task<ValidationErorrsDto?> ValidateDelete(int MessageId);
    }
}
