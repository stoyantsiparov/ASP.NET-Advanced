using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Web.ViewModels.FitnessEventViewModels;
using static FitnessApp.Common.EntityValidationConstants.FitnessEvent;

public class FitnessEventViewModel
{
	public int Id { get; set; }

	[Required]
	[MaxLength(TitleMaxLength)]
	[MinLength(TitleMinLength)]
	public string Title { get; set; } = null!;

	public string? ImageUrl { get; set; }

	[Required]
	[MaxLength(DescriptionMaxLength)]
	[MinLength(DescriptionMinLength)]
	public string Description { get; set; } = null!;

	[Required]
	[MaxLength(LocationMaxLength)]
	[MinLength(LocationMinLength)]
	public string Location { get; set; } = null!;

	[Required]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = DateTimeFormat, ApplyFormatInEditMode = true)]
	public string StartDate { get; set; } = null!;

	[Required]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = DateTimeFormat, ApplyFormatInEditMode = true)]
	public string EndDate { get; set; } = null!;
}