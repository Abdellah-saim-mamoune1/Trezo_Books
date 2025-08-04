using EcommerceBackend.Core.Domain.Models.ClientXEmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Core.Domain.Models.EmployeeModels
{
    public class Employee
    {
        [Key, ForeignKey("Person")]
        public int PersonId { get; set; }

        [ForeignKey("EmployeeAccount")]
        public int AccountId { get; set; }
        public Person? Person { get; set; }
        public  EmployeeAccount? EmployeeAccount { get; set; }
      
    }
}
