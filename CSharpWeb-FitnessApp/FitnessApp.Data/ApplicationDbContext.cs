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

			builder.Entity<MembershipType>()
				.Property(mt => mt.Price)
				.HasPrecision(18, 2);
			builder.Entity<SpaProcedure>()
				.Property(sp => sp.Price)
				.HasPrecision(18, 2);
		}
	}
}
