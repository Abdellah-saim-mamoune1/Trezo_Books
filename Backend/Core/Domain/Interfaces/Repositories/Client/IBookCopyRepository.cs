using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.CBookTypeDTO_s;


namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface IBookCopyRepository
    {
        public Task<List<GetInitialBooksCopiesDataPerTypeDto>> GetInitialBooksCopiesDataPerTypeAsync();
        public Task<GetInitialPaginatedBooksCopiesDataByTypeDto> GetPaginatedBooksCopiesByTypeAsync(GetPaginatedBooksTypesDto form);
        public Task<List<DCGetInitialBookCopyData>> GetInitialBestSellersBooksCopiesDataAsync();
        public Task<List<DCGetInitialBookCopyData>> GetInitialRecommendedBooksCopiesDataAsync();
        public Task<List<DCGetInitialBookCopyData>> GetInitialNewReleasesBooksCopiesDataAsync();
        public Task<List<DCGetInitialBookCopyData>> GetInitialTopRatedBooksCopiesDataAsync();
        public Task<List<GetBooksTypesDto>> GetBooksTypesAsync();
        public Task<List<GetBookCopyDto>> GetBooksCopiesInfoByNameAsync(string Name);
        public Task<GetBookCopyDto> GetBookCopyInfoByIdAsync(int Id);
    }
}
