using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.CBookTypeDTO_s;


namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface ICBookCopyRepository
    {
        public Task<List<DCGetInitialBooksCopiesDataPerType>> GetInitialBooksCopiesDataPerTypeAsync();
        public Task<DCGetInitialPaginatedBooksCopiesDataByType> GetPaginatedBooksCopiesByTypeAsync(DCGetPaginatedBooksTypes form);
        public Task<List<DCGetInitialBookCopyData>> GetInitialBestSellersBooksCopiesDataAsync();
        public Task<List<DCGetInitialBookCopyData>> GetInitialRecommendedBooksCopiesDataAsync();
        public Task<List<DCGetInitialBookCopyData>> GetInitialNewReleasesBooksCopiesDataAsync();
        public Task<List<DCGetInitialBookCopyData>> GetInitialTopRatedBooksCopiesDataAsync();
        public Task<List<DEGetBooksTypes>> GetBooksTypesAsync();
        public Task<List<DCGetBookCopy>> GetBooksCopiesInfoByNameAsync(string Name);
        public Task<DCGetBookCopy> GetBookCopyInfoByIdAsync(int Id);
    }
}
