namespace FitnessApp.Web.ViewModels.MembershipTypeViewModels;

public class MembershipTypeDetailsViewModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public decimal Price { get; set; }
	public int Duration { get; set; }
	public string Description { get; set; } = null!;
	public string? ImageUrl { get; set; }
}