using EcommerceBackend.DTO_s.CartDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface ICCartRepository
    {
        public  Task<int> CreateAsync(int Id, int ClientId);
        public  Task<bool> DeleteAsync(int ItemId, int clientId);
        public  Task<bool> UpdateAsync(DCUpdateCartItem Item, int clientId);
        public  Task<List<DCGetCartItem>> GetClientCartItemsAsync(int Id);
    }
}
