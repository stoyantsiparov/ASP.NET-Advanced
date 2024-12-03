namespace FitnessApp.Web.ViewModels.InstructorViewModels;

public class InstructorDetailsViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string Bio { get; set; } = null!;
    public string Specialization { get; set; } = null!;
}