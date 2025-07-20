using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.StatisticsInterfaces
{
    public interface IEStatisticsService
    {
        public Task<DApiResponse<object?>> GetTotalsAsync();
        public Task<DApiResponse<object?>> GetResentOrdersAsync();
        public Task<DApiResponse<object?>> GetNewClientsAsync();
        public Task<DApiResponse<object?>> GetAllClientsAsync();


    }
}
