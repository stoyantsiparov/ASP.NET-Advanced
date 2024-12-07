using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://daysym.com/wp-content/uploads/2024/01/dream-about-scuba-diving.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "hhttps://daysym.com/wp-content/uploads/2024/01/dream-about-scuba-diving.jpg");
        }
    }
}
