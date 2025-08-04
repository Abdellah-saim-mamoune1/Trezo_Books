using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.CBookTypeDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Application.DTO_s.ClientDTO_s
{
    public class GetInitialPaginatedBooksCopiesDataByTypeDto:PaginationDto
    {
        public IEnumerable<DCGetInitialBookCopyData>? BooksCopies { get; set; }
    }
}
