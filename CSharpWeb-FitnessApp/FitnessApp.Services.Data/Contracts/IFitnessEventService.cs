using FitnessApp.Web.ViewModels.FitnessEvents;

namespace FitnessApp.Services.Data.Contracts;

public interface IFitnessEventService
{
    Task<IEnumerable<AllFitnessEventsViewModel>> GetAllFitnessEventsAsync();
    Task<FitnessEventViewModel?> GetFitnessEventByIdAsync(int id);
    Task<FitnessEventDetailsViewModel?> GetFitnessEventDetailsAsync(int id);
    Task<IEnumerable<AllFitnessEventsViewModel>> GetMyFitnessEventsAsync(string userId);
    Task AddToMyFitnessEventsAsync(string userId, FitnessEventViewModel fitnessEventViewModel, DateTime appointmentDateTime);
    Task RemoveFromMyFitnessEventsAsync(string userId, FitnessEventViewModel fitnessEventViewModel);
}