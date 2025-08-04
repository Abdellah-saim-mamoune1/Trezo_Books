using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using EcommerceBackend.UtilityClasses;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.EmployeeRepositories
{
    public class BookRepository(AppDbContext _db): IBookRepository
    {

        public async Task<int> Create(Book book)
        {
            int Id = -1;
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    _db.Add(book);
                   await  _db.SaveChangesAsync();

                    Id = book.Id;
                    await transaction.CommitAsync();

                });
              
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Book creation transaction failed: {ex.Message}");
               
            }

            return Id;
        }

        public async Task<bool> Update(BookGetXUpdateDto form)
        {
            bool success = false;
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var book = await _db.Books.AsQueryable().FirstAsync(b => b.Id == form.Id);

                   

                    book.Name = form.Name;
                    book.TypeId = form.TypeId;
                    book.AuthorId = form.AuthorId;
                    book.Description = form.Description;
                    book.PublishedAt = form.PublishedAt;
                    book.Pages = form.PagesNumber;

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                });
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Book update transaction failed: {ex.Message}");
            }

            return success;
        }

        public async Task<bool> Delete(int id)
        {
            bool success = false;
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var book = await _db.Books.AsQueryable().FirstAsync(b => b.Id == id);

                    UMethods.DeleteProductImageFromDisk("MainProductImages", book.ImageUrl);

                    _db.Books.Remove(book);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    success = true;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Book deletion transaction failed: {ex.Message}");
            }

            return success;
        }



        public async Task<GetPaginatedBooksDto> GetPaginatedBooksAsync(PaginationFormDto form)
        {
            var allAuthorsQueryable = _db.Books.AsQueryable();

            var Books = await allAuthorsQueryable
                .Select(a => new BookGetXUpdateDto
                {
                    Id = a.Id,
                    AuthorId = a.AuthorId,
                    Name = a.Name,
                    Image = a.ImageUrl,
                    PagesNumber = a.Pages,
                    Description = a.Description,
                    PublishedAt = a.PublishedAt,
                    TypeId = a.TypeId

                })
                .Skip((form.pageNumber - 1) * form.pageSize)
                .Take(form.pageSize)
                .ToListAsync();

            var totalCount = await allAuthorsQueryable.CountAsync();

            return new GetPaginatedBooksDto
            {
                PageSize = Books.Count,
                PageNumber = form.pageNumber,
                Quantity = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / form.pageSize),
                Books = Books
            };
        }


        public async Task<BookGetXUpdateDto?> GetBookByIdAsync(int Id)
        {


            return  await _db.Books.AsQueryable().Where(b => b.Id == Id)
                .Select(a => new BookGetXUpdateDto
                {
                    Id = a.Id,
                    AuthorId = a.AuthorId,
                    Name = a.Name,
                    Image = a.ImageUrl,
                    PagesNumber = a.Pages,
                    Description = a.Description,
                    PublishedAt = a.PublishedAt,
                    TypeId = a.TypeId

                }).FirstOrDefaultAsync();
           
        }

        public async Task<List<BookGetXUpdateDto>?> GetBookByNameAsync(string Name)
        {

            return await _db.Books.AsQueryable().Where(b => b.Name.ToLower().Contains(Name.ToLower()))
                .Select(a => new BookGetXUpdateDto
                {
                    Id = a.Id,
                    AuthorId = a.AuthorId,
                    Name = a.Name,
                    Image = a.ImageUrl,
                    PagesNumber = a.Pages,
                    Description = a.Description,
                    PublishedAt = a.PublishedAt,
                    TypeId = a.TypeId

                }).ToListAsync();
        }

    }
}   

