using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedBioPropToInstructorAndSeededTheBio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Instructors",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "",
                comment: "Biography of the fitness instructor");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Bio",
                value: "Natalie is a certified yoga instructor with over 10 years of experience. She is passionate about helping others achieve their fitness goals and improve their overall well-being.");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Bio",
                value: "Warren is a certified personal trainer and fitness coach. He specializes in high-intensity interval training (HIIT) and enjoys helping clients push their limits and reach their full potential.");

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Bio",
                value: "Emily is a certified Zumba instructor with a background in dance and fitness. She loves creating a fun and inclusive environment where everyone can enjoy the benefits of Zumba.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Instructors");
        }
    }
}
