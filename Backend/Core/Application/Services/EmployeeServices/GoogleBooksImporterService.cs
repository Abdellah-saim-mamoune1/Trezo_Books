using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.Infrastructure.Data;
using EcommerceBackend.WebAPI;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace EcommerceBackend.Core.Application.Services.EmployeeServices
{
    public class GoogleBooksImporterService
    {
        private readonly HttpClient _http;
        private readonly AppDbContext _db;

        public GoogleBooksImporterService(HttpClient http, AppDbContext db)
        {
            _http = http;
            _db = db;
        }

        public async Task ImportBooksAsync(string type)
        {
            int totalCount = 3000;
            int maxResults = 40;
            for (int startIndex = 0; startIndex < totalCount; startIndex += maxResults)
            {
                var url = $"https://www.googleapis.com/books/v1/volumes?q=subject:{type}&langRestrict=en&maxResults={maxResults}&startIndex={startIndex}";



                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Error fetching data from Google Books.");
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DEGetGoogleApi>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result?.Items == null || !await _db.BooksTypes.AnyAsync(b => b.Name == type))
                {
                    Console.WriteLine("No books found.");
                    return;
                }

                var Type = await _db.BooksTypes.FirstAsync(b => b.Name == type);

                var englishBooks = result.Items
                    .Where(i =>
            i.VolumeInfo != null &&
            !string.IsNullOrWhiteSpace(i.VolumeInfo.Title) &&
            DateOnly.TryParse(i.VolumeInfo.PublishedDate, out var date) &&
            i.VolumeInfo.Authors != null &&
            i.VolumeInfo.PageCount != 0 &&
            i.VolumeInfo.Authors.Any(a => !string.IsNullOrWhiteSpace(a)) &&
            !string.IsNullOrWhiteSpace(i.VolumeInfo.Language) &&
            i.VolumeInfo.Language == "en" &&
             !string.IsNullOrWhiteSpace(i.VolumeInfo.Description) &&
            i.VolumeInfo.ImageLinks != null &&
            !string.IsNullOrWhiteSpace(i.VolumeInfo.ImageLinks.SmallThumbnail)
        )
        .ToList();
                int count = 0;
                foreach (var item in englishBooks)
                {


                    if (count == 1000)
                        break;
                    var volume = item.VolumeInfo!;
                    if (await _db.Authors.AnyAsync(a => a.FullName == volume.Authors!.First()))
                        continue;

                    var author = new Author
                    {


                        FullName = volume.Authors?.FirstOrDefault() ?? "Unknown"

                    };

                    _db.Add(author);
                    await _db.SaveChangesAsync();
                    DateOnly.TryParse(volume.PublishedDate, out var date);
                    var book = new Book
                    {
                        Name = volume.Title!,
                        AuthorId = author.Id,
                        Description = volume!.Description!,
                        ImageUrl = volume!.ImageLinks!.SmallThumbnail!,
                        averageRating = volume.AverageRating,
                        ratingsCount = volume.RatingsCount,
                        Pages = volume.PageCount,
                        PublishedAt = date,
                        TypeId = Type.Id

                        // ضع الخصائص الأخرى مثل السعر أو النوع هنا إذا أردت
                    };

                    int randomQuantity = new Random().Next(1, 31);
                    int randomPrice = new Random().Next(40, 200);

                  
                    

                    _db.Books.Add(book);
                    count++;
                    await _db.SaveChangesAsync();

                    var bookcopy = new BookCopy
                    {
                        Price = randomPrice,
                        Quantity = randomQuantity,
                        IsAvailable = true,
                        BookId = book.Id,
                    };

                    _db.Add(bookcopy);
                    await _db.SaveChangesAsync();

                }
            }

           
        }
    }

}
