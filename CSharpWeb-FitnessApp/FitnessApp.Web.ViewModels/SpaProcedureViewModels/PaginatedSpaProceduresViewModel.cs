namespace FitnessApp.Web.ViewModels.SpaProcedureViewModels;

public class PaginatedSpaProceduresViewModel
{
    public IEnumerable<AllSpaProceduresViewModel> SpaProcedures { get; set; } = new List<AllSpaProceduresViewModel>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public string? SearchQuery { get; set; }
}