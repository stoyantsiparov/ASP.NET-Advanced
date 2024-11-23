using FitnessApp.Web.ViewModels.ClassViewModels;

namespace FitnessApp.Services.Data.Contracts
{
	public interface IClassService
	{
		Task<IEnumerable<AllClassesViewModel>> GetAllClassesAsync();
		Task<ClassesViewModel?> GetClassByIdAsync(int id);
		Task<ClassesDetailsViewModel?> GetClassDetailsAsync(int id);
		Task<IEnumerable<AllClassesViewModel>> GetMyClassesAsync(string userId);
		Task AddToMyClassesAsync(string userId, ClassesViewModel? classesViewModel, DateTime appointmentDateTime);
		Task RemoveFromMyClassesAsync(string userId, ClassesViewModel? classesViewModel);
	}
}