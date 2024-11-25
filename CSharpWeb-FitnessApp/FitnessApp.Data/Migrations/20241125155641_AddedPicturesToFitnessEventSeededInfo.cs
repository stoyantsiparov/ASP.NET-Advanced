using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPicturesToFitnessEventSeededInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.chicagospringhalf.com/wp-content/uploads/sites/32/2023/05/2022_SPRCHI_RaceDay_Ali_282-1024x683.jpg");

            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://www.reserveamerica.com/articles/wp-content/uploads/2024/07/11174967-1e85-45df-8097-ac30b3bb1c34.jpg");

            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://images.stockcake.com/public/c/a/0/ca09354d-17f0-4693-b9d3-fb2d399a07c1_large/autumn-lakeside-walk-stockcake.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://example.com/spring-marathon.jpg");

            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://example.com/mountain-hike.jpg");

            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://example.com/autumn-lake-walk.jpg");
        }
    }
}
