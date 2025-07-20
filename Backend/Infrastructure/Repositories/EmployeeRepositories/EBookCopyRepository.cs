using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.EmployeeRepositories
{
    public class EBookCopyRepository(AppDbContext _db): IEBookCopyRepository
    {


        public async Task<int> Create(DEBookCopy BookCopy)
        {
            int Id = -1;
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                    var book = new BookCopy
                    {
                        BookId=BookCopy.BookId,
                        Quantity=BookCopy.Quantity,
                        Price=BookCopy.Price,
                        IsAvailable=BookCopy.IsAvailable
                    };

                    _db.Add(book);
                    await _db.SaveChangesAsync();

                    Id = book.Id;
                    await transaction.CommitAsync();

                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Book copy creation transaction failed: {ex.Message}");

            }

            return Id;
        }

        public async Task<bool> Delete(int id)
        {
            bool success = false;
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var bookCopy = await _db.BooksCopies.FirstAsync(b => b.Id == id);

                    _db.Remove(bookCopy);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    success = true;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Book copy deletion transaction failed: {ex.Message}");
            }

            return success;
        }


        public async Task<bool> Update(DBookCopyGetXUpdate form)
        {
            bool success = false;
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var bookCopy = await _db.BooksCopies.FirstAsync(b => b.Id == form.Id);

                    bookCopy.BookId = form.BookId;
                    bookCopy.Price = form.Price;
                    bookCopy.Quantity = form.Quantity;
                    bookCopy.IsAvailable = form.IsAvailable;

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                });
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Book copy update transaction failed: {ex.Message}");
            }

            return success;
        }


        public async Task<DEGetPaginatedBooksCopies> GetPaginatedBooksCopiesAsync(DPaginationForm form)
        {
            var allAuthorsQueryable = _db.BooksCopies.AsQueryable();

            var BooksCopies = await allAuthorsQueryable
                .Select(a => new DBookCopyGetXUpdate
                {
                    Id = a.Id,
                    BookId = a.BookId,
                    Price = a.Price,
                    Quantity = a.Quantity,
                    IsAvailable = a.IsAvailable
                })
                .Skip((form.pageNumber - 1) * form.pageSize)
                .Take(form.pageSize)
                .ToListAsync();

            var totalCount = await allAuthorsQueryable.CountAsync();

            return new DEGetPaginatedBooksCopies
            {
                PageSize = BooksCopies.Count,
                PageNumber = form.pageNumber,
                Quantity = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / form.pageSize),
                BooksCopies = BooksCopies
            };
        }


        public async Task<DBookCopyGetXUpdate?> GetBookCopyByIdAsync(int Id)
        {

           return  await _db.BooksCopies.AsQueryable()
                .Select(a => new DBookCopyGetXUpdate
                {
                    Id = a.Id,
                    BookId = a.BookId,
                    Price = a.Price,
                    Quantity = a.Quantity,
                    IsAvailable = a.IsAvailable
                }).FirstOrDefaultAsync(b => b.Id == Id);
               
        }

    }
}
