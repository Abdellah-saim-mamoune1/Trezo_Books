using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.StatisticsInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.UtilityClasses;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EStatisticsServices
{
    public class EStatisticsService(IEStatisticsRepository _Repo): IEStatisticsService
    {

        public async Task<DApiResponse<object?>> GetTotalsAsync()
        {

            var data = await _Repo.GetTotalsAsync();

            return UApiResponder<object>.Success(data, "Totals were fetched successfully.");
        }

        public async Task<DApiResponse<object?>> GetResentOrdersAsync()
        {

            var data = await _Repo.GeResentOrdersAsync();

            return UApiResponder<object>.Success(data, "Resent orders were fetched successfully.");
        }

        public async Task<DApiResponse<object?>> GetNewClientsAsync()
        {

            var data = await _Repo.GeNewClientsAsync();

            return UApiResponder<object>.Success(data, "New clients were fetched successfully.");
        }


        public async Task<DApiResponse<object?>> GetAllClientsAsync()
        {

            var data = await _Repo.GeAllClientsAsync();

            return UApiResponder<object>.Success(data, "Clients were fetched successfully.");
        }


    }
}
