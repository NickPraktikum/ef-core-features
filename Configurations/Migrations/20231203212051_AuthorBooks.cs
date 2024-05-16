using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Experiments.Migrations
{
    /// <inheritdoc />
    public partial class AuthorBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authors_Title",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "UX_Authors_Isbn",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "Pages",
                table: "Authors",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "Isbn",
                table: "Authors",
                newName: "SecondName");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "BirthDate",
                table: "Authors",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Authors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: true),
                    Isbn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Book",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "UX_Books_Isbn",
                table: "Book",
                column: "Isbn",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "Authors",
                newName: "Isbn");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Authors",
                newName: "Pages");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Authors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Title",
                table: "Authors",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "UX_Authors_Isbn",
                table: "Authors",
                column: "Isbn",
                unique: true);
        }
    }
}
