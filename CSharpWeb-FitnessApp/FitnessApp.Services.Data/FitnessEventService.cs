using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.FitnessEvents;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.FitnessEvent;

namespace FitnessApp.Services.Data;

public class FitnessEventService : IFitnessEventService
{
	private readonly ApplicationDbContext _context;

	public FitnessEventService(ApplicationDbContext context)
	{
		_context = context;
	}

	/// <summary>
	/// Get all fitness events
	/// </summary>
	public async Task<IEnumerable<AllFitnessEventsViewModel>> GetAllFitnessEventsAsync()
	{
		return await _context.FitnessEvents
			.Select(e => new AllFitnessEventsViewModel
			{
				Id = e.Id,
				Title = e.Title,
				ImageUrl = e.ImageUrl,
				Location = e.Location,
				StartDateTime = e.StartDate,
				EndDateTime = e.EndDate
			})
			.AsNoTracking()
			.ToListAsync();
	}

	/// <summary>
	/// Get fitness event by id
	/// </summary>
	public async Task<FitnessEventViewModel?> GetFitnessEventByIdAsync(int id)
	{
		return await _context.FitnessEvents
			.Where(e => e.Id == id)
			.Select(e => new FitnessEventViewModel
			{
				Id = e.Id,
				Title = e.Title
			})
			.FirstOrDefaultAsync();
	}

	/// <summary>
	/// Get fitness event details
	/// </summary>
	public async Task<FitnessEventDetailsViewModel?> GetFitnessEventDetailsAsync(int id)
	{
		return await _context.FitnessEvents
			.Where(e => e.Id == id)
			.Select(e => new FitnessEventDetailsViewModel
			{
				Id = e.Id,
				Title = e.Title,
				ImageUrl = e.ImageUrl,
				Location = e.Location,
				Description = e.Description,
				StartDateTime = e.StartDate.ToString(DateTimeFormat, System.Globalization.CultureInfo.InvariantCulture),
				EndDateTime = e.EndDate.ToString(DateTimeFormat, System.Globalization.CultureInfo.InvariantCulture)
			})
			.AsNoTracking()
			.FirstOrDefaultAsync();
	}

	/// <summary>
	/// Get user's fitness events
	/// </summary>
	public async Task<IEnumerable<AllFitnessEventsViewModel>> GetMyFitnessEventsAsync(string userId)
	{
		return await _context.EventRegistrations
			.Where(r => r.MemberId == userId)
			.Select(r => new AllFitnessEventsViewModel
			{
				Id = r.FitnessEvent.Id,
				Title = r.FitnessEvent.Title,
				ImageUrl = r.FitnessEvent.ImageUrl,
				Location = r.FitnessEvent.Location,
				StartDateTime = r.FitnessEvent.StartDate,
				EndDateTime = r.FitnessEvent.EndDate
			})
			.AsNoTracking()
			.ToListAsync();
	}

	/// <summary>
	/// Add fitness event to user's fitness events
	/// </summary>
	public async Task AddToMyFitnessEventsAsync(string userId, FitnessEventViewModel? fitnessEventViewModel, DateTime appointmentDateTime)
	{
		var fitnessEvent = await _context.FitnessEvents.FindAsync(fitnessEventViewModel.Id);

		if (fitnessEvent == null)
		{
			throw new InvalidOperationException("The specified event does not exist.");
		}

		var existingRegistration = await _context.EventRegistrations
			.FirstOrDefaultAsync(er => er.MemberId == userId && er.EventId == fitnessEventViewModel.Id);

		if (existingRegistration != null)
		{
			throw new InvalidOperationException("You have already signed up for this event.");
		}

		var eventRegistration = new EventRegistration
		{
			MemberId = userId,
			EventId = fitnessEventViewModel.Id
		};

		await _context.EventRegistrations.AddAsync(eventRegistration);
		await _context.SaveChangesAsync();
	}

	/// <summary>
	/// Remove fitness event from user's fitness events
	/// </summary>
	public async Task RemoveFromMyFitnessEventsAsync(string userId, FitnessEventViewModel? fitnessEventViewModel)
	{
		var registration = await _context.EventRegistrations
			.FirstOrDefaultAsync(er => er.MemberId == userId && er.EventId == fitnessEventViewModel.Id);

		if (registration == null)
		{
			throw new InvalidOperationException("You are not registered for this event.");
		}

		_context.EventRegistrations.Remove(registration);
		await _context.SaveChangesAsync();
	}
}