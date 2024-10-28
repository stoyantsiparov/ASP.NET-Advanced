using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Data.Data
{
	public class Member
	{
		[Key]
		public required int Id { get; set; }

		[MaxLength(50)]
		public required string FirstName { get; set; }

		[MaxLength(50)]
		public required string LastName { get; set; }

		[MaxLength(100)]
		public required string Email { get; set; }

		[MaxLength(15)]
		public required string Phone { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Join Date")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public required DateTime JoinDate { get; set; }

		[ForeignKey(nameof(MembershipType))]
		public required int MembershipTypeId { get; set; }
		public required MembershipType MembershipType { get; set; }

		public virtual ICollection<ClassRegistration> ClassRegistrations { get; set; } = new List<ClassRegistration>();
		public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();
		public virtual ICollection<SpaRegistration> SpaAppointments { get; set; } = new List<SpaRegistration>();
	}
}
