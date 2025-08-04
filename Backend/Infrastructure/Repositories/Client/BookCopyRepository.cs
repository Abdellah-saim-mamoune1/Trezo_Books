using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.CBookTypeDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace EcommerceBackend.Infrastructure.Repositories.ClientRepositories
{
    public class BookCopyRepository(

        AppDbContext _db,
        IMemoryCache _Cache
        ): IBookCopyRepository
    {
        private IQueryable<BookCopy> GetBookCopiesQueryable()
        {
            return _db.BooksCopies.AsQueryable();
        }

        public async Task<List<GetInitialBooksCopiesDataPerTypeDto>> GetInitialBooksCopiesDataPerTypeAsync()
        {

            var Types = await _db.BooksTypes.Select(b => b.Name).ToListAsync();
            List<GetInitialBooksCopiesDataPerTypeDto> InitialBooksCopiesData = new();
            foreach (var Type in Types)
            {
                var BooksCopies = await GetBookCopiesQueryable().Include(p => p.Book).Where(b=>b.IsAvailable!=false&&b.Quantity!=0).
                    Select(b => new DCGetInitialBookCopyData
                    {
                        Id = b.Id,
                        Name = b.Book!.Name,
                        ImageUrl = b.Book.ImageUrl,
                        Price = b.Price
                    })
                .Take(15)
                .ToListAsync();

                InitialBooksCopiesData.Add(new GetInitialBooksCopiesDataPerTypeDto { Type = Type, BooksCopies = BooksCopies });
            }

            return InitialBooksCopiesData;

        }

        public async Task<GetInitialPaginatedBooksCopiesDataByTypeDto> GetPaginatedBooksCopiesByTypeAsync(GetPaginatedBooksTypesDto form)
        {
            var allBooksCopiesQueryable = GetBookCopiesQueryable();
            var typeId =await _db.BooksTypes.Where(t => t.Name == form.Type).Select(t=>t.Id).FirstAsync();
            var BooksCopies = await allBooksCopiesQueryable.Include(b => b.Book).ThenInclude(b => b!.BookType).Include(b => b.Book!.Author).Where(b => b.Book!.BookType!.Name == form.Type)
                .Where(b => b.IsAvailable != false && b.Quantity != 0).Select(b => new DCGetInitialBookCopyData
                {
                    Id = b.Id,
                    Name = b.Book!.Name,
                    ImageUrl = b.Book.ImageUrl,
                    Price = b.Price,
                    AuthorName = b.Book!.Author!.FullName
                })
                .Skip((form.pageNumber - 1) * form.pageSize)
                .Take(form.pageSize)
                .ToListAsync();

            var totalCount = await allBooksCopiesQueryable.Include(b=>b.Book).Where(b=>b.Book!.TypeId==typeId).CountAsync();

            return new GetInitialPaginatedBooksCopiesDataByTypeDto
            {
                PageSize = BooksCopies.Count,
                PageNumber = form.pageNumber,
                Quantity = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / form.pageSize),
                BooksCopies = BooksCopies
            };
        }


        public async Task<List<DCGetInitialBookCopyData>> GetInitialRecommendedBooksCopiesDataAsync()
        {
            if(_Cache.TryGetValue("InitialRecommendedBooksCopiesData",out List<DCGetInitialBookCopyData>? BooksCopies))
            {
                return BooksCopies!;
            }
                  BooksCopies = await GetBookCopiesQueryable().Include(p => p.Book).ThenInclude(b=>b!.Author)
                     .OrderBy(b => Guid.NewGuid())
                     .Where(b => b.IsAvailable != false && b.Quantity != 0).Select(b => new DCGetInitialBookCopyData
                    {
                        Id=b.Id,
                        Name = b.Book!.Name,
                        ImageUrl = b.Book.ImageUrl,
                        Price = b.Price,
                        AuthorName = b.Book!.Author!.FullName
                     })
                    .Take(15)      
                    .ToListAsync();

            _Cache.Set<List<DCGetInitialBookCopyData>> ( "InitialRecommendedBooksCopiesData",BooksCopies);

            return BooksCopies;
        }

        public async Task<List<DCGetInitialBookCopyData>> GetInitialTopRatedBooksCopiesDataAsync()
        {
            if (_Cache.TryGetValue("InitialTopRatedBooksCopiesData", out List<DCGetInitialBookCopyData>? BooksCopies))
            {
                return BooksCopies!;
            }
              BooksCopies = await _db.Books.AsQueryable().Where(k => k.BookCopies != null).OrderByDescending(k=>k.ratingsCount)
                .Include(b => b!.Author).Include(k => k.BookCopies)
                  .Where(b => b.BookCopies!.IsAvailable != false && b.BookCopies.Quantity != 0)
                 .Select(b => new DCGetInitialBookCopyData
                  {
                      Id = b.BookCopies!.Id,
                      Name = b.Name,
                      ImageUrl = b.ImageUrl,
                      Price = b.BookCopies.Price,
                      AuthorName = b.Author!.FullName
                  })
                .Take(15)
                .ToListAsync();

            _Cache.Set<List<DCGetInitialBookCopyData>>("InitialTopRatedBooksCopiesData", BooksCopies);

            return BooksCopies;
        }

        public async Task<List<DCGetInitialBookCopyData>> GetInitialNewReleasesBooksCopiesDataAsync()
        {
            if (_Cache.TryGetValue("InitialNewReleasesBooksCopiesData", out List<DCGetInitialBookCopyData>? BooksCopies))
            {
                return BooksCopies!;
            }

            var booksCopies = await _db.BooksCopies.AsQueryable()
          .Where(b => b.Book != null && b.Book.Author != null)
           .Where(b => b.IsAvailable != false && b.Quantity != 0).Select(b => new
             {
            b.Id,
            b.Price,
            BookName = b.Book!.Name,
            b.Book.ImageUrl,
            b.Book.PublishedAt,
            AuthorName = b.Book.Author!.FullName
          })
          .OrderByDescending(x => x.PublishedAt)
          .Take(15)
          .ToListAsync();

            BooksCopies=booksCopies.Select(b => new DCGetInitialBookCopyData
            {
                Id = b.Id,
                Name = b.BookName,
                ImageUrl = b.ImageUrl,
                Price = b.Price,
                AuthorName = b.AuthorName
            }).ToList();

            _Cache.Set<List<DCGetInitialBookCopyData>>("InitialNewReleasesBooksCopiesData", BooksCopies);

            return BooksCopies;

        }

        public async Task<List<DCGetInitialBookCopyData>> GetInitialBestSellersBooksCopiesDataAsync()
        {

            if (_Cache.TryGetValue("InitialBestSellersBooksCopiesData", out List<DCGetInitialBookCopyData>? BooksCopies))
            {
                return BooksCopies!;
            }

            var topBookCopies = await _db.OrderItems.AsQueryable()
              .GroupBy(oi => oi.BookCopyId)
              .Select(g => new
              {
                 BookCopyId = g.Key,
                 Number = g.Count()
              })
              .OrderByDescending(g => g.Number)
              .Take(15)
              .ToListAsync();
            var bookCopyIds = topBookCopies.Select(x => x.BookCopyId).ToList();

               BooksCopies = await GetBookCopiesQueryable()
                .Include(p => p.Book).ThenInclude(b=>b!.Author)
                .Where(b => bookCopyIds.Contains(b.Id)&&b.IsAvailable != false && b.Quantity != 0)
                .Select(b => new DCGetInitialBookCopyData
                {
                    Id = b.Id,
                    Name = b.Book!.Name,
                    ImageUrl = b.Book.ImageUrl,
                    Price = b.Price,
                    AuthorName = b.Book!.Author!.FullName
                })
                .ToListAsync();

            _Cache.Set<List<DCGetInitialBookCopyData>>("InitialBestSellersBooksCopiesData", BooksCopies);

            return BooksCopies;
        }

        public async Task<GetBookCopyDto> GetBookCopyInfoByIdAsync(int Id)
        {
            var bookCopy = await _db.BooksCopies.AsQueryable().Include(b => b.Book)
                 .Include(b => b.Book!.BookType).Include(b => b.Book!.Author)
                 .Where(b=>b.Id==Id&& b.IsAvailable != false && b.Quantity != 0).Select(b => new GetBookCopyDto
                 {
                     Id=b.Id,
                     Name=b!.Book!.Name,
                     ImageUrl=b.Book.ImageUrl,
                     Category=b.Book!.BookType!.Name,
                     Author = b.Book!.Author!.FullName,
                     Description = b.Book!.Description,
                     Price= b.Price,
                     Quantity=b.Quantity,
                     AverageRating=b.Book.averageRating,
                     RatingsCount=b.Book.ratingsCount
                 }).FirstAsync();

            return bookCopy;
        }


        public async Task<List<GetBookCopyDto>> GetBooksCopiesInfoByNameAsync(string Name)
        {
            var booksCopies = await _db.BooksCopies.AsQueryable().Include(b => b.Book)
                 .Include(b => b.Book!.BookType).Include(b => b.Book!.Author)
                 .Where(b => b.Book!.Name.ToLower() == Name.ToLower()&& b.IsAvailable != false && b.Quantity != 0).Select(b => new GetBookCopyDto
                 {
                     Id = b.Id,
                     Name = b!.Book!.Name,
                     ImageUrl = b.Book.ImageUrl,
                     Category = b.Book!.BookType!.Name,
                     Author = b.Book!.Author!.FullName,
                     Description = b.Book!.Description,
                     Price = b.Price,
                     Quantity = b.Quantity,
                     AverageRating = b.Book.averageRating,
                     RatingsCount = b.Book.ratingsCount
                 }).ToListAsync();

            return booksCopies;
        }


        public async Task<List<GetBooksTypesDto>> GetBooksTypesAsync()
        {
            return await _db.BooksTypes.Select(b => new GetBooksTypesDto
            {
               Id= b.Id,
               Name= b.Name
            }).ToListAsync();
        }
    }
}
