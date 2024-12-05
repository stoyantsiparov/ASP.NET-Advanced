using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.MembershipTypeViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.ApplicationsConstants;
using static FitnessApp.Common.ErrorMessages.Roles;
using static FitnessApp.Common.ErrorMessages.MembershipType;

namespace FitnessApp.Services.Data;
public class MembershipTypeService : IMembershipTypeService
{
	private readonly ApplicationDbContext _context;
	private readonly UserManager<IdentityUser> _userManager;

    public MembershipTypeService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

	/// <summary>
	/// Get all membership types from the database.
	/// </summary>
	public async Task<IEnumerable<AllMembershipTypeViewModel>> GetAllMembershipTypesAsync()
	{
		return await _context.MembershipTypes
			.Select(m => new AllMembershipTypeViewModel
			{
				Id = m.Id,
				Name = m.Name,
				Price = m.Price,
				Duration = m.Duration,
				ImageUrl = m.ImageUrl
			})
			.AsNoTracking()
			.ToListAsync();
	}

	/// <summary>
	/// Get a specific membership type by its ID.
	/// </summary>
	public async Task<MembershipTypeViewModel?> GetMembershipTypeByIdAsync(int id)
	{
		return await _context.MembershipTypes
			.Where(m => m.Id == id)
			.Select(m => new MembershipTypeViewModel
			{
				Id = m.Id,
				Name = m.Name,
				Price = m.Price,
				Duration = m.Duration,
				Description = m.Description,
				ImageUrl = m.ImageUrl
			})
			.FirstOrDefaultAsync();
	}

	/// <summary>
	/// Get detailed information about a specific membership type.
	/// </summary>
	public async Task<MembershipTypeDetailsViewModel?> GetMembershipTypeDetailsAsync(int id)
	{
		return await _context.MembershipTypes
			.Where(m => m.Id == id)
			.Select(m => new MembershipTypeDetailsViewModel
			{
				Id = m.Id,
				Name = m.Name,
				Price = m.Price,
				Duration = m.Duration,
				Description = m.Description,
				ImageUrl = m.ImageUrl
			})
			.AsNoTracking()
			.FirstOrDefaultAsync();
	}

	/// <summary>
	/// Get all membership types that the user is subscribed to.
	/// </summary>
	public async Task<IEnumerable<AllMembershipTypeViewModel>> GetMyMembershipTypesAsync(string userId)
	{
		return await _context.MembershipRegistrations
			.Where(r => r.MemberId == userId)
			.Select(r => new AllMembershipTypeViewModel
			{
				Id = r.MembershipType.Id,
				Name = r.MembershipType.Name,
				Price = r.MembershipType.Price,
				Duration = r.MembershipType.Duration,
				ImageUrl = r.MembershipType.ImageUrl
			})
			.AsNoTracking()
			.ToListAsync();
	}

    /// <summary>
    /// Add a membership type to the user's list of memberships.
    /// </summary>
    public async Task AddMyMembershipAsync(string userId, MembershipTypeViewModel? membershipTypeViewModel)
    {
        if (membershipTypeViewModel != null)
        {
            var membershipType = await _context.MembershipTypes.FindAsync(membershipTypeViewModel.Id);

            if (membershipType == null)
            {
                throw new InvalidOperationException(MembershipTypeDoesNotExist);
            }
        }

        var existingMembership = await _context.MembershipRegistrations
            .FirstOrDefaultAsync(r => r.MemberId == userId);

        if (existingMembership != null)
        {
            throw new InvalidOperationException(OnlyOneMembershipTypeAllowed);
        }

        if (membershipTypeViewModel != null)
        {
            var registration = new MembershipRegistration
            {
                MemberId = userId,
                MembershipTypeId = membershipTypeViewModel.Id
            };

            await _context.MembershipRegistrations.AddAsync(registration);
        }

        await _context.SaveChangesAsync();

        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var roleAdded = await _userManager.AddToRoleAsync(user, MemberRole);
            if (!roleAdded.Succeeded)
            {
                throw new InvalidOperationException(FailedToAssignMemberRole);
            }
        }
    }

    /// <summary>
    /// Remove a membership type from the user's list of memberships.
    /// </summary>
    public async Task RemoveMyMembershipAsync(string userId, MembershipTypeViewModel? membershipTypeViewModel)
    {
        var registration = await _context.MembershipRegistrations
            .FirstOrDefaultAsync(r => membershipTypeViewModel != null && r.MemberId == userId && r.MembershipTypeId == membershipTypeViewModel.Id);

        if (registration == null)
        {
            throw new InvalidOperationException(MembershipNotPurchased);
        }

        _context.MembershipRegistrations.Remove(registration);
        await _context.SaveChangesAsync();

        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var roleRemoved = await _userManager.RemoveFromRoleAsync(user, MemberRole);
            if (!roleRemoved.Succeeded)
            {
                throw new InvalidOperationException(FailedToRemoveMemberRole);
            }
        }
    }

    /// <summary>
    /// Get the necessary information for adding a new membership type.
    /// </summary>
    public async Task<AddMembershipTypeViewModel> GetMembershipTypeForAddAsync()
	{
		var model = new AddMembershipTypeViewModel
		{
			Name = string.Empty,
			ImageUrl = string.Empty,
			Description = string.Empty,
			Price = 0.0m,
			Duration = 0
		};

		return await Task.FromResult(model);
	}

    /// <summary>
    ///	Add a new membership type to the database.
    /// </summary>
    public async Task AddMembershipTypeAsync(AddMembershipTypeViewModel model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null || !await _userManager.IsInRoleAsync(user, AdminRole))
        {
            throw new InvalidOperationException(YouAreNotAuthorizedToAdd);
        }

        var membershipType = new MembershipType
        {
            Name = model.Name,
            ImageUrl = model.ImageUrl,
            Description = model.Description,
            Price = model.Price,
            Duration = model.Duration
        };

        await _context.MembershipTypes.AddAsync(membershipType);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Edit an existing membership type.
    /// </summary>
    public async Task EditMembershipTypeAsync(MembershipTypeViewModel model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null || !await _userManager.IsInRoleAsync(user, AdminRole))
        {
            throw new InvalidOperationException(YouAreNotAuthorizedToEdit);
        }

        var membershipType = await _context.MembershipTypes.FindAsync(model.Id);

        if (membershipType == null)
        {
            throw new InvalidOperationException(MembershipTypeDoesNotExist);
        }

        membershipType.Name = model.Name;
        membershipType.Price = model.Price;
        membershipType.Duration = model.Duration;
        membershipType.Description = model.Description;
        membershipType.ImageUrl = model.ImageUrl;

        _context.MembershipTypes.Update(membershipType);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get the necessary information for deleting a membership type.
    /// </summary>
    public async Task<DeleteMembershipTypeViewModel?> GetMembershipTypeForDeleteAsync(int id)
	{
		return await _context.MembershipTypes
		.Where(m => m.Id == id)
		.Select(m => new DeleteMembershipTypeViewModel
		{
			Id = m.Id,
			Name = m.Name,
			Description = m.Description
		})
		.FirstOrDefaultAsync();
	}

    /// <summary>
    /// Delete a membership type from the database.
    /// </summary>
    public async Task DeleteMembershipTypeAsync(int id, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null || !await _userManager.IsInRoleAsync(user, AdminRole))
        {
            throw new InvalidOperationException(YouAreNotAuthorizedToDelete);
        }

        var membershipType = await _context.MembershipTypes.FindAsync(id);

        if (membershipType == null)
        {
            throw new InvalidOperationException(MembershipTypeDoesNotExist);
        }

        _context.MembershipTypes.Remove(membershipType);
        await _context.SaveChangesAsync();
    }
}