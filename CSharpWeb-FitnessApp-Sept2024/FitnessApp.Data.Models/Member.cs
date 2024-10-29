using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FitnessApp.Common.EntityValidationConstants.Member;

namespace FitnessApp.Data.Models;

public class Member
{
	[Key]
	public required int Id { get; set; }

	[MaxLength(FirstNameMaxLength)]
	public required string FirstName { get; set; }

	[MaxLength(LastNameMaxLength)]
	public required string LastName { get; set; }

	[MaxLength(EmailMaxLength)]
	public required string Email { get; set; }

	[MaxLength(PhoneNumberMaxLength)]
	public required string Phone { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = JoinDateTimeFormat, ApplyFormatInEditMode = true)]
	public required DateTime JoinDate { get; set; }

	[ForeignKey(nameof(MembershipType))]
	public required int MembershipTypeId { get; set; }
	public required MembershipType MembershipType { get; set; }

	public virtual ICollection<ClassRegistration> ClassRegistrations { get; set; } = new List<ClassRegistration>();
	public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();
	public virtual ICollection<SpaRegistration> SpaRegistrations { get; set; } = new List<SpaRegistration>();
}