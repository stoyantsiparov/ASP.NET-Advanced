using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedFitnessEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FitnessEvents",
                columns: new[] { "Id", "Description", "EndDate", "ImageUrl", "Location", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, "A calm and peaceful yoga session to start your day.", new DateTime(2024, 12, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "https://yogajala.com/wp-content/uploads/8-Benefits-Of-Morning-Yoga.jpg", "Gym 1", new DateTime(2024, 12, 5, 7, 0, 0, 0, DateTimeKind.Unspecified), "Morning Yoga" },
                    { 2, "An intense, high-energy interval training session.", new DateTime(2024, 12, 5, 19, 0, 0, 0, DateTimeKind.Unspecified), "https://i.ytimg.com/vi/66_hHeSUrzU/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLB88ucCUVHp_EFpv6T47y7oJRpRsQ", "Gym 2", new DateTime(2024, 12, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "HIIT Challenge" },
                    { 3, "A fun and energetic Zumba dance class for all levels.", new DateTime(2024, 12, 6, 11, 0, 0, 0, DateTimeKind.Unspecified), "https://i.ytimg.com/vi/N3wBXogMYfM/hq720.jpg?sqp=-oaymwE7CK4FEIIDSFryq4qpAy0IARUAAAAAGAElAADIQj0AgKJD8AEB-AH-CYAC0AWKAgwIABABGGUgUihUMA8=&rs=AOn4CLD9yvCPKa7mHvL_lLUQr-TvnlNYRw", "Gym 3", new DateTime(2024, 12, 6, 10, 0, 0, 0, DateTimeKind.Unspecified), "Zumba Party" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
