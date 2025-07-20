using EcommerceBackend.Core.Domain.Models.CartModels;
using EcommerceBackend.Core.Domain.Models.ClientModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Core.Domain.Models.BookModels
{
    public class BookCopy
    {
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public float Price { get; set; }
        public float Rating { get; set; } = 0;
        public DateTime? CreatedAt { get; set; }
        public bool IsAvailable { get; set; }
        public IEnumerable<CartItems>? cartItems { get; set; }
        public IEnumerable<Wishlist>? Wishlist { get; set; }
        public IEnumerable<BookCopyRating>? BookCopyRatings { get; set; }
        public Book? Book { get; set; }
    }
}
