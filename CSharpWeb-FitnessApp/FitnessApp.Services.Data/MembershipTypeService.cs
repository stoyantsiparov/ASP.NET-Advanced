using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.MembershipTypeViewModels;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Services.Data;
public class MembershipTypeService : IMembershipTypeService
{
	private readonly ApplicationDbContext _context;

	public MembershipTypeService(ApplicationDbContext context)
	{
		_context = context;
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
		var membershipType = await _context.MembershipTypes.FindAsync(membershipTypeViewModel.Id);

		if (membershipType == null)
		{
			throw new InvalidOperationException("The membership type does not exist.");
		}

		var existingMembership = await _context.MembershipRegistrations
			.FirstOrDefaultAsync(r => r.MemberId == userId);

		if (existingMembership != null)
		{
			throw new InvalidOperationException("You can only have one membership type at a time.");
		}

		var registration = new MembershipRegistration
		{
			MemberId = userId,
			MembershipTypeId = membershipTypeViewModel.Id
		};

		await _context.MembershipRegistrations.AddAsync(registration);
		await _context.SaveChangesAsync();
	}

	/// <summary>
	/// Remove a membership type from the user's list of memberships.
	/// </summary>
	public async Task RemoveMyMembershipAsync(string userId, MembershipTypeViewModel? membershipTypeViewModel)
	{
		var registration = await _context.MembershipRegistrations
			.FirstOrDefaultAsync(r => r.MemberId == userId && r.MembershipTypeId == membershipTypeViewModel.Id);

		if (registration == null)
		{
			throw new InvalidOperationException("You are not subscribed to this membership type.");
		}

		_context.MembershipRegistrations.Remove(registration);
		await _context.SaveChangesAsync();
	}
}