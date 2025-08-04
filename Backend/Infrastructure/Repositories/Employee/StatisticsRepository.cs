using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s.StatisticsDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.DTO_s.StatisticsDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.EmployeeRepositories
{
    public class StatisticsRepository(AppDbContext _db): IStatisticsRepository
    {

        public async Task<GetTotalsDto> GetTotalsAsync()
        {

            var ClientsNumber = await _db.Clients.AsQueryable().CountAsync();
            var BooksNumber=await _db.Books.AsQueryable().CountAsync();
            var OrdersNumber = await _db.Orders.AsQueryable().CountAsync();
            var BooksCopiesNumber=await _db.BooksCopies.AsQueryable().CountAsync();

            var stats = new GetTotalsDto
            {
                TotalBooks = BooksNumber,
                TotalOrders = OrdersNumber,
                TotalBooksCopies = BooksCopiesNumber,
                TotalClients = ClientsNumber
            };

            return stats;
        }

        public async Task<List<GetResentOrdersDto>> GeResentOrdersAsync()
        {
            var count = await _db.Orders.AsQueryable().CountAsync();

            var data = await _db.Orders.AsQueryable().Select(s => new GetResentOrdersDto
            {
                Status = s.Status,
                TotalPrice = s.TotalPrice,
                ClientId = s.ClientId,
                Id = s.Id,
                TotalQuantity = s.TotalQuantity,

            }).Skip(count - 5).Take(5).ToListAsync();
             data.Reverse();

            return data;
        }


       
        public async Task<List<GetClientsDto>> GeNewClientsAsync()
        {
            var count = await _db.Clients.AsQueryable().CountAsync();

            var data = await _db.Clients.Include(c => c.Account).Include(c => c.Person).AsQueryable()
                .Select(c => new GetClientsDto
                {
                    Account = c.Account!.Account,
                    FirstName = c.Person!.FirstName,
                    LastName = c.Person.LastName,
                    Id = c.PersonId,
                    PhoneNumber = c.Person.PhoneNumber,
                    CreatedAt = c.Account.CreatedAt

                }).OrderByDescending(C=>C.CreatedAt).Take(5).ToListAsync();
            

            return data;
        }

        public async Task<List<GetClientsDto>> GeAllClientsAsync()
        {
            var count = await _db.Clients.AsQueryable().CountAsync();

            var data = await _db.Clients.Include(c => c.Account).Include(c => c.Person).AsQueryable()
                .Select(c => new GetClientsDto
                {
                    Account = c.Account!.Account,
                    FirstName = c.Person!.FirstName,
                    LastName = c.Person.LastName,
                    Id = c.PersonId,
                    PhoneNumber = c.Person.PhoneNumber,
                    CreatedAt = c.Account.CreatedAt

                }).ToListAsync();
           
            return data;
        }




    }
}
