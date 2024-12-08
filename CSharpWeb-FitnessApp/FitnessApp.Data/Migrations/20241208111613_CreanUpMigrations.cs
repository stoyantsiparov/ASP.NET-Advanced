using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreanUpMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FitnessEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Title of the fitness event"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Image URL of the fitness class"),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false, comment: "Description of the fitness event"),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Location of the fitness event"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Start date of the fitness event"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "End date of the fitness event")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "First name of the fitness instructor"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Last name of the fitness instructor"),
                    Bio = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false, comment: "Biography of the fitness instructor"),
                    Specialization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Specialization of the fitness instructor"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Image URL of the fitness instructor")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Membership type"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Price of the membership type"),
                    Duration = table.Column<int>(type: "int", nullable: false, comment: "Duration of the membership type in days"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Image URL of the membership type"),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false, comment: "Description of the membership type")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpaProcedures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Spa type"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Image URL of the spa procedure"),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false, comment: "Description of the spa procedure"),
                    Duration = table.Column<int>(type: "int", nullable: false, comment: "Duration of the spa procedure in minutes"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Price of the spa procedure"),
                    AppointmentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Appointment date and time for the spa service")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaProcedures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventRegistrations",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistrations", x => new { x.EventId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_EventRegistrations_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRegistrations_FitnessEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "FitnessEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Name of the fitness class"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Image URL of the fitness class"),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false, comment: "Description of the fitness class"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Price for the fitness class"),
                    Schedule = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time of the fitness class"),
                    Duration = table.Column<int>(type: "int", nullable: false, comment: "Duration of the fitness class in minutes"),
                    InstructorId = table.Column<int>(type: "int", nullable: false, comment: "Instructor of the fitness class")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipRegistrations",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MembershipTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipRegistrations", x => new { x.MembershipTypeId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_MembershipRegistrations_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembershipRegistrations_MembershipTypes_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalTable: "MembershipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpaRegistrations",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpaProcedureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaRegistrations", x => new { x.SpaProcedureId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_SpaRegistrations_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpaRegistrations_SpaProcedures_SpaProcedureId",
                        column: x => x.SpaProcedureId,
                        principalTable: "SpaProcedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassesRegistrations",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassesRegistrations", x => new { x.ClassId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_ClassesRegistrations_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassesRegistrations_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    { 6, "Diving is the sport of jumping or falling into water from a platform or springboard, often with acrobatics. It is part of the Olympic Games and also enjoyed recreationally as a non-competitive activity.", new DateTime(2025, 9, 25, 16, 0, 0, 0, DateTimeKind.Unspecified), "https://daysym.com/wp-content/uploads/2024/01/dream-about-scuba-diving.jpg", "Blue hole", new DateTime(2025, 9, 25, 12, 0, 0, 0, DateTimeKind.Unspecified), "Diving" }
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_InstructorId",
                table: "Classes",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassesRegistrations_MemberId",
                table: "ClassesRegistrations",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_MemberId",
                table: "EventRegistrations",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipRegistrations_MemberId",
                table: "MembershipRegistrations",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaRegistrations_MemberId",
                table: "SpaRegistrations",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClassesRegistrations");

            migrationBuilder.DropTable(
                name: "EventRegistrations");

            migrationBuilder.DropTable(
                name: "MembershipRegistrations");

            migrationBuilder.DropTable(
                name: "SpaRegistrations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "FitnessEvents");

            migrationBuilder.DropTable(
                name: "MembershipTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SpaProcedures");

            migrationBuilder.DropTable(
                name: "Instructors");
        }
    }
}
