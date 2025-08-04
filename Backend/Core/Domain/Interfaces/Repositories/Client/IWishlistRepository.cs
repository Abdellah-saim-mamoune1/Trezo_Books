using EcommerceBackend.Core.Domain.Models.ClientModels;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface IWishlistRepository
    {
        public Task<List<Wishlist>?> GetClientWhishListById(int ClientId);
        public Task<Wishlist?> GetClientWhishListItemById(int ClientId, int ItemId);
        public Task<int> CreateAsync(int BookCopyId, int ClientId);
        public Task<bool> DeleteAsync(int WishlistItemId, int ClientId);
    }
}
