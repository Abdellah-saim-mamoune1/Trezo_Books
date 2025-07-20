using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.COrderServicesInterfaces
{
    public interface ICOrderManagementService
    {
        public Task<DApiResponse<object?>> CreateOrderAsync(DCAddOrder Item, int ClientId);
        public Task<DApiResponse<object?>> GetPaginatedOrderAsync(DPaginationForm form, int ClientId);
    }
}
