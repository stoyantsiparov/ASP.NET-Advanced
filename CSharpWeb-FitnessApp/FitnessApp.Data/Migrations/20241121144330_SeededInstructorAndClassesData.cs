using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeededInstructorAndClassesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "FirstName", "ImageUrl", "LastName", "Specialization" },
                values: new object[,]
                {
                    { 1, "Natalie", "https://horizonweekly.ca/wp-content/uploads/2021/01/Nat-2.jpg", "Asatryan", "Yoga" },
                    { 2, "Warren", "https://images.squarespace-cdn.com/content/v1/651489d366d19e59b7bbf9cf/a68428a6-992f-45a4-adfc-1b5a75e5cfda/Warren_square500.jpg", "Scott", "HIIT" },
                    { 3, "Emily", "https://d29za44huniau5.cloudfront.net/uploads/2023/11/first-class-mobile.png", "Johnson", "Zumba" }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Description", "Duration", "ImageUrl", "InstructorId", "Name", "Schedule" },
                values: new object[,]
                {
                    { 1, "A calm and peaceful yoga session to start your day.", 60, "https://yogajala.com/wp-content/uploads/8-Benefits-Of-Morning-Yoga.jpg", 1, "Morning Yoga", new DateTime(2024, 12, 5, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "An intense, high-energy interval training session.", 45, "https://i.ytimg.com/vi/66_hHeSUrzU/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLB88ucCUVHp_EFpv6T47y7oJRpRsQ", 2, "HIIT Challenge", new DateTime(2024, 12, 5, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "A fun and energetic Zumba dance class for all levels.", 60, "https://i.ytimg.com/vi/N3wBXogMYfM/hq720.jpg?sqp=-oaymwE7CK4FEIIDSFryq4qpAy0IARUAAAAAGAElAADIQj0AgKJD8AEB-AH-CYAC0AWKAgwIABABGGUgUihUMA8=&rs=AOn4CLD9yvCPKa7mHvL_lLUQr-TvnlNYRw", 3, "Zumba Party", new DateTime(2024, 12, 6, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
