using FitnessApp.Web.ViewModels.InstructorViewModels;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Web.ViewModels.ClassViewModels;

public class DeleteClassViewModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
    public string Schedule { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public int Duration { get; set; }
}