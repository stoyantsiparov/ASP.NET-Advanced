namespace FitnessApp.Web.ViewModels.FitnessEvents;

public class AllFitnessEventsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ImageUrl { get; set; }
    public string Location { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}