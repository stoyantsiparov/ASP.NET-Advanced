using System.ComponentModel.DataAnnotations;
using static FitnessApp.Common.EntityValidationConstants.MembershipType;

namespace FitnessApp.Data.Models;

public class MembershipType
{
	[Key]
	public required int Id { get; set; }

	[MaxLength(NameMaxLength)]
	public required string Name { get; set; }

	[Range((double)PriceMinValue, (double)PriceMaxValue)]
	public required decimal Price { get; set; }

	[Range(DurationMinValue, DurationMaxValue)]
	public required int Duration { get; set; }

	public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}