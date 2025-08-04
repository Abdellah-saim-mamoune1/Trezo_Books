using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.COrderServicesInterfaces
{
    public interface IOrderManagementValidationService
    {
        public Task<List<ValidationErorrsDto>?> ValidateAdd(AddOrderDto Item);
        public Task<List<ValidationErorrsDto>?> ValidateGet(PaginationFormDto Pagination, int ClientId);
    }
}
