using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.COrderServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.UtilityClasses;

namespace EcommerceBackend.Core.Application.Services.ClientServices.COrderServices
{
    public class OrderManagementService
        (

        IOrderManagementValidationService _Validate,
        IOrderRepository _Repo,
        ICartRepository _CartRepo

        ): IOrderManagementService
    {

        public async Task<ApiResponseDto<object?>> CreateOrderAsync(AddOrderDto Items,int ClientId)
        {
            var Errors = await _Validate.ValidateAdd(Items);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            bool success = await _Repo.CreateAsync(Items,ClientId);

            foreach (var Item in Items.CartItemsIds!)
            {
               
                await _CartRepo.DeleteAsync(Item, ClientId);
            }

            if (!success)
                return UApiResponder<object>.Fail("Internal server error.", Errors, 500);

            return UApiResponder<object>.Success(null, "Order was created successfully.");
        }

      
        public async Task<ApiResponseDto<object?>> GetPaginatedOrderAsync(PaginationFormDto form, int ClientId)
        {
            var Errors = await _Validate.ValidateGet(form,ClientId);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            var data = await _Repo.GetPaginatedClientOrdersById(form,ClientId);

            return UApiResponder<object>.Success(data, "Orders were fetched successfully.");
        }


    }
}
