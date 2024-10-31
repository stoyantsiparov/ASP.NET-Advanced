using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.MembershipType;

namespace FitnessApp.Data.Models;

public class MembershipType
{
	[Comment("Primary key")]
	[Key]
	public int Id { get; set; }

	[Required]
	[Comment("Membership type")]
	[MaxLength(NameMaxLength)]
	public string Name { get; set; } = null!;

	[Required]
	[Comment("Price of the membership type")]
	[Range((double)PriceMinValue, (double)PriceMaxValue)]
	public decimal Price { get; set; }

	[Required]
	[Comment("Duration of the membership type in days")]
	[Range(DurationMinValue, DurationMaxValue)]
	public int Duration { get; set; }

	[Comment("Members with this membership type")]
	public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}