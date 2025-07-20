using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceBackend.Migrations
{
    /// <inheritdoc />
    public partial class fixingtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksCopiess_Bookss_BookId",
                table: "BooksCopiess");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookss_Authorss_AuthorId",
                table: "Bookss");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookss_BooksTypess_TypeId",
                table: "Bookss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BooksTypess",
                table: "BooksTypess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookss",
                table: "Bookss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BooksCopiess",
                table: "BooksCopiess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authorss",
                table: "Authorss");

            migrationBuilder.RenameTable(
                name: "BooksTypess",
                newName: "BooksTypes");

            migrationBuilder.RenameTable(
                name: "Bookss",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "BooksCopiess",
                newName: "BooksCopies");

            migrationBuilder.RenameTable(
                name: "Authorss",
                newName: "Authors");

            migrationBuilder.RenameIndex(
                name: "IX_Bookss_TypeId",
                table: "Books",
                newName: "IX_Books_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookss_AuthorId",
                table: "Books",
                newName: "IX_Books_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_BooksCopiess_BookId",
                table: "BooksCopies",
                newName: "IX_BooksCopies_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BooksTypes",
                table: "BooksTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BooksCopies",
                table: "BooksCopies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BooksTypes_TypeId",
                table: "Books",
                column: "TypeId",
                principalTable: "BooksTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BooksCopies_Books_BookId",
                table: "BooksCopies",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_BooksTypes_TypeId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_BooksCopies_Books_BookId",
                table: "BooksCopies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BooksTypes",
                table: "BooksTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BooksCopies",
                table: "BooksCopies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "BooksTypes",
                newName: "BooksTypess");

            migrationBuilder.RenameTable(
                name: "BooksCopies",
                newName: "BooksCopiess");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Bookss");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Authorss");

            migrationBuilder.RenameIndex(
                name: "IX_BooksCopies_BookId",
                table: "BooksCopiess",
                newName: "IX_BooksCopiess_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_TypeId",
                table: "Bookss",
                newName: "IX_Bookss_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorId",
                table: "Bookss",
                newName: "IX_Bookss_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BooksTypess",
                table: "BooksTypess",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BooksCopiess",
                table: "BooksCopiess",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookss",
                table: "Bookss",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authorss",
                table: "Authorss",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BooksCopiess_Bookss_BookId",
                table: "BooksCopiess",
                column: "BookId",
                principalTable: "Bookss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookss_Authorss_AuthorId",
                table: "Bookss",
                column: "AuthorId",
                principalTable: "Authorss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookss_BooksTypess_TypeId",
                table: "Bookss",
                column: "TypeId",
                principalTable: "BooksTypess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
