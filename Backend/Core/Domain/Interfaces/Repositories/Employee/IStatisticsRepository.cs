using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s.StatisticsDTO_s;
using EcommerceBackend.DTO_s.StatisticsDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IStatisticsRepository
    {
        public Task<GetTotalsDto> GetTotalsAsync();
        public Task<List<GetResentOrdersDto>> GeResentOrdersAsync();
        public Task<List<GetClientsDto>> GeNewClientsAsync();
        public Task<List<GetClientsDto>> GeAllClientsAsync();


    }
}
