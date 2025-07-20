using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CWishlistServicesInterfaces
{
    public interface ICWishlistManagementValidationService
    {
        public Task<List<DValidationErorrs>?> ValidateAdd(int BookCopyId);
        public Task<List<DValidationErorrs>?> ValidateDelete(int WishListItemId, int ClientId);
    }
}
