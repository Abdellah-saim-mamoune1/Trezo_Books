using EcommerceBackend.Core.Domain.Models.ClientModels;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.CartDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CCartServicesInterfaces
{
    public interface ICCartManagementService
    {
        public Task<DApiResponse<object?>> AddToCartAsync(int BookId, int ClientId);
        public Task<DApiResponse<object?>> UpdateAsync(DCUpdateCartItem Item, int clientId);
        public Task<DApiResponse<object?>> DeleteAsync(int ItemId, int clientId);
        public Task<DApiResponse<object?>> GetAsync(int ClientId);
    }
}
