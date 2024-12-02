namespace FitnessApp.Web.ViewModels.FitnessEventViewModels;

public class DeleteFitnessEventViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string Location { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string StartDateTime { get; set; } = null!;
    public string EndDateTime { get; set; } = null!;
}