using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.Core.Domain.Models.ClientModels;
using EcommerceBackend.Core.Domain.Models.EmployeeModels;
using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Core.Domain.Models.ClientXEmployeeModels
{
    public class Person
    {


        [Key]
        public int Id { get; set; }
        [Required, MaxLength(25, ErrorMessage = "Invalid FirstName size")]
        public required string FirstName { get; set; }
        [Required, MaxLength(25, ErrorMessage = "Invalid LastName size")]
        public required string LastName { get; set; }

        public string? Gender { get; set; }=string.Empty;

        public DateOnly? BirthDate { get; set; }

        [Required, MinLength(4), MaxLength(22, ErrorMessage = "Invalid phone number size")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public Employee? Employee { get; set; }

        public Client? Client { get; set; }

     
    }
}
