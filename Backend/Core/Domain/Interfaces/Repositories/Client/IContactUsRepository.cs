using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface IContactUsRepository
    {
        public  Task<bool> Create(ContactUsSetDto form);
    }
}
