using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.Member;

namespace FitnessApp.Data.Models;

public class Member
{
	[Comment("Primary key")]
	[Key]
	public required int Id { get; set; }

	[Comment("First name of the member")]
	[MaxLength(FirstNameMaxLength)]
	public required string FirstName { get; set; }

	[Comment("Last name of the member")]
	[MaxLength(LastNameMaxLength)]
	public required string LastName { get; set; }

	[Comment("Email of the member")]
	[MaxLength(EmailMaxLength)]
	public required string Email { get; set; }

	[Comment("Phone number of the member")]
	[MaxLength(PhoneNumberMaxLength)]
	public required string Phone { get; set; }

	[Comment("Date whn the member joined the gym")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = JoinDateTimeFormat, ApplyFormatInEditMode = true)]
	public required DateTime JoinDate { get; set; }

	[Comment("Membership type of the member")]
	[ForeignKey(nameof(MembershipType))]
	public required int MembershipTypeId { get; set; }
	public required MembershipType MembershipType { get; set; }

	[Comment("Class registrations of the member")]
	public virtual ICollection<ClassRegistration> ClassRegistrations { get; set; } = new List<ClassRegistration>();

	[Comment("Event registrations of the member")]
	public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

	[Comment("Spa registrations of the member")]
	public virtual ICollection<SpaRegistration> SpaRegistrations { get; set; } = new List<SpaRegistration>();
}