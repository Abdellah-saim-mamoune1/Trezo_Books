using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IEBookRepository
    {
        public Task<int> Create(Book bookForm);
        public Task<bool> Update(DEBookGetXUpdate form);
        public Task<bool> Delete(int id);
        public Task<DEGetPaginatedBooks> GetPaginatedBooksAsync(DPaginationForm form);
        public Task<DEBookGetXUpdate?> GetBookByIdAsync(int Id);
        public Task<List<DEBookGetXUpdate>?> GetBookByNameAsync(string Name);
    }
}
