using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Migrations
{
    public partial class SeedData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "PlaceId", "UserName", "UserRating" },
                values: new object[] { 1, 1, "Josh", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1);
        }
    }
}
