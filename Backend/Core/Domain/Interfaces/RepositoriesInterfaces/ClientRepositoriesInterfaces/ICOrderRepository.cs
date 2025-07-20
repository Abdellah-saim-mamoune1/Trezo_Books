using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface ICOrderRepository
    {
        public Task<bool> CreateAsync(DCAddOrder Item, int ClientId);
        public Task<DGetPaginatedOrder> GetPaginatedClientOrdersById(DPaginationForm form, int ClientId);
    }
}
