using FitnessApp.Web.ViewModels.SpaProcedureViewModels;

namespace FitnessApp.Services.Data.Contracts;

public interface ISpaProcedureService
{
	Task<IEnumerable<AllSpaProceduresViewModel>> GetAllSpaProceduresAsync();
	Task<SpaProceduresViewModel?> GetSpaProceduresByIdAsync(int id);
	Task<SpaProceduresDetailsViewModel?> GetSpaProceduresDetailsAsync(int id);
	Task<IEnumerable<AllSpaProceduresViewModel>> GetMySpaProceduresAsync(string userId);
	Task AddToMySpaAppointmentsAsync(string userId, SpaProceduresViewModel spaProcedure, DateTime appointmentDateTime);
	Task RemoveFromMySpaAppointmentsAsync(string userId, SpaProceduresViewModel spaProcedure);
    Task<AddSpaProcedureViewModel> GetSpaProcedureForAddAsync();
    Task AddSpaProcedureAsync(AddSpaProcedureViewModel model, string userId);
    Task EditSpaProcedureAsync(SpaProceduresViewModel model, string userId);
    Task<DeleteSpaProcedureViewModel?> GetSpaProcedureForDeleteAsync(int id);
    Task DeleteSpaProcedureAsync(int id, string userId);
}