using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EOrdersServicesInterfaces
{
    public interface IOrdersManagementValidationService
    {
        public List<ValidationErorrsDto>? ValidateGet(PaginationFormDto Pagination);
        public Task<List<ValidationErorrsDto>?> ValidateSetStatus(int OrderId, string status);
    }
}
