using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceBackend.Migrations
{
    /// <inheritdoc />
    public partial class changingthings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Authors",
                newName: "FullName");

            migrationBuilder.AddColumn<float>(
                name: "averageRating",
                table: "Books",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ratingsCount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "averageRating",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ratingsCount",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Authors",
                newName: "LastName");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "Authors",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
