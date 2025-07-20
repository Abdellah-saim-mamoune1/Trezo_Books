using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.Core.Domain.Models.CartModels;
using EcommerceBackend.Core.Domain.Models.ClientModels;
using EcommerceBackend.Core.Domain.Models.ClientXEmployeeModels;
using EcommerceBackend.Core.Domain.Models.EmployeeModels;
using EcommerceBackend.Core.Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAccount> EmployeeAccount { get; set; }
        public DbSet<ClientAccount> ClientsAccounts { get; set; } 
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<EmployeeAccountType> EmployeeAccountTypes { get; set; } 
        public DbSet<CartItems> Carts { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookType> BooksTypes { get; set; }
        public DbSet<BookCopy> BooksCopies { get; set; }
        public DbSet<Wishlist> WishesLists { get; set; }
        public DbSet<BookCopyRating> BookCopyRatings { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Client)
                .WithOne(c => c.Person)
                .HasForeignKey<Client>(c => c.PersonId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Person>()
                  .HasOne(p => p.Employee)
                  .WithOne(c => c.Person)
                  .HasForeignKey<Employee>(c => c.PersonId)
                  .OnDelete(DeleteBehavior.Restrict);

       

            modelBuilder.Entity<Employee>()
                .HasOne(p => p.EmployeeAccount)
                .WithOne(c => c.Employee)
                .HasForeignKey<Employee>(c => c.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

         
            modelBuilder.Entity<CartItems>()
.HasOne(p => p.client)
.WithMany(c => c.Carts)
.HasForeignKey(c => c.ClientId)
.OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<OrderItem>()
.HasOne(p => p.order)
.WithMany(c => c.OrderItems)
.HasForeignKey(c => c.OrderId)
.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
.HasOne(p => p.client)
.WithMany(c => c.Orders)
.HasForeignKey(c => c.ClientId)
.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
.HasOne(p => p.Author)
.WithMany(c => c.Books)
.HasForeignKey(c => c.AuthorId)
.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
.HasOne(p => p.BookType)
.WithMany(c => c.Books)
.HasForeignKey(c => c.TypeId)
.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookCopy>()
.HasOne(p => p.Book)
.WithOne(c => c.BookCopies)
.HasForeignKey<BookCopy>(c => c.BookId)
.OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Wishlist>()
.HasOne(p => p.Client)
.WithMany(c => c.Wishlist)
.HasForeignKey(c => c.ClientId)
.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wishlist>()
.HasOne(p => p.BookCopy)
.WithMany(c => c.Wishlist)
.HasForeignKey(c => c.BookCopyId)
.OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<BookCopyRating>()
.HasOne(p => p.Client)
.WithMany(c => c.BookCopyRatings)
.HasForeignKey(c => c.ClientId)
.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookCopyRating>()
.HasOne(p => p.BookCopy)
.WithMany(c => c.BookCopyRatings)
.HasForeignKey(c => c.BookCopyId)
.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClientAccount>()
.Property(l => l.CreatedAt)
.HasDefaultValueSql("GETDATE()");


            modelBuilder.Entity<EmployeeAccount>()
    .Property(l => l.CreatedAt)
    .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Order>()
   .Property(l => l.CreatedAt)
   .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<OrderItem>()
.Property(l => l.CreatedAt)
.HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<CartItems>()
.Property(l => l.CreatedAt)
.HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<BookCopy>()
.Property(l => l.CreatedAt)
.HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ContactUs>()
.Property(l => l.CreatedAt)
.HasDefaultValueSql("GETDATE()");

        }
    }
}
