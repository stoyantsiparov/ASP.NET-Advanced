namespace FitnessApp.Web.ViewModels.SpaProcedureViewModels;

public class SpaProceduresDetailsViewModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string? ImageUrl { get; set; }
	public string Description { get; set; } = null!;
	public int Duration { get; set; }
	public decimal Price { get; set; }
	public string AppointmentDateTime { get; set; } = null!;
}