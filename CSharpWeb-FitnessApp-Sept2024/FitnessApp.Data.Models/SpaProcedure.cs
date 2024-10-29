using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.SpaProcedure;

namespace FitnessApp.Data.Models;

public class SpaProcedure
{
	[Comment("Primary key")]
	[Key]
	public int Id { get; set; }

	[Comment("Spa type")]
	[MaxLength(NameMaxLength)]
	public required string Name { get; set; }

	[Comment("Description of the spa procedure")]
	[MaxLength(DescriptionMaxLength)]
	public required string Description { get; set; }

	[Comment("Duration of the spa procedure in minutes")]
	[Range(DurationMinValue, DurationMaxValue)]
	public required int Duration { get; set; }

	[Comment("Price of the spa procedure")]
	[Range((double)PriceMinValue, (double)PriceMaxValue)]
	public required decimal Price { get; set; }

	[Comment("Registrations for this spa procedure")]
	public virtual ICollection<SpaRegistration> SpaRegistrations { get; set; } = new List<SpaRegistration>();
}