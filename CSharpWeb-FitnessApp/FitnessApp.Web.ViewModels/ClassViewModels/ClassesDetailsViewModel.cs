using FitnessApp.Web.ViewModels.InstructorViewModels;

namespace FitnessApp.Web.ViewModels.ClassViewModels;

public class ClassesDetailsViewModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string? ImageUrl { get; set; }
	public string Description { get; set; } = null!;
	public decimal Price { get; set; }
	public string Schedule { get; set; } = null!;
	public int Duration { get; set; }

	public InstructorViewModel Instructor { get; set; } = null!;
}