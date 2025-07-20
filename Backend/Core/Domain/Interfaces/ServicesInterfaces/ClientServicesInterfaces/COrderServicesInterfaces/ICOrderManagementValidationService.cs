using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.COrderServicesInterfaces
{
    public interface ICOrderManagementValidationService
    {
        public Task<List<DValidationErorrs>?> ValidateAdd(DCAddOrder Item);
        public Task<List<DValidationErorrs>?> ValidateGet(DPaginationForm Pagination, int ClientId);
    }
}
