using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PB202_Pronia.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class addedRoleSeedDatas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34cf98c4-6752-4334-a41a-dcd6cfa09cd4", null, "Member", "MEMBER" },
                    { "78dbc786-5226-4028-9772-7b17eee997ea", null, "Moderator", "MODERATOR" },
                    { "ccfdb7ca-6ada-411d-be54-196fa5a390e7", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7c60df9f-e193-41fb-97ca-a7669fb2faca", 0, "37e364e2-f7fb-4593-8d64-1c3f71104fb3", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEFsNZBh+epZl9PtcEWTQ/RFyKmqg78J8CtETfv6IbL8U0KzZX/u0I38YSTzTcZRLJA==", null, false, "NCSJXLWMZICDXVVDECA6D6YO74BI64A2", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34cf98c4-6752-4334-a41a-dcd6cfa09cd4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78dbc786-5226-4028-9772-7b17eee997ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ccfdb7ca-6ada-411d-be54-196fa5a390e7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c60df9f-e193-41fb-97ca-a7669fb2faca");

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[] { -1, "testpb202" });
        }
    }
}
