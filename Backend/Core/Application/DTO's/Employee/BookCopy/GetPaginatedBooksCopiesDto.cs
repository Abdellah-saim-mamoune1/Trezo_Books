using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s
{
    public class GetPaginatedBooksCopiesDto:PaginationDto
    {
        public IEnumerable<DBookCopyGetXUpdate>? BooksCopies { get; set; }
    }
}
