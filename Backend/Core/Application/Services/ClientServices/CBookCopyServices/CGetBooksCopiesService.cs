using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.CBookTypeDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Application.Validators.SharedValidators;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CBookCopyServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.Infrastructure.Data;
using EcommerceBackend.UtilityClasses;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Core.Application.Services.ClientServices.CBookCopyServices
{
    public class CGetBooksCopiesService
        (
        ICBookCopyRepository _Repo,
        AppDbContext _db
      
        ): ICGetBooksCopiesService
    {
        public async Task<DApiResponse<object?>> GetInitialBooksCopiesDataPerType()
        {
            var Data = await _Repo.GetInitialBooksCopiesDataPerTypeAsync();
            return UApiResponder<object>.Success(Data, "Books copies were retrieved successfully.");
        }


        public async Task<DApiResponse<object?>> GetInitialRecommendedBooksCopiesData()
        {
            var Data = await _Repo.GetInitialRecommendedBooksCopiesDataAsync();
            return UApiResponder<object>.Success(Data, "Recommended books copies were retrieved successfully.");
        }

        public async Task<DApiResponse<object?>> GetInitialTopRatedBooksCopiesData()
        {
            var Data = await _Repo.GetInitialTopRatedBooksCopiesDataAsync();
            return UApiResponder<object>.Success(Data, "Top rated books copies were retrieved successfully.");
        }


        public async Task<DApiResponse<object?>> GetInitialBestSellersBooksCopiesData()
        {
            var Data = await _Repo.GetInitialBestSellersBooksCopiesDataAsync();
            return UApiResponder<object>.Success(Data, "Best sellers books copies were retrieved successfully.");
        }

        public async Task<DApiResponse<object?>> GetInitialNewReleasedBooksCopiesData()
        {
            var Data = await _Repo.GetInitialNewReleasesBooksCopiesDataAsync();
            return UApiResponder<object>.Success(Data, "New released books copies were retrieved successfully.");
        }

        public async Task<DApiResponse<object?>> GetBooksTypes()
        {
            var Data = await _Repo.GetBooksTypesAsync();
            return UApiResponder<object>.Success(Data, "Books types were retrieved successfully.");
        }


        public async Task<DApiResponse<object?>> GetInitialBooksCopiesDataByType(DCGetPaginatedBooksTypes form)
        {
            List<DValidationErorrs> errors = new();
            var Validate = new PaginationFormValidator();
            var result=Validate.Validate(form);
            if(!result.IsValid)
                errors = result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();

            if (!await ValidateBookTypeExistence(form.Type))
            {
                errors.Add(new DValidationErorrs { FieldId = "Type", Message = "Book type not found." });
            }
            
            if(errors.Count!=0)
                return UApiResponder<object>.Fail("Invalid pieces of information.", errors,400);

            var Data = await _Repo.GetPaginatedBooksCopiesByTypeAsync(form);
            return UApiResponder<object>.Success(Data, "Books copies were retrieved successfully.");
        }

        public async Task<DApiResponse<object?>> GetBookCopyInfoById(int Id)
        {
            List<DValidationErorrs> errors = new();

            if (!await ValidateBookCopyExistenceById(Id))
            {
                errors.Add(new DValidationErorrs { FieldId = "Id", Message = "Book copy not found." });
            }

            if (errors.Count != 0)
                return UApiResponder<object>.Fail("Invalid pieces of information.", errors, 400);

            var Data = await _Repo.GetBookCopyInfoByIdAsync(Id);
            return UApiResponder<object>.Success(Data, "Book copy was retrieved successfully.");
        }


        public async Task<DApiResponse<object?>> GetBooksCopiesInfoByName(string Name)
        {
            List<DValidationErorrs> errors = new();

            var Data = await _Repo.GetBooksCopiesInfoByNameAsync(Name);
            return UApiResponder<object>.Success(Data, "Books copies were retrieved successfully.");
        }


        private async Task<bool>ValidateBookTypeExistence(string Type)
        {
            return await _db.BooksTypes.AnyAsync(t => t.Name == Type);
        }
        private async Task<bool> ValidateBookCopyExistenceById(int Id)
        {
            return await _db.BooksCopies.AsQueryable().AnyAsync(b => b.Id == Id);
        }

    }
}
