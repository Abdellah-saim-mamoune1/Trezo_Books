
namespace EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s.StatisticsDTO_s
{
    public class GetClientsDto
    {
        public int  Id { get; set; }
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; }= string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
