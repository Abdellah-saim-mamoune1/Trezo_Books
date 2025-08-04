using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.BookServicesInterfaces;
using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using EcommerceBackend.UtilityClasses;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EBookServices
{
    public class BookManagementService
        (
        
        AppDbContext _db,
         IBookRepository _Repo,
         IBookManagementValidationService _Validate
        ): IBookManagementService
    {

        public async Task<ApiResponseDto<object?>> CreateBookAsync(string ISBN)
        {
            var Errors = new List<ValidationErorrsDto>();
            using var httpClient = new HttpClient();
            var url = $"https://www.googleapis.com/books/v1/volumes?q=isbn:{ISBN}";

            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return UApiResponder<object>.Fail("Internal server error",
                    new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "GoogleAPI", Message = "GoogleAPI error." } }, 500);
            }

            var jsonString = await response.Content.ReadAsStringAsync();

            JsonDocument booksData;
            try
            {
                booksData = JsonDocument.Parse(jsonString);
            }
            catch (JsonException)
            {
                return UApiResponder<object>.Fail("Invalid JSON response from Google Books",
                    new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "GoogleAPI", Message = "Invalid JSON structure." } }, 500);
            }

            if (!booksData.RootElement.TryGetProperty("items", out var items) || items.ValueKind != JsonValueKind.Array || !items.EnumerateArray().Any())
            {
                return UApiResponder<object>.Fail("Book not found",
                    new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "ISBN", Message = "Book not found on Google Books." } }, 404);
            }

            var firstBook = items.EnumerateArray().First();

            if (!firstBook.TryGetProperty("volumeInfo", out var volumeInfo))
            {
                return UApiResponder<object>.Fail("Bad request",
                    new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "ISBN", Message = "Missing volumeInfo in book data." } }, 400);
            }

            // قراءة الخصائص بأمان
            var bookTitle = volumeInfo.TryGetProperty("title", out var titleElem) ? titleElem.GetString() : null;
            if (string.IsNullOrWhiteSpace(bookTitle))
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Title", Message = "Book title not found." });
            }

            var Categories = volumeInfo.TryGetProperty("categories", out var categoriesElem) && categoriesElem.ValueKind == JsonValueKind.Array
                ? categoriesElem.EnumerateArray().Select(c => c.GetString()).Where(c => !string.IsNullOrWhiteSpace(c)).ToList()
                : new List<string>();

            var authors = volumeInfo.TryGetProperty("authors", out var authorsElem) && authorsElem.ValueKind == JsonValueKind.Array
                ? authorsElem.EnumerateArray().Select(a => a.GetString()).Where(a => !string.IsNullOrWhiteSpace(a)).ToList()
                : new List<string>();

            var authorName = authors.FirstOrDefault() ?? "Unknown";
            var description = volumeInfo.TryGetProperty("description", out var desc) ? desc.GetString() : null;
            var imageUrl = volumeInfo.TryGetProperty("imageLinks", out var img) && img.TryGetProperty("thumbnail", out var thumb) ? thumb.GetString() : null;
            var pages = volumeInfo.TryGetProperty("pageCount", out var pageCount) && pageCount.TryGetInt32(out var pCount) ? pCount : 0;
            var publishedAt = volumeInfo.TryGetProperty("publishedDate", out var published) && DateOnly.TryParse(published.GetString(), out var pubDate)
                ? pubDate
                : DateOnly.FromDateTime(DateTime.Today);
            var averageRating = volumeInfo.TryGetProperty("averageRating", out var avgRatingElem) && avgRatingElem.TryGetDouble(out var avgRating)
                ? avgRating
                : 0.0;
            var ratingsCount = volumeInfo.TryGetProperty("ratingsCount", out var ratingsCountElem) && ratingsCountElem.TryGetInt32(out var rCount)
                ? rCount
                : 0;

            // فحص الازدواج
            var exists = await _db.Books
                .Include(b => b.Author)
                .AnyAsync(b => b.Name.ToLower() == bookTitle!.ToLower() && b.Author!.FullName.ToLower() == authorName.ToLower());

            // استخراج TypeId من التصنيفات
            var types = await _db.BooksTypes.ToDictionaryAsync(p => p.Name, p => p.Id);
            int TypeId = -1;
            foreach (var category in Categories)
            {
                if (types.TryGetValue(category, out var Id))
                {
                    TypeId = Id;
                    break;
                }
            }

            if (TypeId == -1)
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Category", Message = "Book category not found in system." });
            }

            if (exists)
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Book", Message = "Book already exists." });
            }

            if (Errors.Count != 0)
                return UApiResponder<object>.Fail("Invalid book data", Errors, 400);

            // إنشاء الكاتب
            var author = new Author { FullName = authorName };
            _db.Add(author);
            await _db.SaveChangesAsync();

            var book = new Book
            {
                AuthorId = author.Id,
                Name = bookTitle!,
                Description = description!,
                ImageUrl = imageUrl!,
                Pages = pages,
                TypeId = TypeId,
                PublishedAt = publishedAt,
                averageRating = (float)averageRating,
                ratingsCount = ratingsCount,
            };

            int BookId = await _Repo.Create(book);
            if (BookId == -1)
            {
                return UApiResponder<object>.Fail("Internal server error",
                    new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "Server", Message = "Failed to insert book into database." } }, 500);
            }

            return UApiResponder<object>.Success(BookId, "Book was created successfully.");
        }


        public async Task<ApiResponseDto<object?>> UpdateBookAsync(BookGetXUpdateDto book)
        {

            var Errors = await _Validate.ValidateUpdate(book);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information", Errors, 400);
            }

            if (await _Repo.Update(book) == ValidationStatus.Fail)
            {
                var ServerError = new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "Server", Message = "Internal server Error" } };
                return UApiResponder<object>.Fail("Internal server error", ServerError, 500);
            }

            return UApiResponder<object>.Success(null, "Book was updated successfully.");
        }

        public async Task<ApiResponseDto<object?>> DeleteBookAsync(int BookId)
        {
            var Errors = await _Validate.ValidateDelete(BookId);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information", Errors, 400);
            }

            if (await _Repo.Delete(BookId) == ValidationStatus.Fail)
            {
                var ServerError = new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "Server", Message = "Internal server Error" } };
                return UApiResponder<object>.Fail("Internal server error", ServerError, 500);
            }

            return UApiResponder<object>.Success(null, "Book was deleted successfully.");
        }

        public async Task<ApiResponseDto<object?>> GetPaginatedBooksAsync(PaginationFormDto Form)
        {
            var Errors =  _Validate.ValidateGetPaginated(Form);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            var Authors = await _Repo.GetPaginatedBooksAsync(Form);

            return UApiResponder<object>.Success(Authors, "Books were fetched successfully.");
        }

        public async Task<ApiResponseDto<object?>> GetBookByIdAsync(int Id)
        {

            var Book = await _Repo.GetBookByIdAsync(Id);

            return UApiResponder<object>.Success(Book, Book==null?"Book not found.":"Book was fetched successfully.");
        }

        public async Task<ApiResponseDto<object?>> GetBookByNameAsync(string Name)
        {

            var Book = await _Repo.GetBookByNameAsync(Name);

            return UApiResponder<object>.Success(Book, Book == null ? "Book not found." : "Book was fetched successfully.");
        }

    }
}
