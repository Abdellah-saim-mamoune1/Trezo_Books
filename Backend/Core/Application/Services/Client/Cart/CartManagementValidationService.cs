using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CCartServicesInterfaces;
using EcommerceBackend.DTO_s.CartDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Core.Application.Services.ClientServices.CCartServices
{
    public class CartManagementValidationService
        (

       AppDbContext _db

        ): ICartManagementValidationService
    {


        public async Task<List<ValidationErorrsDto>?> ValidateAdd(int Id, int ClientId)
        {
            List<ValidationErorrsDto> errors = new();

            if (!await ValidateClientExistenceById(ClientId))
            {
                errors .Add(new ValidationErorrsDto { FieldId = "ClientId.", Message = "Client not found." });
            }
            if (!await ValidateBookCopyExistenceById(Id))
            {
                errors.Add(new ValidationErorrsDto { FieldId = "BookCopy.BookCopyId.", Message = "BookCopyId not found." });
            }
           
            return errors.Count != 0 ? errors : null;
        }

        public async Task<List<ValidationErorrsDto>?> ValidateUpdate(UpdateCartItemDto Item, int clientId)
        {
            List<ValidationErorrsDto> errors = new();

            if (!await ValidateCartItemExistenceById(Item.CartItemId))
            {
                errors.Add(new ValidationErorrsDto { FieldId = "CartItemId.", Message = "Cart item not found." });
            }
            if (Item.Quantity < 1)
                errors.Add(new ValidationErorrsDto { FieldId = "Quantity.", Message = "Quantity must be positive number." });
            if (!await ValidateClientExistenceById(clientId))
            {
                errors.Add(new ValidationErorrsDto { FieldId = "ClientId.", Message = "Client not found." });
            }

            return errors.Count != 0 ? errors : null;
        }

        public async Task<List<ValidationErorrsDto>?> ValidateDelete(int ItemId, int clientId)
        {
            List<ValidationErorrsDto> errors = new();

            if (!await ValidateCartItemExistenceById(ItemId))
            {
                errors.Add(new ValidationErorrsDto { FieldId = "ItemId.", Message = "Cart item not found." });
            }
            if (!await ValidateClientExistenceById(clientId))
            {
                errors.Add(new ValidationErorrsDto { FieldId = "ClientId.", Message = "Client not found." });
            }

            return errors.Count != 0 ? errors : null;
        }

        public async Task<List<ValidationErorrsDto>?> ValidateGet(int ClientId)
        {
            List<ValidationErorrsDto> errors = new();

            if (!await ValidateClientExistenceById(ClientId))
            {
                errors.Add(new ValidationErorrsDto { FieldId = "ClientId.", Message = "Client not found." });
            }

            return errors.Count != 0 ? errors : null;
        }





        private async Task<bool>ValidateClientExistenceById(int Id)
        {
            return await _db.Clients.AnyAsync(c => c.PersonId == Id);
        }

        private async Task<bool> ValidateBookCopyExistenceById(int Id)
        {
            return await _db.BooksCopies.AnyAsync(c => c.Id == Id);
        }

        private async Task<bool> ValidateCartItemExistenceById(int Id)
        {
            return await _db.Carts.AnyAsync(c => c.Id == Id);
        }

    }
}
