using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Core.Domain.Models.BookModels
{
    public class Book
    {
        public int Id {  get; set; }
        [Required]
        public string Name { get; set; }=string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [ForeignKey("BookType")]
        public int TypeId { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public int Pages { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        public float averageRating{ get; set; }
        public int ratingsCount { get; set; }

        [Required]
        public DateOnly? PublishedAt { get; set; }
        public BookType? BookType { get; }
        public Author? Author { get; set; }
        public BookCopy? BookCopies { get; set; }
    }
}
