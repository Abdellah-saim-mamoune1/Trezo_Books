using EcommerceBackend.DTO_s.EmployeeXClientDTO_s;

namespace EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s
{
    public class GetEmployeesDto:PersonDto
    {
        public int Id { get; set; }
        public string Type { get; set; }=string.Empty;
       
    }
}
