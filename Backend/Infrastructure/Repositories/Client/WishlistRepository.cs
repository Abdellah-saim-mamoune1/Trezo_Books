using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Models.ClientModels;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.ClientRepositories
{
    public class WishlistRepository(AppDbContext _db): IWishlistRepository
    {

        public async Task<List<Wishlist>?>GetClientWhishListById(int ClientId)
        {
            return await _db.WishesLists.Where(w=>w.ClientId==ClientId).ToListAsync();    
        }
        public async Task<Wishlist?>GetClientWhishListItemById(int ClientId,int ItemId)
        {
            return await _db.WishesLists.FirstOrDefaultAsync(w => w.ClientId == ClientId && w.Id == ItemId);
        }



        public async Task<int> CreateAsync(int BookCopyId, int ClientId)
        {
            int ItemId = -1;

            try
            {

                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var WishlistItem = new Wishlist
                    {
                        BookCopyId = BookCopyId,
                        ClientId = ClientId,

                    };

                    _db.Add(WishlistItem);
                    await _db.SaveChangesAsync();
                    ItemId = WishlistItem.Id;
                    await transaction.CommitAsync();

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wishlist item creation transaction failed: {ex.Message}");
            }
            return ItemId;


        }

        public async Task<bool> DeleteAsync(int WishlistItemId, int ClientId)
        {
            bool Success = false;
            try
            {

                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var WishlistItem = await GetClientWhishListItemById(ClientId, WishlistItemId);

                    _db.WishesLists.Remove(WishlistItem!);
                    await _db.SaveChangesAsync();

                    await transaction.CommitAsync();

                });
                Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wishlist item deletion transaction failed: {ex.Message}");
            }
            return Success;


        }



    }
}
