namespace FitnessApp.Web.ViewModels.FitnessEventViewModels;

public class FitnessEventDetailsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ImageUrl { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string? StartDateTime { get; set; }
    public string? EndDateTime { get; set; }
}