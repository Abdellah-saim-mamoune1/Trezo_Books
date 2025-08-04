using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.Core.Domain.Models.CartModels;
using EcommerceBackend.DTO_s.CartDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.ClientRepositories
{
    public class CartRepository(AppDbContext _db): ICartRepository
    {

        public async Task<int> CreateAsync(int Id, int ClientId)
        {
            int ItemId = -1;

            try
            {

                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                    var Item = await _db.Carts.AsQueryable().FirstOrDefaultAsync(C => C.BookCopyId == Id&&C.ClientId==ClientId);
                    if (Item==null)
                    {
                        float price =await _db.BooksCopies.AsQueryable().Where(c => c.Id == Id).Select(c => c.Price).FirstAsync();
                        var CartItem = new CartItems
                        {
                            BookCopyId = Id,
                            ClientId = ClientId,
                            Quantity = 1,
                            Price = price,
                        };

                        _db.Add(CartItem);
                        await _db.SaveChangesAsync();
                        ItemId = CartItem.Id;
                    }
                    else
                    {
                        var BookCopyPrice = await _db.BooksCopies.AsQueryable().FirstAsync(b => b.Id == Id);
                        Item.Quantity++;
                        Item.Price+= BookCopyPrice.Price;
                        await _db.SaveChangesAsync();
                        ItemId = Item.Id;
                    }

                        await transaction.CommitAsync();

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cart item registration transaction failed: {ex.Message}");
            }
            return ItemId;


        }



        public async Task<bool> DeleteAsync(int ItemId, int clientId)
        {
            bool Success = false;

            try
            {

                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var CartItem = await _db.Carts.AsQueryable().FirstAsync(c => c.Id == ItemId && c.ClientId == clientId);
                    _db.Remove(CartItem);
                    await _db.SaveChangesAsync();

                    Success = true;
                    await transaction.CommitAsync();

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cart item deletion transaction failed: {ex.Message}");
            }
            return Success;


        }


        public async Task<bool> UpdateAsync(UpdateCartItemDto Item, int clientId)
        {
            bool Success = false;
            try
            {

                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var CartItem = await _db.Carts.AsQueryable().FirstAsync(c => c.Id == Item.CartItemId && c.ClientId == clientId);
                    CartItem.Quantity = Item.Quantity;
                    await _db.SaveChangesAsync();

                    Success = true;
                    await transaction.CommitAsync();

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cart item update transaction failed: {ex.Message}");
            }
            return Success;


        }

      
        public async Task<List<GetCartItemDto>> GetClientCartItemsAsync(int Id)
        {

            return await _db.Carts.AsQueryable().Include(c => c.bookCopy)
                .ThenInclude(c => c!.Book).Where(c => c.ClientId == Id).Select
                (
                c => new GetCartItemDto
                {
                    Id = c.Id,
                    ProductId = c.bookCopy!.Id,
                    Name = c.bookCopy!.Book!.Name,
                    ImageUrl = c.bookCopy.Book.ImageUrl,
                    TotalPrice = c.Price,
                    BookCopyQuantity=c.bookCopy.Quantity,
                    TotalQuantity = c.Quantity,
                    CreatedAt = c.CreatedAt
                }
                ).ToListAsync();
        }



    }
}
