using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemporalTable.Migrations
{
    /// <inheritdoc />
    public partial class SetupMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "AuthorsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "AuthorRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "AuthorCreation"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "AuthorsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "AuthorRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "AuthorCreation"),
                    SecondName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "AuthorsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "AuthorRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "AuthorCreation"),
                    Age = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "AuthorsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "AuthorRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "AuthorCreation"),
                    BirthDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "AuthorsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "AuthorRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "AuthorCreation"),
                    AuthorCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "AuthorsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "AuthorRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "AuthorCreation"),
                    AuthorRemoval = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "AuthorsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "AuthorRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "AuthorCreation")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "AuthorsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "AuthorRemoval")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "AuthorCreation");

            migrationBuilder.CreateTable(
                name: "BookEntity",
                columns: table => new
                {
                    BookId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BookEntityHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BookEntityHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation"),
                    Pages = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BookEntityHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation"),
                    AuthorEntityAuthorId = table.Column<long>(type: "bigint", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BookEntityHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation"),
                    BookCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BookEntityHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation"),
                    BookRemoval = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BookEntityHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation"),
                    Isbn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BookEntityHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookEntity", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BookEntity_Authors_AuthorEntityAuthorId",
                        column: x => x.AuthorEntityAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId");
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "BookEntityHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation");

            migrationBuilder.CreateIndex(
                name: "IX_Author_FirstName",
                table: "Authors",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_BookEntity_AuthorEntityAuthorId",
                table: "BookEntity",
                column: "AuthorEntityAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "BookEntity",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "UX_Books_Isbn",
                table: "BookEntity",
                column: "Isbn",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookEntity")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "BookEntityHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation");

            migrationBuilder.DropTable(
                name: "Authors")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "AuthorsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "AuthorRemoval")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "AuthorCreation");
        }
    }
}
