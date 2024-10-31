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
	public int Id { get; set; }

	[Required]
	[Comment("First name of the member")]
	[MaxLength(FirstNameMaxLength)]
	public string FirstName { get; set; } = null!;

	[Required]
	[Comment("Last name of the member")]
	[MaxLength(LastNameMaxLength)]
	public string LastName { get; set; } = null!;

	[Required]
	[Comment("Email of the member")]
	[MaxLength(EmailMaxLength)]
	public string Email { get; set; } = null!;

	[Required]
	[Comment("Phone number of the member")]
	[MaxLength(PhoneNumberMaxLength)]
	public string Phone { get; set; } = null!;

	[Required]
	[Comment("Date whn the member joined the gym")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = JoinDateTimeFormat, ApplyFormatInEditMode = true)]
	public DateTime JoinDate { get; set; }

	[Comment("Membership type of the member")]
	[ForeignKey(nameof(MembershipType))]
	public int MembershipTypeId { get; set; }
	public MembershipType MembershipType { get; set; } = null!;

	[Comment("Class registrations of the member")]
	public virtual ICollection<ClassRegistration> ClassRegistrations { get; set; } = new List<ClassRegistration>();

	[Comment("Event registrations of the member")]
	public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

	[Comment("Spa registrations of the member")]
	public virtual ICollection<SpaRegistration> SpaRegistrations { get; set; } = new List<SpaRegistration>();
}