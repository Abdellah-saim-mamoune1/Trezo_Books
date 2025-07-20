using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s.StatisticsDTO_s;
using EcommerceBackend.DTO_s.StatisticsDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IEStatisticsRepository
    {
        public Task<DEGetTotals> GetTotalsAsync();
        public Task<List<DEGetResentOrders>> GeResentOrdersAsync();
        public Task<List<DEGetClients>> GeNewClientsAsync();
        public Task<List<DEGetClients>> GeAllClientsAsync();


    }
}
