using EcommerceBackend.Core.Domain.Models.ClientXEmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Core.Domain.Models.EmployeeModels
{
    public class EmployeeAccount
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string AccountAddress { get; set; }

        [Required]
        public required string Password { get; set; }

        [ForeignKey("Token")]
        public int TokenId { get; set; }

        [ForeignKey("EmployeeAccountType")]
        public int AccountTypeId { get; set; }

        [Required]
        public bool IsFrozen { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public  Employee? Employee { get; set; }
        public  Token? token { get; set; }
        public EmployeeAccountType? EmployeeAccountType { get; set; }
    }
}
