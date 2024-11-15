using FitnessApp.Data.Models;
using FitnessApp.Web.ViewModels.SpaProcedures;

namespace FitnessApp.Services.Data.Contracts
{
    public interface ISpaProcedureService
    {
        Task<IEnumerable<AllSpaProceduresViewModel>> GetAllSpaProceduresAsync();
        // TODO: REMOVE SpaProcedure? and replace with SpaProcedureViewModel
        Task<SpaProcedure?> GetSpaProceduresByIdAsync(int id);
        Task<SpaProceduresDetailsViewModel?> GetSpaProceduresDetailsAsync(int id);
        Task<IEnumerable<AllSpaProceduresViewModel>> GetMySpaProceduresAsync(string userId);
        // TODO: REMOVE SpaProcedure? and replace with SpaProcedureViewModel
        Task AddToMySpaAppointmentsAsync(string userId, SpaProcedure spaProcedure, DateTime appointmentDateTime);
        // TODO: REMOVE SpaProcedure? and replace with SpaProcedureViewModel
        Task RemoveFromMySpaAppointmentsAsync(string userId, SpaProcedure spaProcedure);
    }
}