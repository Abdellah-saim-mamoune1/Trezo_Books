using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EOrdersServicesInterfaces
{
    public interface IEOrdersManagementValidationService
    {
        public List<DValidationErorrs>? ValidateGet(DPaginationForm Pagination);
        public Task<List<DValidationErorrs>?> ValidateSetStatus(int OrderId, string status);
    }
}
