using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CWishlistServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.UtilityClasses;

namespace EcommerceBackend.Core.Application.Services.ClientServices.CWishlistServices
{
    public class CWishlistManagementService
        (
       
        ICWishlistManagementValidationService _Validate,
        ICWishlistRepository _Repo

        ): ICWishlistManagementService
    {



        public async Task<DApiResponse<object?>> AddToListAsync(int BookCopyId, int ClientId)
        {
           
            var Errors = await _Validate.ValidateAdd(BookCopyId);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            int ItemId = await _Repo.CreateAsync(BookCopyId, ClientId);
            if (ItemId == -1)
                return UApiResponder<object>.Fail("Internal server error.", Errors, 500);

            return UApiResponder<object>.Success(ItemId, "Item was added to Wishlist successfully.");
        }


        public async Task<DApiResponse<object?>> DeleteListItemAsync(int ItemId, int ClientId)
        {
            var Errors = await _Validate.ValidateDelete(ItemId, ClientId);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            bool result = await _Repo.DeleteAsync(ItemId, ClientId);
            
            if (!result)
                return UApiResponder<object>.Fail("Internal server error.", Errors, 500);

            return UApiResponder<object>.Success(ItemId, "Item was deleted from Wishlist successfully.");
        }


        public async Task<DApiResponse<object?>> GetClientWishlistItemAsync( int ClientId)
        {
            var result = await _Repo.GetClientWhishListById(ClientId);
            return UApiResponder<object>.Success(result, "Wishlist items was retrieved successfully.");
        }




    }
}
