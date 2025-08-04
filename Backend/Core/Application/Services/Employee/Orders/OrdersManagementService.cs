using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EOrdersServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.UtilityClasses;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EOrdersServices
{
    public class OrdersManagementService
        (
        IOrdersManagementValidationService _Validate,
        IGetPaginatedOrders _Get
        ): IOrdersManagementService
    {



        public async Task<ApiResponseDto<object?>> GetPaginatedOrderAsync(PaginationFormDto form)
        {
            var Errors = _Validate.ValidateGet(form);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            var data = await _Get.GetPaginatedClientOrdersById(form);

            return UApiResponder<object>.Success(data, "Orders were fetched successfully.");
        }

        public async Task<ApiResponseDto<object?>> SetOrderStatusAsync(int OrderId,string status)
        {
            var Errors = await _Validate.ValidateSetStatus(OrderId, status);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            var result = await _Get.SetOrderStatus(OrderId, status);
            if (result == false)
                return UApiResponder<object>.Fail("Internal server error.", Errors, 500);

            return UApiResponder<object>.Success(null, "Order status was updated successfully.");
        }

    }
}
