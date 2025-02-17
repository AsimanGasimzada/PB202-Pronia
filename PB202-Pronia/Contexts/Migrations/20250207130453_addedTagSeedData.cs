using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PB202_Pronia.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class addedTagSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[] { -1, "testpb202" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
