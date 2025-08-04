using EcommerceBackend.Core.Domain.Models.BookModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Core.Domain.Models.ClientModels
{
    public class Wishlist
    {
        public int Id { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        [ForeignKey("BookCopy")]
        public int BookCopyId { get; set; }
        public Client? Client { get; set; }
        public BookCopy? BookCopy { get; set; }

    }
}
