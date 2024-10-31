using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Web.ViewModels.SpaProcedures;

public class SpaProceduresDetailsViewModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string? ImageUrl { get; set; }
	public string Description { get; set; } = null!;
	public int Duration { get; set; }
	public decimal Price { get; set; }
    public string TreatmentDay { get; set; } = null!;
    public IEnumerable<string> TreatmentDaysOptions { get; set; } = new List<string> { "Saturday", "Sunday" };
}