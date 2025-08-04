using EcommerceBackend.DTO_s.EmployeeXClientDTO_s;

namespace EcommerceBackend.DTO_s.EmployeeDTO_s
{
    public class EmployeeSignUpDto
    {
        public PersonDto? Person_informations { get; set; }
        public string Password { get; set; }=string.Empty;
        public string Role { get; set; }=string.Empty;
    }
}
