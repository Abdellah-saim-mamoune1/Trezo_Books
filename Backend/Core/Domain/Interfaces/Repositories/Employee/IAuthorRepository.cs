using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IAuthorRepository
    {
        public Task<int> CreateAsync(AuthorDto author);
        public Task<bool> DeleteAsync(int Id);
        public Task<bool> UpdateAsync(AuthorGetXUpdateDto AuthorInfo);
        public Task<AuthorGetXUpdateDto?> GetAuthorByIdAsync(int Id);
        public Task<List<AuthorGetXUpdateDto>?> GetAuthorByName(string Name);
        public Task<GetPaginatedAuthorsDto> GetPaginatedAuthorsAsync(PaginationFormDto form);
        public IQueryable<Author> GetAllAuthorsQueryable();
    }
}
