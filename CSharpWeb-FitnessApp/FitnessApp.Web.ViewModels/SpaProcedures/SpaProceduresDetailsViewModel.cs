namespace FitnessApp.Web.ViewModels.SpaProcedures;

public class SpaProceduresDetailsViewModel
{
	public required int Id { get; set; }

	public required string Name { get; set; }

	public string? ImageUrl { get; set; }

	public required string Description { get; set; }

	public required int Duration { get; set; }

	public required decimal Price { get; set; }
}