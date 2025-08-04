using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IBookRepository
    {
        public Task<int> Create(Book bookForm);
        public Task<bool> Update(BookGetXUpdateDto form);
        public Task<bool> Delete(int id);
        public Task<GetPaginatedBooksDto> GetPaginatedBooksAsync(PaginationFormDto form);
        public Task<BookGetXUpdateDto?> GetBookByIdAsync(int Id);
        public Task<List<BookGetXUpdateDto>?> GetBookByNameAsync(string Name);
    }
}
