namespace FitnessApp.Web.ViewModels.SpaProcedures;

public class AllSpaProceduresViewModel
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public string? ImageUrl { get; set; }
	public string? TreatmentDay { get; set; }
	public DateTime AppointmentDateTime { get; set; }
}