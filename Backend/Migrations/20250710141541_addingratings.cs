using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceBackend.Migrations
{
    /// <inheritdoc />
    public partial class addingratings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookCopyRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    BookCopyId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopyRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCopyRatings_BooksCopies_BookCopyId",
                        column: x => x.BookCopyId,
                        principalTable: "BooksCopies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookCopyRatings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WishesLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    BookCopyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishesLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishesLists_BooksCopies_BookCopyId",
                        column: x => x.BookCopyId,
                        principalTable: "BooksCopies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WishesLists_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCopyRatings_BookCopyId",
                table: "BookCopyRatings",
                column: "BookCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopyRatings_ClientId",
                table: "BookCopyRatings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WishesLists_BookCopyId",
                table: "WishesLists",
                column: "BookCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_WishesLists_ClientId",
                table: "WishesLists",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCopyRatings");

            migrationBuilder.DropTable(
                name: "WishesLists");
        }
    }
}
