using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using EcommerceBackend.UtilityClasses;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.EmployeeRepositories
{
    public class AuthorRepository(AppDbContext _db) : IAuthorRepository
    {
        public async Task<int> CreateAsync(AuthorDto author)
        {
            int AuthorId = -1;

            try
            {
               
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                  
                    var Author = new Author
                    {
                        FullName = author.FullName

                    };
    
                    _db.Add(Author);
                    await _db.SaveChangesAsync();

                    AuthorId = Author.Id;

                    await transaction.CommitAsync();

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Author Registration Transaction failed: {ex.Message}");
            }
            return AuthorId;


        }
        public async Task<bool> DeleteAsync(int Id)
        {
            bool Success = false;
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var Author = await GetAllAuthorsQueryable().FirstAsync(author => author.Id == Id);

                    _db.Remove(Author);
                    await _db.SaveChangesAsync();

                    await transaction.CommitAsync();
                    Success = true;

                });
                return ValidationStatus.Success;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Author deletion Transaction failed: {ex.Message}");
            }

            return Success;

        }
        public async Task<bool> UpdateAsync(AuthorGetXUpdateDto AuthorInfo)
        {
            bool Success = false;
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                    var Author = await GetAllAuthorsQueryable().FirstAsync(author => author.Id == AuthorInfo.Id);
                    Author.FullName = AuthorInfo.FullName;
                   
                    await _db.SaveChangesAsync();

                    await transaction.CommitAsync();

                    Success = true;
                });


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Author update Transaction failed: {ex.Message}");

            }
            return Success;

        }
        public async Task<GetPaginatedAuthorsDto> GetPaginatedAuthorsAsync(PaginationFormDto form)
        {
            var allAuthorsQueryable = GetAllAuthorsQueryable();

            var authors = await allAuthorsQueryable
                .Select(a => new AuthorGetXUpdateDto
                {
                    Id = a.Id,
                    FullName = a.FullName,
                })
                .Skip((form.pageNumber - 1) * form.pageSize)
                .Take(form.pageSize)
                .ToListAsync();

            var totalCount = await allAuthorsQueryable.CountAsync();

            return new GetPaginatedAuthorsDto
            {
                PageSize = authors.Count,
                PageNumber = form.pageNumber,
                Quantity = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / form.pageSize),
                Authors = authors
            };
        }
        public IQueryable<Author> GetAllAuthorsQueryable()
        {
            return _db.Authors.AsQueryable();
        }


        public async Task<AuthorGetXUpdateDto?> GetAuthorByIdAsync(int Id)
        {
            return await _db.Authors.AsQueryable().Where(a => a.Id == Id).Select(a => new AuthorGetXUpdateDto
            {
                Id = a.Id,
                FullName = a.FullName,
            }).FirstOrDefaultAsync();
        }

        public async Task<List<AuthorGetXUpdateDto>?> GetAuthorByName(string Name)
        {
            return await _db.Authors.Where(a => a.FullName.ToLower().Contains(Name.ToLower())).Select(a => new AuthorGetXUpdateDto
            {
                Id = a.Id,
                FullName = a.FullName,
            }).ToListAsync();
        }

    }
}
