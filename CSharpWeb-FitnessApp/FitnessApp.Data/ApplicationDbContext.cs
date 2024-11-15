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
					Title = "Morning Yoga",
					Description = "A calm and peaceful yoga session to start your day.",
					Location = "Gym 1",
					StartDate = new DateTime(2024, 12, 5, 7, 0, 0), // DateTime example with time
					EndDate = new DateTime(2024, 12, 5, 8, 0, 0),   // DateTime example with time
					ImageUrl = "https://yogajala.com/wp-content/uploads/8-Benefits-Of-Morning-Yoga.jpg"
				},
				new FitnessEvent
				{
					Id = 2,
					Title = "HIIT Challenge",
					Description = "An intense, high-energy interval training session.",
					Location = "Gym 2",
					StartDate = new DateTime(2024, 12, 5, 18, 0, 0), // DateTime example with time
					EndDate = new DateTime(2024, 12, 5, 19, 0, 0),   // DateTime example with time
					ImageUrl = "https://i.ytimg.com/vi/66_hHeSUrzU/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLB88ucCUVHp_EFpv6T47y7oJRpRsQ"
				},
				new FitnessEvent
				{
					Id = 3,
					Title = "Zumba Party",
					Description = "A fun and energetic Zumba dance class for all levels.",
					Location = "Gym 3",
					StartDate = new DateTime(2024, 12, 6, 10, 0, 0), // DateTime example with time
					EndDate = new DateTime(2024, 12, 6, 11, 0, 0),   // DateTime example with time
					ImageUrl = "https://i.ytimg.com/vi/N3wBXogMYfM/hq720.jpg?sqp=-oaymwE7CK4FEIIDSFryq4qpAy0IARUAAAAAGAElAADIQj0AgKJD8AEB-AH-CYAC0AWKAgwIABABGGUgUihUMA8=&rs=AOn4CLD9yvCPKa7mHvL_lLUQr-TvnlNYRw"
				});
		}
	}
}
