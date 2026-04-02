using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.EmployeeRepositories
{
    public class AuthorRepository(AppDbContext _db) : IAuthorRepository
    {
        public async Task<int> Create(AuthorDto authorForm)
        {
            int Id = -1;
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var author = new Author
                    {
                        FullName = authorForm.FullName
                    };

                    _db.Add(author);
                    await _db.SaveChangesAsync();

                    Id = author.Id;
                    await transaction.CommitAsync();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Author creation transaction failed: {ex.Message}");
            }

            return Id;
        }

        public async Task<bool> Update(AuthorGetXUpdateDto form)
        {
            bool success = false;
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var author = await _db.Authors.AsQueryable().FirstAsync(a => a.Id == form.Id);
                    author.FullName = form.FullName;

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                });
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Author update transaction failed: {ex.Message}");
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

                    var author = await _db.Authors.AsQueryable().FirstAsync(a => a.Id == id);
                    _db.Authors.Remove(author);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    success = true;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Author deletion transaction failed: {ex.Message}");
            }

            return success;
        }

        public async Task<GetPaginatedAuthorsDto> GetPaginatedAuthorsAsync(PaginationFormDto form)
        {
            var allAuthorsQueryable = _db.Authors.AsQueryable();

            var Authors = await allAuthorsQueryable
                .Select(a => new AuthorGetXUpdateDto
                {
                    Id = a.Id,
                    FullName = a.FullName
                })
                .Skip((form.pageNumber - 1) * form.pageSize)
                .Take(form.pageSize)
                .ToListAsync();

            var totalCount = await allAuthorsQueryable.CountAsync();

            return new GetPaginatedAuthorsDto
            {
                PageSize = Authors.Count,
                PageNumber = form.pageNumber,
                Quantity = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / form.pageSize),
                Authors = Authors
            };
        }

        public async Task<AuthorGetXUpdateDto?> GetAuthorByIdAsync(int Id)
        {
            return await _db.Authors.AsQueryable().Where(a => a.Id == Id)
                .Select(a => new AuthorGetXUpdateDto
                {
                    Id = a.Id,
                    FullName = a.FullName
                }).FirstOrDefaultAsync();
        }
    }
}