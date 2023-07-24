using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace G_ToDo.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { "closed", "Close" },
                    { "open", "Open" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: "closed");

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: "open");
        }
    }
}
