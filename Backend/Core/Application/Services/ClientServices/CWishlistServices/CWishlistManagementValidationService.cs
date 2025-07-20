using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CWishlistServicesInterfaces;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Core.Application.Services.ClientServices.CWishlistServices
{
    public class CWishlistManagementValidationService
        (
        
        ICWishlistRepository _GetWishlist
        ,AppDbContext _db
        
        ): ICWishlistManagementValidationService
    {

           public async Task<List<DValidationErorrs>?> ValidateAdd(int BookCopyId)
           {
            List<DValidationErorrs> errors = new();

            if (!await ValidateBookCopyExistenceById(BookCopyId))
                errors.Add(new DValidationErorrs { FieldId = "BookCopyId.", Message = "Book copy not found." });

            return errors.Count != 0 ? errors : null;
           }

        public async Task<List<DValidationErorrs>?> ValidateDelete( int WishListItemId,int ClientId)
        {
            List<DValidationErorrs> errors = new();

            if (!await ValidateWishListItemExistenceById(ClientId, WishListItemId))
                errors.Add(new DValidationErorrs { FieldId = "WishListItemId.", Message = "Wishlist item not found." });

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
