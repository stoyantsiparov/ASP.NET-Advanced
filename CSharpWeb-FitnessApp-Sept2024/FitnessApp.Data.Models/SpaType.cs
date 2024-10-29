using System.ComponentModel.DataAnnotations;
using static FitnessApp.Common.EntityValidationConstants.SpaType;

namespace FitnessApp.Data.Models;

public class SpaType
{
	[Key]
	public int Id { get; set; }

	[MaxLength(NameMaxLength)]
	public required string Name { get; set; }

	[MaxLength(DescriptionMaxLength)]
	public required string Description { get; set; }

	[Range(DurationMinValue, DurationMaxValue)]
	public required int Duration { get; set; }

	[Range((double)PriceMinValue, (double)PriceMaxValue)]
	public required decimal Price { get; set; }

	public virtual ICollection<SpaRegistration> SpaRegistrations { get; set; } = new List<SpaRegistration>();
}