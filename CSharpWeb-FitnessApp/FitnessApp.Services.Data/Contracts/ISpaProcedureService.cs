using FitnessApp.Web.ViewModels.SpaProcedures;

namespace FitnessApp.Services.Data.Contracts
{
	public interface ISpaProcedureService
	{
		Task<IEnumerable<AllSpaProceduresViewModel>> GetAllSpaProceduresAsync();
		Task<SpaProceduresViewModel?> GetSpaProceduresByIdAsync(int id);
		Task<SpaProceduresDetailsViewModel?> GetSpaProceduresDetailsAsync(int id);
		Task<IEnumerable<AllSpaProceduresViewModel>> GetMySpaProceduresAsync(string userId);
		Task AddToMySpaAppointmentsAsync(string userId, SpaProceduresViewModel spaProcedure, DateTime appointmentDateTime);
		Task RemoveFromMySpaAppointmentsAsync(string userId, SpaProceduresViewModel spaProcedure);
	}
}