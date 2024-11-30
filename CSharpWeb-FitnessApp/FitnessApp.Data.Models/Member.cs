using System.ComponentModel.DataAnnotations;
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

    [Comment("Membership type of the member")]
	public virtual ICollection<MembershipRegistration> MembershipRegistrations { get; set; } = new List<MembershipRegistration>();

	[Comment("Class registrations of the member")]
	public virtual ICollection<ClassRegistration> ClassRegistrations { get; set; } = new List<ClassRegistration>();

	[Comment("Event registrations of the member")]
	public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

	[Comment("Spa registrations of the member")]
	public virtual ICollection<SpaRegistration> SpaRegistrations { get; set; } = new List<SpaRegistration>();
}