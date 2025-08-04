namespace EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s.BookDTO_s
{
    public class AddBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Language { get; set; } = "en";
    }
}
