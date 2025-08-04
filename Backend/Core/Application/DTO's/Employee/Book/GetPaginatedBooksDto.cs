using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Application.DTO_s.BookDTO_s
{
    public class GetPaginatedBooksDto:PaginationDto
    {
        public IEnumerable<BookGetXUpdateDto>? Books { get; set; }
    }
}
