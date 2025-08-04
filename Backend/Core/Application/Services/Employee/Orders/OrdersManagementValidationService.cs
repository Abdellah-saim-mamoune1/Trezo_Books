using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EOrdersServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EOrdersServices
{
    public class OrdersManagementValidationService(AppDbContext _db): IOrdersManagementValidationService
    {

        public List<ValidationErorrsDto>? ValidateGet(PaginationFormDto Pagination)
        {
            List<ValidationErorrsDto> errors = new();

            if (Pagination.pageNumber < 1)
                errors.Add(new ValidationErorrsDto { FieldId = "pageNumber.", Message = "pageNumber must be a positive number." });

            if (Pagination.pageSize < 1)
                errors.Add(new ValidationErorrsDto { FieldId = "pageSize.", Message = "pageSize must be a positive number." });

            return errors.Count != 0 ? errors : null;
        }


        public async Task<List<ValidationErorrsDto>?> ValidateSetStatus(int OrderId,string status)
        {
            List<ValidationErorrsDto> errors = new();

            if (!await _db.Orders.AnyAsync(o=>o.Id==OrderId))
                errors.Add(new ValidationErorrsDto { FieldId = "OrderId.", Message = "Order not found." });

            if (status.ToLower()!="processing"&&status.ToLower() != "shipped")
                errors.Add(new ValidationErorrsDto { FieldId = "status.", Message = "status is not valid." });

            return errors.Count != 0 ? errors : null;
        }





    }
}
