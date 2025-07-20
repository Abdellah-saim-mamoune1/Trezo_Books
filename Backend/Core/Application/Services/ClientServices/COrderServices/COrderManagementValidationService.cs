using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.COrderServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Core.Application.Services.ClientServices.COrderServices
{
    public class COrderManagementValidationService(
        AppDbContext _db
        ): ICOrderManagementValidationService
    {


        public async Task<List<DValidationErorrs>?> ValidateAdd(DCAddOrder Item)
        {
            List<DValidationErorrs> errors = new();
           

                if (Item.ShipmentAddress.Length < 5 || Item.ShipmentAddress.Length > 50)
                    errors.Add(new DValidationErorrs { FieldId = "ShipmentAddress.", Message = "ShipmentAddress length must be between 5 and 50." });

                if(Item.CartItemsIds==null)
                    errors.Add(new DValidationErorrs { FieldId = "CartItemsIds.", Message = "Cart is empty." });

                if(Item.CartItemsIds!=null)
            foreach (var CartItemId in Item.CartItemsIds)
            {
                if (!await _db.Carts.AnyAsync(c => c.Id == CartItemId))
                    errors.Add(new DValidationErorrs { FieldId = "CartItemId.", Message = "Cart item not found." });
            }


            return errors.Count != 0 ? errors : null;
        }

        public async Task<List<DValidationErorrs>?> ValidateGet(DPaginationForm Pagination, int ClientId)
        {
            List<DValidationErorrs> errors = new();

            if(Pagination.pageNumber<1)
                errors.Add(new DValidationErorrs { FieldId = "pageNumber.", Message = "pageNumber must be a positive number." });

            if(Pagination.pageSize<1)
                errors.Add(new DValidationErorrs { FieldId = "pageSize.", Message = "pageSize must be a positive number." });

            if (!await ValidateClientExistenceById(ClientId))
                errors.Add(new DValidationErorrs { FieldId = "ClientId.", Message = "Client not found." });

            return errors.Count != 0 ? errors : null;
        }


        private async Task<bool> ValidateClientExistenceById(int Id)
        {
            return await _db.Clients.AnyAsync(c => c.PersonId == Id);
        }






    }
}
