using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface ICContactUsRepository
    {
        public  Task<bool> Create(DEContactUsSet form);
    }
}
