using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceBackend.Migrations
{
    /// <inheritdoc />
    public partial class updatingthetoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Clients_ClientPersonId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_ClientPersonId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_ClientsAccounts_TokenId",
                table: "ClientsAccounts");

            migrationBuilder.DropColumn(
                name: "ClientPersonId",
                table: "Tokens");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsAccounts_TokenId",
                table: "ClientsAccounts",
                column: "TokenId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClientsAccounts_TokenId",
                table: "ClientsAccounts");

            migrationBuilder.AddColumn<int>(
                name: "ClientPersonId",
                table: "Tokens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_ClientPersonId",
                table: "Tokens",
                column: "ClientPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsAccounts_TokenId",
                table: "ClientsAccounts",
                column: "TokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Clients_ClientPersonId",
                table: "Tokens",
                column: "ClientPersonId",
                principalTable: "Clients",
                principalColumn: "PersonId");
        }
    }
}
