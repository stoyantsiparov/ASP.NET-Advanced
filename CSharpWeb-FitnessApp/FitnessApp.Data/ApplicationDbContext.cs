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
		}
	}
}
