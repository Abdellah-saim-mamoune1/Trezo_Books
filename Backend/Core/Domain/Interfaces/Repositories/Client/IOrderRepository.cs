using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface IOrderRepository
    {
        public Task<bool> CreateAsync(AddOrderDto Item, int ClientId);
        public Task<GetPaginatedOrderDto> GetPaginatedClientOrdersById(PaginationFormDto form, int ClientId);
    }
}
