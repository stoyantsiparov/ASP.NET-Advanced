using FitnessApp.Web.ViewModels.InstructorViewModels;

namespace FitnessApp.Services.Data.Contracts;

public interface IInstructorService
{
    Task<IEnumerable<AllInstructorsViewModel>> GetAllInstructorsAsync();
    Task<InstructorViewModel?> GetInstructorByIdAsync(int id);
    Task<InstructorDetailsViewModel?> GetInstructorDetailsAsync(int id);
    Task<AddInstructorViewModel> GetInstructorForAddAsync();
    Task AddInstructorAsync(AddInstructorViewModel model, string userId);
    Task EditInstructorAsync(InstructorViewModel model, string userId);
    Task<DeleteInstructorViewModel?> GetInstructorForDeleteAsync(int id);
    Task DeleteInstructorAsync(int id, string userId);
}