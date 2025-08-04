using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CWishlistServicesInterfaces
{
    public interface IWishlistManagementValidationService
    {
        public Task<List<ValidationErorrsDto>?> ValidateAdd(int BookCopyId);
        public Task<List<ValidationErorrsDto>?> ValidateDelete(int WishListItemId, int ClientId);
    }
}
