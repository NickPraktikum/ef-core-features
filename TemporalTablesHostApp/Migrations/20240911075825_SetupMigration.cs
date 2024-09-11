using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemporalTablesHostApp.Migrations
{
    /// <inheritdoc />
    public partial class SetupMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "TodosHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation"),
                    Todo = table.Column<string>(type: "nvarchar(max)", maxLength: 100, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "TodosHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation"),
                    BookCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "TodosHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation"),
                    BookRemoval = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "TodosHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "TodosHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "TodosHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "BookRemoval")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "BookCreation");
        }
    }
}
