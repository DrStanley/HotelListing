using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListing.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "488fcc0f-e640-42c1-8d57-aac161b69447", "cdb80526-113c-4d4a-b382-92c0a1b8bb2c", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5d2603d9-db3c-4780-adb2-67380b8cb0ab", "44e8fdee-0103-4d2e-8d8d-0320bd67d921", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27786bc3-f197-4a4b-9f9a-fe6103913967", "d3483f09-4826-415f-b293-7cd9f4ad91f8", "FrontDesk", "FRONTDESK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27786bc3-f197-4a4b-9f9a-fe6103913967");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "488fcc0f-e640-42c1-8d57-aac161b69447");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d2603d9-db3c-4780-adb2-67380b8cb0ab");
        }
    }
}
