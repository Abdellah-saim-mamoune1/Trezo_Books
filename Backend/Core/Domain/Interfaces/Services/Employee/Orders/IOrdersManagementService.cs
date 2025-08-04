using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EOrdersServicesInterfaces
{
    public interface IOrdersManagementService
    {
        public Task<ApiResponseDto<object?>> GetPaginatedOrderAsync(PaginationFormDto form);
        public Task<ApiResponseDto<object?>> SetOrderStatusAsync(int OrderId, string status);
    }
}
