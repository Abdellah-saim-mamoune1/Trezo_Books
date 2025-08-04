using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Models.OrderModels;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.ClientRepositories
{
    public class OrderRepository
        (

        AppDbContext _db

        ): IOrderRepository
    {
        public async Task<bool> CreateAsync(AddOrderDto Items,int ClientId)
        {
            bool Success = false;

            try
            {

                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                        var order = new Order
                        {
                            ClientId = ClientId,
                            TotalPrice = Items.TotalPrice,
                            TotalQuantity =Items.TotalQuantity,
                            ShipmentAddress = Items.ShipmentAddress,
                            Status = "Processing",
                        };

                        _db.Add(order);
                        await _db.SaveChangesAsync();
                        await CreateOrdersItems(order.Id, Items.CartItemsIds!);
                        await transaction.CommitAsync();

                });
                Success= true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Order creation transaction failed: {ex.Message}");
            }
            return Success;
        }

        public async Task<GetPaginatedOrderDto> GetPaginatedClientOrdersById(PaginationFormDto form, int ClientId)
        {
            var AllOrders = _db.Orders.AsQueryable();
            var orders = await AllOrders
            .Where(o => o.OrderItems!.Any() && o.ClientId == ClientId)
            .Include(o => o.OrderItems!)
            .ThenInclude(oi => oi.BookCopy)
            .Select(o => new CDGetOrder
            {

                ShipmentAddress = o.ShipmentAddress,
                Status = o.Status,
                ArrivedAt = o.ArrivedAt,
                CreatedAt = o.CreatedAt,
                Id = o.Id,
                ClientId=o.ClientId,
                TotalPrice = o.TotalPrice,
                TotalQuantity = o.TotalQuantity,
                Items = o.OrderItems!.Select(I => new CDGetOrderItem
                {
                    Id = I.Id,
                    Name = I.BookCopy!.Book!.Name,
                    ImageUrl = I.BookCopy.Book.ImageUrl,
                    CreatedAt = I.CreatedAt,
                    TotalPrice = I.Price,
                    Quantity = I.Quantity
                }).ToList()

            })
            .Skip((form.pageNumber - 1) * form.pageSize)
            .Take(form.pageSize).ToListAsync();

            return new GetPaginatedOrderDto
            {
                Orders = orders,
                PageSize = orders.Count,
                PageNumber = form.pageNumber,
                Quantity = AllOrders.Count(),
                TotalPages = (int)Math.Ceiling((double)AllOrders.Count() / form.pageSize),
            };
        }
        private async Task CreateOrdersItems(int OrderId, List<int> ItemsIds)
        {
            List<OrderItem> OrdersItems = new();
            foreach (var Id in ItemsIds)
            {
                var Item = await _db.Carts.AsQueryable().FirstAsync(c => c.Id == Id);
                var OrderItem = new OrderItem
                {
                    Quantity = Item.Quantity,
                    Price = Item.Price,
                    BookCopyId = Item.BookCopyId,
                    OrderId = OrderId,
                };
                var BookCopy = await _db.BooksCopies.AsQueryable().FirstAsync(b => b.Id == Item.BookCopyId);
                BookCopy.Quantity-=OrderItem.Quantity;
                await _db.SaveChangesAsync();
                OrdersItems.Add(OrderItem);
            }

            _db.AddRange(OrdersItems);
            await _db.SaveChangesAsync();

        }






    }
}
