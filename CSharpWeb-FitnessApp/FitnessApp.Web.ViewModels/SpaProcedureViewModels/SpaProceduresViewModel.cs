using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Web.ViewModels.SpaProcedureViewModels;
using static FitnessApp.Common.EntityValidationConstants.SpaProcedure;

public class SpaProceduresViewModel 
{
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    [MinLength(NameMinLength)]
	public string Name { get; set; } = null!;

	public string? ImageUrl { get; set; }

	[Required]
	[MaxLength(DescriptionMaxLength)]
	[MinLength(DescriptionMinLength)]
	public string Description { get; set; } = null!;

	[Required]
	[Range(DurationMinValue, DurationMaxValue)]
	public int Duration { get; set; }

	[Required]
	[Range((double)PriceMinValue, (double)PriceMaxValue)]
	public decimal Price { get; set; }

	[Required]
	[DataType(DataType.DateTime)]
	[DisplayFormat(DataFormatString = AppointmentDateTimeFormat, ApplyFormatInEditMode = true)]
	public string AppointmentDateTime { get; set; } = null!;
}