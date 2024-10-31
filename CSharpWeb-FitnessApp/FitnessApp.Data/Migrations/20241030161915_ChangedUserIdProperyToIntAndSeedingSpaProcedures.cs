using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserIdProperyToIntAndSeedingSpaProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SpaProcedures",
                columns: new[] { "Id", "Description", "Duration", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "A soothing massage to relieve tension and stress.", 60, 
	                    "https://www.dshieldsusa.com/wp-content/uploads/2021/05/relaxing-massage-slide.jpg", "Relaxing Massage", 50.00m },
                    { 2, "A rejuvenating facial to nourish and hydrate your skin.", 45, 
	                    "https://spamd.net/wp-content/uploads/2022/03/medications-facial-treatments.jpg", "Facial Treatment", 40.00m },
                    { 3, "A session using essential oils to promote relaxation and well-being.", 30, 
	                    "https://elementsmassage.com/files/shared/AZ%20-%20Elements%20Massage%205-1864269.jpg", "Aromatherapy Session", 30.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
