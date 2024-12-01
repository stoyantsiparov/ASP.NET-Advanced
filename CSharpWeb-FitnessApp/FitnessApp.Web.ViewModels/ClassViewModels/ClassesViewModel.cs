using System.ComponentModel.DataAnnotations;
using FitnessApp.Web.ViewModels.InstructorViewModels;

namespace FitnessApp.Web.ViewModels.ClassViewModels;
using static FitnessApp.Common.EntityValidationConstants.Class;

public class ClassesViewModel
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
    public string Schedule { get; set; } = null!;

	[Required]
	[Range(DurationMinValue, DurationMaxValue)]
	public int Duration { get; set; }

	[Range(1, int.MaxValue)]
	public int InstructorId { get; set; }
	public virtual IEnumerable<InstructorViewModel> Instructors { get; set; } = new List<InstructorViewModel>();
}