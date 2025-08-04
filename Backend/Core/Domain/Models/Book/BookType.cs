namespace EcommerceBackend.Core.Domain.Models.BookModels
{
    public class BookType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Book>? Books  { get; set; }
    }
}
