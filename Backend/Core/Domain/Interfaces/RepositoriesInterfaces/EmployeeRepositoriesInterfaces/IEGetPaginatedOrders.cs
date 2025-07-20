using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IEGetPaginatedOrders
    {
        public Task<DGetPaginatedOrder> GetPaginatedClientOrdersById(DPaginationForm form);
        public Task<bool> SetOrderStatus(int Id, string status);
    }
}
