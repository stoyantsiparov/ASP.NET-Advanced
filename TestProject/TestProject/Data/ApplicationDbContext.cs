using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestProject.Data.Data;

namespace TestProject.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<EventRegistration>()
				.HasKey(er => new { er.EventId, er.MemberId });
			builder.Entity<ClassRegistration>()
				.HasKey(cr => new { cr.ClassId, cr.MemberId });
			builder.Entity<SpaRegistration>()
				.HasKey(sr => new { sr.SpaTypeId, sr.MemberId });

			builder.Entity<MembershipType>()
				.Property(mt => mt.Price)
				.HasPrecision(18, 2);
			builder.Entity<SpaType>()
				.Property(sp => sp.Price)
				.HasPrecision(18, 2);
		}

		public virtual DbSet<Member> Members { get; set; }
		public virtual DbSet<MembershipType> MembershipTypes { get; set; }
		public virtual DbSet<Event> Events { get; set; }
		public virtual DbSet<EventRegistration> EventRegistrations { get; set; }
		public virtual DbSet<Class> Classes { get; set; }
		public virtual DbSet<ClassRegistration> ClassesRegistrations { get; set; }
		public virtual DbSet<Instructor> Instructors { get; set; }
		public virtual DbSet<SpaType> SpaTypes { get; set; }
		public virtual DbSet<SpaRegistration> SpaRegistrations { get; set; }
	}
}
