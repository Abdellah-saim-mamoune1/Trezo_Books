using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IEAuthorRepository
    {
        public Task<int> CreateAsync(DEAuthor author);
        public Task<bool> DeleteAsync(int Id);
        public Task<bool> UpdateAsync(DEAuthorGetXUpdate AuthorInfo);
        public Task<DEAuthorGetXUpdate?> GetAuthorByIdAsync(int Id);
        public Task<List<DEAuthorGetXUpdate>?> GetAuthorByName(string Name);
        public Task<DEGetPaginatedAuthors> GetPaginatedAuthorsAsync(DPaginationForm form);
        public IQueryable<Author> GetAllAuthorsQueryable();
    }
}
