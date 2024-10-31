using System;
using FitnessApp.Web.ViewModels.SpaProcedures;

namespace FitnessApp.Services.Data.Contracts;

public interface ISpaProcedureService
{
	/// <summary>
	/// Get all spa procedures
	/// </summary>
	Task<IEnumerable<AllSpaProceduresViewModel>> GetAllSpaProceduresAsync();

	/// <summary>
	/// Gets the spa treatments the user is signed up for.
	/// </summary>
	Task<IEnumerable<AllSpaProceduresViewModel>> GetMySpaProceduresAsync(string userId);

	/// <summary>
	/// Get spa procedures by id
	/// </summary>
	Task<SpaProceduresViewModel?> GetSpaProceduresByIdAsync(int id);
	
	/// <summary>
	/// Get spa procedures details by id
	/// </summary>
	Task<SpaProceduresDetailsViewModel?> GetSpaProceduresDetailsAsync(int id);

    /// <summary>
    /// Add spa procedures to user's appointments
    /// </summary>
    Task AddToMySpaAppointmentsAsync(string userId, SpaProceduresViewModel spaProcedures);

    /// <summary>
    /// Remove spa procedures from user's appointments
    /// </summary>
    Task RemoveFromMySpaAppointmentsAsync(string userId, SpaProceduresViewModel spaProcedures);
}