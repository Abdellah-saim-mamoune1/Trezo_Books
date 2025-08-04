using EcommerceBackend.Core.Domain.Models.ClientModels;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.CartDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CCartServicesInterfaces
{
    public interface ICartManagementService
    {
        public Task<ApiResponseDto<object?>> AddToCartAsync(int BookId, int ClientId);
        public Task<ApiResponseDto<object?>> UpdateAsync(UpdateCartItemDto Item, int clientId);
        public Task<ApiResponseDto<object?>> DeleteAsync(int ItemId, int clientId);
        public Task<ApiResponseDto<object?>> GetAsync(int ClientId);
    }
}
