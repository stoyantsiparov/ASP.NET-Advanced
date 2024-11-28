using FitnessApp.Web.ViewModels.MembershipTypeViewModels;

namespace FitnessApp.Services.Data.Contracts;
public interface IMembershipTypeService
{
	Task<IEnumerable<AllMembershipTypeViewModel>> GetAllMembershipTypesAsync();
	Task<MembershipTypeViewModel?> GetMembershipTypeByIdAsync(int id);
	Task<MembershipTypeDetailsViewModel?> GetMembershipTypeDetailsAsync(int id);
	Task<IEnumerable<AllMembershipTypeViewModel>> GetMyMembershipTypesAsync(string userId);
	Task AddMyMembershipAsync(string userId, MembershipTypeViewModel? membershipTypeViewModel);
	Task RemoveMyMembershipAsync(string userId, MembershipTypeViewModel? membershipTypeViewModel);
}