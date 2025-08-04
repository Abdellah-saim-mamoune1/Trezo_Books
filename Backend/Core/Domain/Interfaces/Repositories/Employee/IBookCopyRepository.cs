using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IBookCopyRepository
    {
        public Task<int> Create(BookCopyDto BookCopy);
        public Task<bool> Delete(int id);
        public Task<bool> Update(DBookCopyGetXUpdate form);
        public Task<GetPaginatedBooksCopiesDto> GetPaginatedBooksCopiesAsync(PaginationFormDto form);
        public Task<DBookCopyGetXUpdate?> GetBookCopyByIdAsync(int Id);
     
    }
}
