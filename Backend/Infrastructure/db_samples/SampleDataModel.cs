using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.DTO_s.ClientDTO_s;
using EcommerceBackend.DTO_s.EmployeeDTO_s;

namespace EcommerceBackend.Infrastructure.db_samples
{
    public class SampleDataModel
    {
        public List<ClientSignUpDto> clients { get; set; } = new();
        public List<EmployeeSignUpDto> employees { get; set; } = new();
        public List<string> bookTypes { get; set; } = new();
        public List<string> employee_roles { get; set; } = new ();
    }
}
