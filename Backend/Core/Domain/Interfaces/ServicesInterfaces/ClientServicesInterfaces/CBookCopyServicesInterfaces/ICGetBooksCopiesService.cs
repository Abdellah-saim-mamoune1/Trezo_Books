using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.CBookTypeDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CBookCopyServicesInterfaces
{
    public interface ICGetBooksCopiesService
    {
        public Task<DApiResponse<object?>> GetInitialBooksCopiesDataPerType();
        public Task<DApiResponse<object?>> GetInitialBooksCopiesDataByType(DCGetPaginatedBooksTypes form);
        public Task<DApiResponse<object?>> GetInitialNewReleasedBooksCopiesData();
        public Task<DApiResponse<object?>> GetInitialBestSellersBooksCopiesData();
        public Task<DApiResponse<object?>> GetInitialRecommendedBooksCopiesData();
        public Task<DApiResponse<object?>> GetInitialTopRatedBooksCopiesData();
        public Task<DApiResponse<object?>> GetBooksTypes();
        public Task<DApiResponse<object?>> GetBooksCopiesInfoByName(string Name);
        public Task<DApiResponse<object?>> GetBookCopyInfoById(int Id);
    }
}
