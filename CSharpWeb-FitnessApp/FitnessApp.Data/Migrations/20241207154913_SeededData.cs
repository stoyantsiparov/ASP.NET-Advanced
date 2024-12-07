using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FitnessEvents",
                columns: new[] { "Id", "Description", "EndDate", "ImageUrl", "Location", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, "Join us for a thrilling 10K spring marathon through the city streets.", new DateTime(2025, 4, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), "https://www.chicagospringhalf.com/wp-content/uploads/sites/32/2023/05/2022_SPRCHI_RaceDay_Ali_282-1024x683.jpg", "Downtown City Center", new DateTime(2025, 4, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), "Spring City Marathon" },
                    { 2, "A challenging hike to the top of the mountain with stunning views.", new DateTime(2025, 7, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), "https://www.reserveamerica.com/articles/wp-content/uploads/2024/07/11174967-1e85-45df-8097-ac30b3bb1c34.jpg", "Rocky Mountain Trail", new DateTime(2025, 7, 15, 6, 0, 0, 0, DateTimeKind.Unspecified), "Mountain Peak Hike" },
                    { 3, "A scenic walk around the beautiful autumn lake. Perfect for relaxation and exercise.", new DateTime(2025, 10, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), "https://images.stockcake.com/public/c/a/0/ca09354d-17f0-4693-b9d3-fb2d399a07c1_large/autumn-lakeside-walk-stockcake.jpg", "Autumn Lake Park", new DateTime(2025, 10, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), "Autumn Lake Walk" },
                    { 4, "A festive 5K run through a snowy winter park.", new DateTime(2025, 12, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), "https://cdn.shopify.com/s/files/1/0203/9788/3467/files/Craft_AW22_ADV_SubZ_Wool-LS-Tee_4_1024x1024.jpg?v=1695349527", "Snowy Pines Park", new DateTime(2025, 12, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "Winter Wonderland Run" },
                    { 5, "Relax and stretch with a peaceful yoga session on the beach.", new DateTime(2025, 6, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), "https://www.townofbethanybeach.com/ImageRepository/Document?documentID=7156", "Golden Sands Beach", new DateTime(2025, 6, 20, 7, 0, 0, 0, DateTimeKind.Unspecified), "Summer Beach Yoga" },
                    { 6, "Diving is the sport of jumping or falling into water from a platform or springboard, often with acrobatics. It is part of the Olympic Games and also enjoyed recreationally as a non-competitive activity.", new DateTime(2025, 9, 25, 16, 0, 0, 0, DateTimeKind.Unspecified), "hhttps://daysym.com/wp-content/uploads/2024/01/dream-about-scuba-diving.jpg", "Blue hole", new DateTime(2025, 9, 25, 12, 0, 0, 0, DateTimeKind.Unspecified), "Diving" }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "Bio", "FirstName", "ImageUrl", "LastName", "Specialization" },
                values: new object[,]
                {
                    { 1, "Natalie is a certified yoga instructor with over 10 years of experience. She is passionate about helping others achieve their fitness goals and improve their overall well-being.", "Natalie", "https://horizonweekly.ca/wp-content/uploads/2021/01/Nat-2.jpg", "Asatryan", "Yoga" },
                    { 2, "Warren is a certified personal trainer and fitness coach. He specializes in high-intensity interval training (HIIT) and enjoys helping clients push their limits and reach their full potential.", "Warren", "https://images.squarespace-cdn.com/content/v1/651489d366d19e59b7bbf9cf/a68428a6-992f-45a4-adfc-1b5a75e5cfda/Warren_square500.jpg", "Scott", "HIIT" },
                    { 3, "Emily is a certified Zumba instructor with a background in dance and fitness. She loves creating a fun and inclusive environment where everyone can enjoy the benefits of Zumba.", "Emily", "https://d29za44huniau5.cloudfront.net/uploads/2023/11/first-class-mobile.png", "Johnson", "Zumba" },
                    { 4, "Olivia is a certified Pilates instructor with a passion for helping individuals improve their core strength and flexibility.", "Olivia", "https://www.clubpilates.com/hubfs/Leah-Pfrommer-Club-Pilates-instructor-exclusive-interview-with-Athletech-News-1.jpg", "Williams", "Pilates" },
                    { 5, "Wolff is a certified strength training coach. He specializes in weightlifting and conditioning, helping clients build muscle and endurance.", "Wolff", "https://jwfitnesssystems.com/wp-content/uploads/2023/02/CW1_7335-scaled.jpg", "Jameson", "Strength Training" },
                    { 6, "Conor Anthony McGregor (born 14 July 1988) is an Irish professional mixed martial artist, professional boxer, businessman and actor. He is a former Ultimate Fighting Championship (UFC) Featherweight and Lightweight Champion, becoming the first UFC fighter to hold UFC championships in two weight classes simultaneously.", "Conor", "https://a.espncdn.com/i/headshots/mma/players/full/3022677.png", "McGregor", "ММА" }
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
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A session using essential oils to promote relaxation and well-being.", 30, "https://elementsmassage.com/files/shared/AZ%20-%20Elements%20Massage%205-1864269.jpg", "Aromatherapy Session", 30.00m },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A therapeutic massage using smooth, heated stones to ease tension.", 75, "https://images-prod.healthline.com/hlcmsresource/images/topic_centers/1296x728_HEADER_benefits-of-hot-stone-massage.jpg", "Hot Stone Massage", 70.00m },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A massage targeting deeper layers of muscle tissue to release chronic tension.", 60, "https://propelphysiotherapy.com/wp-content/uploads/2023/08/what-is-deep-tissue-massage-therapy-propel-physiotherapy.jpg", "Deep Tissue Massage", 60.00m },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A detoxifying wrap using nutrient-rich seaweed to revitalize your skin.", 90, "https://s3.amazonaws.com/salonclouds-uploads/blog/blog_1605466361125864114.png", "Seaweed Body Wrap", 85.00m }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Description", "Duration", "ImageUrl", "InstructorId", "Name", "Price", "Schedule" },
                values: new object[,]
                {
                    { 1, "A calm and peaceful yoga session to start your day.", 60, "https://yogajala.com/wp-content/uploads/8-Benefits-Of-Morning-Yoga.jpg", 1, "Morning Yoga", 50.00m, new DateTime(2024, 12, 5, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "An intense, high-energy interval training session.", 45, "https://i.ytimg.com/vi/66_hHeSUrzU/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQjZQw/2.jpg", 2, "HIIT Challenge", 50.00m, new DateTime(2024, 12, 5, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "A fun and energetic Zumba dance class for all levels.", 60, "https://i.ytimg.com/vi/N3wBXogMYfM/hq720.jpg?sqp=-oaymwE7CK4FEIIDSFryq4qpAy0IARUAAAAAGAElAADIQj0AgKJD8AEB-AH-CYAC0AWKAgwIABABGGUgUihUMA8=&rs=AOn4CLD9yvCPKa7mHvL_lLUQr-TvnlNYRw", 3, "Zumba Dance", 90.00m, new DateTime(2024, 12, 6, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Strengthen your core and improve posture with this Pilates class.", 60, "https://media.self.com/photos/5b9c24c208e0b96633983ce8/2:1/w_2580,c_limit/pilates-butt-core-workout.jpg", 4, "Pilates Core", 85.00m, new DateTime(2024, 12, 7, 8, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "An introductory strength training session focusing on weightlifting techniques.", 45, "https://www.jefit.com/_next/image?url=https%3A%2F%2Fcdn.jefit.com%2Fuc%2Ffile%2Fc34238b8cd6e3cf7%2F1.jpg&w=3840&q=75", 5, "Strength Training Basics", 95.00m, new DateTime(2024, 12, 7, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Learn the basics of ММА in this high-energy and engaging class.", 30, "https://mf.b37mrtl.ru/rbthmedia/images/2018.02/article/5a93bf3385600a57b0096f7e.jpg", 6, "ММА Essentials", 150.00m, new DateTime(2024, 12, 8, 20, 0, 0, 0, DateTimeKind.Unspecified) }
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
                table: "Classes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 6);

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
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FitnessEvents",
                keyColumn: "Id",
                keyValue: 6);

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
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 6);

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

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
