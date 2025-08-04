using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.StatisticsInterfaces
{
    public interface IStatisticsService
    {
        public Task<ApiResponseDto<object?>> GetTotalsAsync();
        public Task<ApiResponseDto<object?>> GetResentOrdersAsync();
        public Task<ApiResponseDto<object?>> GetNewClientsAsync();
        public Task<ApiResponseDto<object?>> GetAllClientsAsync();


    }
}
