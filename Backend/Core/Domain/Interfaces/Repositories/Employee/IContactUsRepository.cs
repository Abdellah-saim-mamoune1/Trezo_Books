using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.Core.Domain.Models.EmployeeModels;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IContactUsRepository
    {
        public Task<List<ContactUs>> Get();
        public Task<bool> Delete(int Id);
        
    }
}
