using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PB202_Pronia.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class addedAdminRoleSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ccfdb7ca-6ada-411d-be54-196fa5a390e7", "7c60df9f-e193-41fb-97ca-a7669fb2faca" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ccfdb7ca-6ada-411d-be54-196fa5a390e7", "7c60df9f-e193-41fb-97ca-a7669fb2faca" });
        }
    }
}
