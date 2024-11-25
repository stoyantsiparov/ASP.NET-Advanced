using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFitnessEventSeededInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "EndDate", "ImageUrl", "Location", "StartDate", "Title" },
                values: new object[] { "Join us for a thrilling 10K spring marathon through the city streets.", new DateTime(2025, 4, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/spring-marathon.jpg", "Downtown City Center", new DateTime(2025, 4, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), "Spring City Marathon" });

            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "EndDate", "ImageUrl", "Location", "StartDate", "Title" },
                values: new object[] { "A challenging hike to the top of the mountain with stunning views.", new DateTime(2025, 7, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/mountain-hike.jpg", "Rocky Mountain Trail", new DateTime(2025, 7, 15, 6, 0, 0, 0, DateTimeKind.Unspecified), "Mountain Peak Hike" });

            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "EndDate", "ImageUrl", "Location", "StartDate", "Title" },
                values: new object[] { "A scenic walk around the beautiful autumn lake. Perfect for relaxation and exercise.", new DateTime(2025, 10, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/autumn-lake-walk.jpg", "Autumn Lake Park", new DateTime(2025, 10, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), "Autumn Lake Walk" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "EndDate", "ImageUrl", "Location", "StartDate", "Title" },
                values: new object[] { "A calm and peaceful yoga session to start your day.", new DateTime(2024, 12, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "https://yogajala.com/wp-content/uploads/8-Benefits-Of-Morning-Yoga.jpg", "Gym 1", new DateTime(2024, 12, 5, 7, 0, 0, 0, DateTimeKind.Unspecified), "Morning Yoga" });

            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "EndDate", "ImageUrl", "Location", "StartDate", "Title" },
                values: new object[] { "An intense, high-energy interval training session.", new DateTime(2024, 12, 5, 19, 0, 0, 0, DateTimeKind.Unspecified), "https://i.ytimg.com/vi/66_hHeSUrzU/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLB88ucCUVHp_EFpv6T47y7oJRpRsQ", "Gym 2", new DateTime(2024, 12, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "HIIT Challenge" });

            migrationBuilder.UpdateData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "EndDate", "ImageUrl", "Location", "StartDate", "Title" },
                values: new object[] { "A fun and energetic Zumba dance class for all levels.", new DateTime(2024, 12, 6, 11, 0, 0, 0, DateTimeKind.Unspecified), "https://i.ytimg.com/vi/N3wBXogMYfM/hq720.jpg?sqp=-oaymwE7CK4FEIIDSFryq4qpAy0IARUAAAAAGAElAADIQj0AgKJD8AEB-AH-CYAC0AWKAgwIABABGGUgUihUMA8=&rs=AOn4CLD9yvCPKa7mHvL_lLUQr-TvnlNYRw", "Gym 3", new DateTime(2024, 12, 6, 10, 0, 0, 0, DateTimeKind.Unspecified), "Zumba Party" });
        }
    }
}
