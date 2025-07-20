using EcommerceBackend.Core.Domain.Models.ClientXEmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Core.Domain.Models.BookModels
{
    public class Author
    {
      
        public int Id { get; set; }
        public required string FullName { get; set; }
        public IEnumerable<Book>? Books { get; set; }
    }
}
