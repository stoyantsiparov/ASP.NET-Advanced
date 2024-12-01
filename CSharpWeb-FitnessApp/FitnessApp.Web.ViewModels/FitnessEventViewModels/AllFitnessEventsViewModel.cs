namespace FitnessApp.Web.ViewModels.FitnessEventViewModels;

public class AllFitnessEventsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string Location { get; set; } = null!;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}