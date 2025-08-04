using EcommerceBackend.DTO_s.CartDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface ICartRepository
    {
        public  Task<int> CreateAsync(int Id, int ClientId);
        public  Task<bool> DeleteAsync(int ItemId, int clientId);
        public  Task<bool> UpdateAsync(UpdateCartItemDto Item, int clientId);
        public  Task<List<GetCartItemDto>> GetClientCartItemsAsync(int Id);
    }
}
