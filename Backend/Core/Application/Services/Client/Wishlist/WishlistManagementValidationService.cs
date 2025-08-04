using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CWishlistServicesInterfaces;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Core.Application.Services.ClientServices.CWishlistServices
{
    public class WishlistManagementValidationService
        (
        
        IWishlistRepository _GetWishlist
        ,AppDbContext _db
        
        ): IWishlistManagementValidationService
    {

           public async Task<List<ValidationErorrsDto>?> ValidateAdd(int BookCopyId)
           {
            List<ValidationErorrsDto> errors = new();

            if (!await ValidateBookCopyExistenceById(BookCopyId))
                errors.Add(new ValidationErorrsDto { FieldId = "BookCopyId.", Message = "Book copy not found." });

            return errors.Count != 0 ? errors : null;
           }

        public async Task<List<ValidationErorrsDto>?> ValidateDelete( int WishListItemId,int ClientId)
        {
            List<ValidationErorrsDto> errors = new();

            if (!await ValidateWishListItemExistenceById(ClientId, WishListItemId))
                errors.Add(new ValidationErorrsDto { FieldId = "WishListItemId.", Message = "Wishlist item not found." });

            return errors.Count != 0 ? errors : null;
        }



        private async Task<bool> ValidateBookCopyExistenceById(int Id)
        {
            return await _db.BooksCopies.AnyAsync(b => b.Id == Id);
        }

        private async Task<bool> ValidateWishListItemExistenceById(int ClientId,int ItemId)
        {
            return await _GetWishlist.GetClientWhishListItemById(ClientId,ItemId)!=null;
        }

    }
}
