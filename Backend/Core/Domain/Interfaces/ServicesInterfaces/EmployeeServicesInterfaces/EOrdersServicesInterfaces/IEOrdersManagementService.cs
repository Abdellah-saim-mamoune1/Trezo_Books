using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EOrdersServicesInterfaces
{
    public interface IEOrdersManagementService
    {
        public Task<DApiResponse<object?>> GetPaginatedOrderAsync(DPaginationForm form);
        public Task<DApiResponse<object?>> SetOrderStatusAsync(int OrderId, string status);
    }
}
