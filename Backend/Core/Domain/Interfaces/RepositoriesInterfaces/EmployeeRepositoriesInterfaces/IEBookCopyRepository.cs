using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IEBookCopyRepository
    {
        public Task<int> Create(DEBookCopy BookCopy);
        public Task<bool> Delete(int id);
        public Task<bool> Update(DBookCopyGetXUpdate form);
        public Task<DEGetPaginatedBooksCopies> GetPaginatedBooksCopiesAsync(DPaginationForm form);
        public Task<DBookCopyGetXUpdate?> GetBookCopyByIdAsync(int Id);
     
    }
}
