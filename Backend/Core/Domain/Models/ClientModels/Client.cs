using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.Core.Domain.Models.CartModels;
using EcommerceBackend.Core.Domain.Models.ClientXEmployeeModels;
using EcommerceBackend.Core.Domain.Models.OrderModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Core.Domain.Models.ClientModels
{
    public class Client
    {
        [Key, ForeignKey("Person")]
        public int PersonId { get; set; }

        [ForeignKey("ClientAccount")]
        public int AccountId { get; set; }
        public IEnumerable<CartItems>? Carts { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
        public IEnumerable<Wishlist>? Wishlist { get; set; }
        public IEnumerable<BookCopyRating>? BookCopyRatings { get; set; }
        public Person? Person { get; set; }
        public ClientAccount? Account { get; set; }

    }
}
