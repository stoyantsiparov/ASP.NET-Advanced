namespace FitnessApp.Web.ViewModels.InstructorViewModels;

public class AllInstructorsViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string Specialization { get; set; } = null!;
}