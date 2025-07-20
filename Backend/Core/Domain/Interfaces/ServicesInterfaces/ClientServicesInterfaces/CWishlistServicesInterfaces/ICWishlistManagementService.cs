using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CWishlistServicesInterfaces
{
    public interface ICWishlistManagementService
    {
        public Task<DApiResponse<object?>> AddToListAsync(int BookCopyId, int ClientId);
        public Task<DApiResponse<object?>> DeleteListItemAsync(int ItemId, int ClientId);
        public Task<DApiResponse<object?>> GetClientWishlistItemAsync(int ClientId);
    }
}
