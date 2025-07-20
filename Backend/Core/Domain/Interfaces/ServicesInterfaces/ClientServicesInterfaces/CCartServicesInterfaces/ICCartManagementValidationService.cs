using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.CartDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CCartServicesInterfaces
{
    public interface ICCartManagementValidationService
    {
        public Task<List<DValidationErorrs>?> ValidateAdd(int Id, int ClientId);
        public Task<List<DValidationErorrs>?> ValidateUpdate(DCUpdateCartItem Item, int clientId);
        public Task<List<DValidationErorrs>?> ValidateDelete(int ItemId, int clientId);
        public Task<List<DValidationErorrs>?> ValidateGet(int ClientId);
    }
}
