using System.Reflection.Emit;
using FitnessApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<MembershipType> MembershipTypes { get; set; } = null!;
        public virtual DbSet<FitnessEvent> FitnessEvents { get; set; } = null!;
        public virtual DbSet<EventRegistration> EventRegistrations { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<ClassRegistration> ClassesRegistrations { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<SpaProcedure> SpaProcedures { get; set; } = null!;
        public virtual DbSet<SpaRegistration> SpaRegistrations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EventRegistration>()
                .HasKey(er => new { er.EventId, er.MemberId });
            builder.Entity<ClassRegistration>()
                .HasKey(cr => new { cr.ClassId, cr.MemberId });
            builder.Entity<SpaRegistration>()
                .HasKey(sr => new { sr.SpaProcedureId, sr.MemberId });

            // Precision settings for prices
            builder.Entity<MembershipType>()
                .Property(mt => mt.Price)
                .HasPrecision(18, 2);
            builder.Entity<SpaProcedure>()
                .Property(sp => sp.Price)
                .HasPrecision(18, 2);

            // Seed data for SpaProcedures
            builder.Entity<SpaProcedure>().HasData(
                new SpaProcedure
                {
                    Id = 1,
                    Name = "Relaxing Massage",
                    Description = "A soothing massage to relieve tension and stress.",
                    Duration = 60,
                    Price = 50.00m,
                    ImageUrl = "https://www.dshieldsusa.com/wp-content/uploads/2021/05/relaxing-massage-slide.jpg"
                },
                new SpaProcedure
                {
                    Id = 2,
                    Name = "Facial Treatment",
                    Description = "A rejuvenating facial to nourish and hydrate your skin.",
                    Duration = 45,
                    Price = 40.00m,
                    ImageUrl = "https://spamd.net/wp-content/uploads/2022/03/medications-facial-treatments.jpg"
                },
                new SpaProcedure
                {
                    Id = 3,
                    Name = "Aromatherapy Session",
                    Description = "A session using essential oils to promote relaxation and well-being.",
                    Duration = 30,
                    Price = 30.00m,
                    ImageUrl = "https://elementsmassage.com/files/shared/AZ%20-%20Elements%20Massage%205-1864269.jpg"
                });

            // Seed data for FitnessEvents
            builder.Entity<FitnessEvent>().HasData(
                new FitnessEvent
                {
                    Id = 1,
                    Title = "Spring City Marathon",
                    Description = "Join us for a thrilling 10K spring marathon through the city streets.",
                    Location = "Downtown City Center",
                    StartDate = new DateTime(2025, 4, 12, 9, 0, 0),
                    EndDate = new DateTime(2025, 4, 12, 12, 0, 0),
                    ImageUrl = "https://www.chicagospringhalf.com/wp-content/uploads/sites/32/2023/05/2022_SPRCHI_RaceDay_Ali_282-1024x683.jpg"
                },
                new FitnessEvent
                {
                    Id = 2,
                    Title = "Mountain Peak Hike",
                    Description = "A challenging hike to the top of the mountain with stunning views.",
                    Location = "Rocky Mountain Trail",
                    StartDate = new DateTime(2025, 7, 15, 6, 0, 0),
                    EndDate = new DateTime(2025, 7, 15, 15, 0, 0),
                    ImageUrl = "https://www.reserveamerica.com/articles/wp-content/uploads/2024/07/11174967-1e85-45df-8097-ac30b3bb1c34.jpg"
                },
                new FitnessEvent
                {
                    Id = 3,
                    Title = "Autumn Lake Walk",
                    Description = "A scenic walk around the beautiful autumn lake. Perfect for relaxation and exercise.",
                    Location = "Autumn Lake Park",
                    StartDate = new DateTime(2025, 10, 8, 10, 0, 0),
                    EndDate = new DateTime(2025, 10, 8, 12, 0, 0),
                    ImageUrl = "https://images.stockcake.com/public/c/a/0/ca09354d-17f0-4693-b9d3-fb2d399a07c1_large/autumn-lakeside-walk-stockcake.jpg"
                }
            );

            // Seed data for Instructors
            builder.Entity<Instructor>().HasData(
                new Instructor
                {
                    Id = 1,
                    FirstName = "Natalie",
                    LastName = "Asatryan",
                    Bio = "Natalie is a certified yoga instructor with over 10 years of experience. She is passionate about helping others achieve their fitness goals and improve their overall well-being.",
                    Specialization = "Yoga",
                    ImageUrl = "https://horizonweekly.ca/wp-content/uploads/2021/01/Nat-2.jpg"
                },
                new Instructor
                {
                    Id = 2,
                    FirstName = "Warren",
                    LastName = "Scott",
                    Bio = "Warren is a certified personal trainer and fitness coach. He specializes in high-intensity interval training (HIIT) and enjoys helping clients push their limits and reach their full potential.",
                    Specialization = "HIIT",
                    ImageUrl = "https://images.squarespace-cdn.com/content/v1/651489d366d19e59b7bbf9cf/a68428a6-992f-45a4-adfc-1b5a75e5cfda/Warren_square500.jpg"
                },
                new Instructor
                {
                    Id = 3,
                    FirstName = "Emily",
                    LastName = "Johnson",
                    Bio = "Emily is a certified Zumba instructor with a background in dance and fitness. She loves creating a fun and inclusive environment where everyone can enjoy the benefits of Zumba.",
                    Specialization = "Zumba",
                    ImageUrl = "https://d29za44huniau5.cloudfront.net/uploads/2023/11/first-class-mobile.png"
                }
            );

            // Seed data for Classes
            builder.Entity<Class>().HasData(
                new Class
                {
                    Id = 1,
                    Name = "Morning Yoga",
                    InstructorId = 1,
                    Schedule = new DateTime(2024, 12, 5, 7, 0, 0),
                    Duration = 60,
                    Description = "A calm and peaceful yoga session to start your day.",
                    ImageUrl = "https://yogajala.com/wp-content/uploads/8-Benefits-Of-Morning-Yoga.jpg"
                },
                new Class
                {
                    Id = 2,
                    Name = "HIIT Challenge",
                    InstructorId = 2,
                    Schedule = new DateTime(2024, 12, 5, 18, 0, 0),
                    Duration = 45,
                    Description = "An intense, high-energy interval training session.",
                    ImageUrl = "https://i.ytimg.com/vi/66_hHeSUrzU/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLB88ucCUVHp_EFpv6T47y7oJRpRsQ"
                },
                new Class
                {
                    Id = 3,
                    Name = "Zumba Party",
                    InstructorId = 3,
                    Schedule = new DateTime(2024, 12, 6, 10, 0, 0),
                    Duration = 60,
                    Description = "A fun and energetic Zumba dance class for all levels.",
                    ImageUrl = "https://i.ytimg.com/vi/N3wBXogMYfM/hq720.jpg?sqp=-oaymwE7CK4FEIIDSFryq4qpAy0IARUAAAAAGAElAADIQj0AgKJD8AEB-AH-CYAC0AWKAgwIABABGGUgUihUMA8=&rs=AOn4CLD9yvCPKa7mHvL_lLUQr-TvnlNYRw"
                }
            );
        }
    }
}