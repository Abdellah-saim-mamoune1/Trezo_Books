using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.CartDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CCartServicesInterfaces
{
    public interface ICartManagementValidationService
    {
        public Task<List<ValidationErorrsDto>?> ValidateAdd(int Id, int ClientId);
        public Task<List<ValidationErorrsDto>?> ValidateUpdate(UpdateCartItemDto Item, int clientId);
        public Task<List<ValidationErorrsDto>?> ValidateDelete(int ItemId, int clientId);
        public Task<List<ValidationErorrsDto>?> ValidateGet(int ClientId);
    }
}
