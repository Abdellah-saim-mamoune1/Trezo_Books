using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.EmployeeRepositories
{
    public class EOrdersRepository(AppDbContext _db): IEGetPaginatedOrders
    {


        public async Task<DGetPaginatedOrder> GetPaginatedClientOrdersById(DPaginationForm form)
        {
            var AllOrders = _db.Orders.AsQueryable();
            var orders = await AllOrders
            .Where(o => o.OrderItems!.Any())
            .Include(o => o.OrderItems!)
            .ThenInclude(oi => oi.BookCopy)
            .Select(o => new CDGetOrder
            {

                ShipmentAddress = o.ShipmentAddress,
                Status = o.Status,
                ClientId = o.ClientId,
                ArrivedAt = o.ArrivedAt,
                CreatedAt = o.CreatedAt,
                Id = o.Id,
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

            return new DGetPaginatedOrder
            {
                Orders = orders,
                PageSize = orders.Count,
                PageNumber = form.pageNumber,
                Quantity = AllOrders.Count(),
                TotalPages = (int)Math.Ceiling((double)AllOrders.Count() / form.pageSize),
            };


        }

        public async Task<bool> SetOrderStatus(int Id,string status)
        {
            try
            {
                var order = await _db.Orders.FirstAsync(o => o.Id == Id);
                order.Status = status;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
