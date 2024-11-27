using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.MembershipTypeViewModels;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Services.Data
{
	public class MembershipTypeService : IMembershipTypeService
	{
		private readonly ApplicationDbContext _context;

		public MembershipTypeService(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get all membership types
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
		/// Get membership type by id
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
		/// Get membership type details
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
		/// Get user's memberships
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
		/// Add membership type to user's list of memberships
		/// </summary>
		public async Task AddToMyMembershipTypesAsync(string userId, MembershipTypeViewModel? membershipTypeViewModel)
		{
			var membershipType = await _context.MembershipTypes.FindAsync(membershipTypeViewModel.Id);

			if (membershipType == null)
			{
				throw new InvalidOperationException("The membership type does not exist.");
			}

			var existingRegistration = await _context.MembershipRegistrations
				.FirstOrDefaultAsync(r => r.MemberId == userId && r.MembershipTypeId == membershipTypeViewModel.Id);

			if (existingRegistration != null)
			{
				throw new InvalidOperationException("You are already subscribed to this membership type.");
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
		/// Remove membership type from user's list of memberships
		/// </summary>
		public async Task RemoveFromMyMembershipTypesAsync(string userId, MembershipTypeViewModel? membershipTypeViewModel)
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
}