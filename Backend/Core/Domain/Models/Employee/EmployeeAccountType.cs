using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Core.Domain.Models.EmployeeModels
{
    public class EmployeeAccountType
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20, ErrorMessage = "Invalid role name length")]
        public string TypeName { get; set; } = string.Empty;
        public List< EmployeeAccount>? EmployeeAccounts { get; set; }

    }
}
