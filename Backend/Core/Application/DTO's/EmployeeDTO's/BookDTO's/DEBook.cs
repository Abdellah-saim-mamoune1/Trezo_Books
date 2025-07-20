namespace EcommerceBackend.Core.Application.DTO_s.BookDTO_s
{
    public class DEBook
    {
        public string Name { get; set; }=string.Empty;
        public string Image { get; set; } = string.Empty;
        public required int PagesNumber { get; set; }
        public int TypeId { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateOnly? PublishedAt { get; set; }
    }
}
