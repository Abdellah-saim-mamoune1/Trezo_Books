using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CCartServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.CartDTO_s;
using EcommerceBackend.UtilityClasses;

namespace EcommerceBackend.Core.Application.Services.ClientServices.CCartServices
{
    public class CCartManagementService
        (

       ICCartManagementValidationService _Validate,
       ICCartRepository _Repo
      

        ): ICCartManagementService
    {


        public async Task<DApiResponse<object?>> AddToCartAsync(int BookId,int ClientId)
        {
            var Errors = await _Validate.ValidateAdd(BookId, ClientId);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            var ItemId = await _Repo.CreateAsync(BookId,ClientId);
            if(ItemId==-1)
                return UApiResponder<object>.Fail("Internal server error.", Errors, 500);

            return UApiResponder<object>.Success(ItemId, "Item was added to cart successfully.");
        }


        public async Task<DApiResponse<object?>> UpdateAsync(DCUpdateCartItem Item, int clientId)
        {
            var Errors = await _Validate.ValidateUpdate(Item,clientId);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            var success = await _Repo.UpdateAsync(Item, clientId);
            if (!success)
                return UApiResponder<object>.Fail("Internal server error.", Errors, 500);

            return UApiResponder<object>.Success(null, "Item was updated successfully.");
        }

        public async Task<DApiResponse<object?>> DeleteAsync(int ItemId, int clientId)
        {
            var Errors = await _Validate.ValidateDelete(ItemId, clientId);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            var success = await _Repo.DeleteAsync(ItemId, clientId);
            if (!success)
                return UApiResponder<object>.Fail("Internal server error.", Errors, 500);

            return UApiResponder<object>.Success(null, "Item was deleted successfully.");
        }


        public async Task<DApiResponse<object?>> GetAsync(int ClientId)
        {
            var Errors = await _Validate.ValidateGet(ClientId);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            var Data = await _Repo.GetClientCartItemsAsync(ClientId);
          
            return UApiResponder<object>.Success(Data, "Items were fetched successfully.");
        }



    }
}
