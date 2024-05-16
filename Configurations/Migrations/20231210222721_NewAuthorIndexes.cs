using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Experiments.Migrations
{
    /// <inheritdoc />
    public partial class NewAuthorIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Author_FirstName",
                table: "Authors",
                column: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Author_FirstName",
                table: "Authors");
        }
    }
}
