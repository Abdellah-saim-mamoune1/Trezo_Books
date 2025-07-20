
namespace EcommerceBackend.DTO_s.EmployeeXClientDTO_s
{
    public class DPerson
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Gender { get; set; }
        public DateOnly? BirthDate { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
    }
}
