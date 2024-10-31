using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.SpaProcedure;

namespace FitnessApp.Data.Models;

public class SpaProcedure
{
	[Comment("Primary key")]
	[Key]
	public int Id { get; set; }

	[Required]
	[Comment("Spa type")]
	[MaxLength(NameMaxLength)]
	public string Name { get; set; } = null!;

	public string? ImageUrl { get; set; }

    [Required]
	[Comment("Description of the spa procedure")]
	[MaxLength(DescriptionMaxLength)]
	public string Description { get; set; } = null!;

    [Required]
	[Comment("Duration of the spa procedure in minutes")]
	[Range(DurationMinValue, DurationMaxValue)]
	public int Duration { get; set; }

	[Required]
	[Comment("Price of the spa procedure")]
	[Range((double)PriceMinValue, (double)PriceMaxValue)]
	public decimal Price { get; set; }

	[Comment("Registrations for this spa procedure")]
	public virtual ICollection<SpaRegistration> SpaRegistrations { get; set; } = new List<SpaRegistration>();
}