namespace FitnessApp.Web.ViewModels.ClassViewModels;

public class AllClassesViewModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string? ImageUrl { get; set; }
	public string Schedule { get; set; } = null!;
	public int Duration { get; set; }
}