using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Web.ViewModels.MembershipTypeViewModels;
using static FitnessApp.Common.EntityValidationConstants.MembershipType;

public class AddMembershipTypeViewModel
{
	[Required]
	[MaxLength(NameMaxLength)]
	[MinLength(NameMinLength)]
	public string Name { get; set; } = null!;

	[Required]
	[Range((double)PriceMinValue, (double)PriceMaxValue)]
	public decimal Price { get; set; }

	[Required]
	[Range(DurationMinValue, DurationMaxValue)]
	public int Duration { get; set; }

	[Required]
	[MaxLength(DescriptionMaxLength)]
	[MinLength(DescriptionMinLength)]
	public string Description { get; set; } = null!;
	public string? ImageUrl { get; set; }
}