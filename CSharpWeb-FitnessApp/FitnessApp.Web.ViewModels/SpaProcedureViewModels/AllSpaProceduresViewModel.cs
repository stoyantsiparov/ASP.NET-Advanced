namespace FitnessApp.Web.ViewModels.SpaProcedureViewModels;

public class AllSpaProceduresViewModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string? Description { get; set; }
	public string? ImageUrl { get; set; }
    public string AppointmentDateTime { get; set; } = null!;
}