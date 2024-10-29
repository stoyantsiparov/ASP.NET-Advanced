using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.MembershipType;

namespace FitnessApp.Data.Models;

public class MembershipType
{
	[Comment("Primary key")]
	[Key]
	public required int Id { get; set; }

	[Comment("Membership type")]
	[MaxLength(NameMaxLength)]
	public required string Name { get; set; }

	[Comment("Price of the membership type")]
	[Range((double)PriceMinValue, (double)PriceMaxValue)]
	public required decimal Price { get; set; }

	[Comment("Duration of the membership type in days")]
	[Range(DurationMinValue, DurationMaxValue)]
	public required int Duration { get; set; }

	[Comment("Members with this membership type")]
	public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}