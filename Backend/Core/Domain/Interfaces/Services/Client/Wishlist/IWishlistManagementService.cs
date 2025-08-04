using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CWishlistServicesInterfaces
{
    public interface IWishlistManagementService
    {
        public Task<ApiResponseDto<object?>> AddToListAsync(int BookCopyId, int ClientId);
        public Task<ApiResponseDto<object?>> DeleteListItemAsync(int ItemId, int ClientId);
        public Task<ApiResponseDto<object?>> GetClientWishlistItemAsync(int ClientId);
    }
}
