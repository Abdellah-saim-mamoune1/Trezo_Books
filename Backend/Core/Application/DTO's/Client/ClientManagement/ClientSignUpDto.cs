using EcommerceBackend.DTO_s.EmployeeXClientDTO_s;

namespace EcommerceBackend.DTO_s.ClientDTO_s
{
    public class ClientSignUpDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }= string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public LoginDto? Account_informations { get; set; }

    }
}
