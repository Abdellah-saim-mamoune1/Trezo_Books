using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.COrderServicesInterfaces
{
    public interface IOrderManagementService
    {
        public Task<ApiResponseDto<object?>> CreateOrderAsync(AddOrderDto Item, int ClientId);
        public Task<ApiResponseDto<object?>> GetPaginatedOrderAsync(PaginationFormDto form, int ClientId);
    }
}
