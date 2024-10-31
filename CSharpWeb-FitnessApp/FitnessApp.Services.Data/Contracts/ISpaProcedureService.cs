using System.Collections;
using FitnessApp.Data.Models;
using FitnessApp.Web.ViewModels.SpaProcedures;

namespace FitnessApp.Services.Data.Contracts
{
    public interface ISpaProcedureService
    {
        /// <summary>
        /// Get all spa procedures
        /// </summary>
        Task<IEnumerable<AllSpaProceduresViewModel>> GetAllSpaProceduresAsync();

        /// <summary>
        /// Get spa procedure by id
        /// </summary>
        Task<SpaProcedure?> GetSpaProceduresByIdAsync(int id);

        /// <summary>
        /// Get all treatment days
        /// </summary>
        Task<List<string>> GetTreatmentDaysAsync();

        /// <summary>
        /// Get spa procedure details by id
        /// </summary>
        Task<SpaProceduresDetailsViewModel?> GetSpaProceduresDetailsAsync(int id);

        /// <summary>
        /// Get all spa procedures for the current user
        /// </summary>
        Task<IEnumerable<AllSpaProceduresViewModel>> GetMySpaProceduresAsync(string userId);

        /// <summary>
        /// Add spa procedure to the current user's appointments
        /// </summary>
        Task AddToMySpaAppointmentsAsync(string userId, SpaProcedure spaProcedure, string treatmentDay);

        /// <summary>
        /// Remove spa procedure from the current user's appointments
        /// </summary>
        Task RemoveFromMySpaAppointmentsAsync(string userId, SpaProcedure spaProcedure);
    }
}