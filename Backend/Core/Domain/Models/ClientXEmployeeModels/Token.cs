using EcommerceBackend.Core.Domain.Models.ClientModels;
using EcommerceBackend.Core.Domain.Models.EmployeeModels;
using System.ComponentModel.DataAnnotations;
namespace EcommerceBackend.Core.Domain.Models.ClientXEmployeeModels
{
    public class Token
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
        [Required]
        public DateTime RefreshTokenExpiryTime { get; set; }
        public  EmployeeAccount? EmployeeAccount { get; set; }
        public ClientAccount ? ClientAccount { get; set; }
    }
}
