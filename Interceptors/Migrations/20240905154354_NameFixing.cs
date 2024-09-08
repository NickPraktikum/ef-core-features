using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interceptors.Migrations
{
    /// <inheritdoc />
    public partial class NameFixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UX_BookEntitys_Isbn",
                table: "Books",
                newName: "UX_BookEntities_Isbn");

            migrationBuilder.RenameIndex(
                name: "IX_BookEntitys_Title",
                table: "Books",
                newName: "IX_BookEntities_Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UX_BookEntities_Isbn",
                table: "Books",
                newName: "UX_BookEntitys_Isbn");

            migrationBuilder.RenameIndex(
                name: "IX_BookEntities_Title",
                table: "Books",
                newName: "IX_BookEntitys_Title");
        }
    }
}
