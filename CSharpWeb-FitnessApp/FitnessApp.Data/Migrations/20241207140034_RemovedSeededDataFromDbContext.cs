using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSeededDataFromDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 4);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FitnessEvents",
                columns: new[] { "Id", "Description", "EndDate", "ImageUrl", "Location", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, "Join us for a thrilling 10K spring marathon through the city streets.", new DateTime(2025, 4, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), "https://www.chicagospringhalf.com/wp-content/uploads/sites/32/2023/05/2022_SPRCHI_RaceDay_Ali_282-1024x683.jpg", "Downtown City Center", new DateTime(2025, 4, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), "Spring City Marathon" },
                    { 2, "A challenging hike to the top of the mountain with stunning views.", new DateTime(2025, 7, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), "https://www.reserveamerica.com/articles/wp-content/uploads/2024/07/11174967-1e85-45df-8097-ac30b3bb1c34.jpg", "Rocky Mountain Trail", new DateTime(2025, 7, 15, 6, 0, 0, 0, DateTimeKind.Unspecified), "Mountain Peak Hike" },
                    { 3, "A scenic walk around the beautiful autumn lake. Perfect for relaxation and exercise.", new DateTime(2025, 10, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), "https://images.stockcake.com/public/c/a/0/ca09354d-17f0-4693-b9d3-fb2d399a07c1_large/autumn-lakeside-walk-stockcake.jpg", "Autumn Lake Park", new DateTime(2025, 10, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), "Autumn Lake Walk" }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "Bio", "FirstName", "ImageUrl", "LastName", "Specialization" },
                values: new object[,]
                {
                    { 1, "Natalie is a certified yoga instructor with over 10 years of experience. She is passionate about helping others achieve their fitness goals and improve their overall well-being.", "Natalie", "https://horizonweekly.ca/wp-content/uploads/2021/01/Nat-2.jpg", "Asatryan", "Yoga" },
                    { 2, "Warren is a certified personal trainer and fitness coach. He specializes in high-intensity interval training (HIIT) and enjoys helping clients push their limits and reach their full potential.", "Warren", "https://images.squarespace-cdn.com/content/v1/651489d366d19e59b7bbf9cf/a68428a6-992f-45a4-adfc-1b5a75e5cfda/Warren_square500.jpg", "Scott", "HIIT" },
                    { 3, "Emily is a certified Zumba instructor with a background in dance and fitness. She loves creating a fun and inclusive environment where everyone can enjoy the benefits of Zumba.", "Emily", "https://d29za44huniau5.cloudfront.net/uploads/2023/11/first-class-mobile.png", "Johnson", "Zumba" }
                });

            migrationBuilder.InsertData(
                table: "MembershipTypes",
                columns: new[] { "Id", "Description", "Duration", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "A basic membership that grants access to all regular classes and gym facilities.", 30, "https://i0.wp.com/poolstats.co/wp-content/uploads/2019/01/Basic-Membership.png?fit=400%2C327&ssl=1", "Basic", 59.99m },
                    { 2, "An elite membership offering access to all classes, gym facilities, and spa treatments.", 60, "https://cdn.vectorstock.com/i/500p/49/16/elite-gold-label-vector-2944916.jpg", "Elite", 99.99m },
                    { 3, "A premium membership offering access to all classes, gym facilities, and spa treatments.", 180, "https://thumbs.dreamstime.com/b/premium-membership-badge-stamp-golden-red-ribbon-text-30827692.jpg", "Premium", 299.99m },
                    { 4, "An exclusive membership with additional perks including priority booking for events and personal training.", 365, "https://cdn11.bigcommerce.com/s-2ooutu2zpl/images/stencil/original/products/35315/51564/VIP_Badge_2__62906.1641934958.png?c=2", "VIP", 499.99m }
                });

            migrationBuilder.InsertData(
                table: "SpaProcedures",
                columns: new[] { "Id", "AppointmentDateTime", "Description", "Duration", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A soothing massage to relieve tension and stress.", 60, "https://www.dshieldsusa.com/wp-content/uploads/2021/05/relaxing-massage-slide.jpg", "Relaxing Massage", 50.00m },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A rejuvenating facial to nourish and hydrate your skin.", 45, "https://spamd.net/wp-content/uploads/2022/03/medications-facial-treatments.jpg", "Facial Treatment", 40.00m },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A session using essential oils to promote relaxation and well-being.", 30, "https://elementsmassage.com/files/shared/AZ%20-%20Elements%20Massage%205-1864269.jpg", "Aromatherapy Session", 30.00m }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Description", "Duration", "ImageUrl", "InstructorId", "Name", "Price", "Schedule" },
                values: new object[,]
                {
                    { 1, "A calm and peaceful yoga session to start your day.", 60, "https://yogajala.com/wp-content/uploads/8-Benefits-Of-Morning-Yoga.jpg", 1, "Morning Yoga", 50.00m, new DateTime(2024, 12, 5, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "An intense, high-energy interval training session.", 45, "https://i.ytimg.com/vi/66_hHeSUrzU/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQjZQw/2.jpg", 2, "HIIT Challenge", 60.00m, new DateTime(2024, 12, 5, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "A fun and energetic Zumba dance class for all levels.", 60, "https://i.ytimg.com/vi/N3wBXogMYfM/hq720.jpg?sqp=-oaymwE7CK4FEIIDSFryq4qpAy0IARUAAAAAGAElAADIQj0AgKJD8AEB-AH-CYAC0AWKAgwIABABGGUgUihUMA8=&rs=AOn4CLD9yvCPKa7mHvL_lLUQr-TvnlNYRw", 3, "Zumba Dance", 40.00m, new DateTime(2024, 12, 6, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }
    }
}
