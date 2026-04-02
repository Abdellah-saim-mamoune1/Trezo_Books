using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IAuthorRepository
    {
        public Task<int> Create(AuthorDto authorForm);
        public Task<bool> Update(AuthorGetXUpdateDto form);
        public Task<bool> Delete(int id);
        public Task<GetPaginatedAuthorsDto> GetPaginatedAuthorsAsync(PaginationFormDto form);
        public Task<AuthorGetXUpdateDto?> GetAuthorByIdAsync(int Id);
    }
}