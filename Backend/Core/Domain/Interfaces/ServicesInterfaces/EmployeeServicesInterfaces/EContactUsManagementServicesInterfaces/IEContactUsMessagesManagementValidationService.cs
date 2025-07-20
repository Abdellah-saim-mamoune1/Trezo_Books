using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.ContactUsManagementServicesInterfaces
{
    public interface IEContactUsMessagesManagementValidationService
    {
        public List<DValidationErorrs>? ValidateSet(DEContactUsSet form);
        public Task<DValidationErorrs?> ValidateDelete(int MessageId);
    }
}
