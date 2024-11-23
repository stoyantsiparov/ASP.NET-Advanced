using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.ClassViewModels;
using FitnessApp.Web.ViewModels.InstructorViewModels;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.EntityValidationConstants.Class;

namespace FitnessApp.Services.Data;

public class ClassService : IClassService
{
	private readonly ApplicationDbContext _context;

	public ClassService(ApplicationDbContext context)
	{
		_context = context;
	}

	/// <summary>
	/// Get all classes
	/// </summary>
	public async Task<IEnumerable<AllClassesViewModel>> GetAllClassesAsync()
	{
		return await _context.Classes
			.Select(c => new AllClassesViewModel
			{
				Id = c.Id,
				Name = c.Name,
				ImageUrl = c.ImageUrl,
				Schedule = c.Schedule,
				Duration = c.Duration
			})
			.AsNoTracking()
			.ToListAsync();
	}

	/// <summary>
	/// Get class by ID
	/// </summary>
	public async Task<ClassesViewModel?> GetClassByIdAsync(int id)
	{
		return await _context.Classes
			.Where(c => c.Id == id)
			.Select(c => new ClassesViewModel
			{
				Id = c.Id,
				Name = c.Name
			})
			.AsNoTracking()
			.FirstOrDefaultAsync();
	}

	/// <summary>
	/// Get class details
	/// </summary>
	public async Task<ClassesDetailsViewModel?> GetClassDetailsAsync(int id)
	{
		return await _context.Classes
			.Where(c => c.Id == id)
			.Select(c => new ClassesDetailsViewModel
			{
				Id = c.Id,
				Name = c.Name,
				ImageUrl = c.ImageUrl,
				Schedule = c.Schedule.ToString(ScheduleDateTimeFormat, System.Globalization.CultureInfo.InvariantCulture),
				Duration = c.Duration,
				Instructor = new InstructorViewModel
				{
					FirstName = c.Instructor.FirstName,
					LastName = c.Instructor.LastName,
                    Bio = c.Instructor.Bio,
                    Specialization = c.Instructor.Specialization,
					ImageUrl = c.Instructor.ImageUrl
				}
			})
			.FirstOrDefaultAsync();
	}

	/// <summary>
	/// Get user's classes
	/// </summary>
	public async Task<IEnumerable<AllClassesViewModel>> GetMyClassesAsync(string userId)
	{
		return await _context.ClassesRegistrations
			.Where(cr => cr.MemberId == userId)
			.Select(cr => new AllClassesViewModel
			{
				Id = cr.Class.Id,
				Name = cr.Class.Name,
				ImageUrl = cr.Class.ImageUrl,
				Schedule = cr.Class.Schedule,
				Duration = cr.Class.Duration
			})
			.AsNoTracking()
			.ToListAsync();
	}

	/// <summary>
	/// Add class to user's classes
	/// </summary>
	public async Task AddToMyClassesAsync(string userId, ClassesViewModel? classesViewModel, DateTime appointmentDateTime)
	{
		var classEntity = await _context.Classes.FindAsync(classesViewModel.Id);

		if (classEntity == null)
		{
			throw new InvalidOperationException("The specified class does not exist.");
		}

		var existingRegistration = await _context.ClassesRegistrations
			.FirstOrDefaultAsync(cr => cr.MemberId == userId && cr.ClassId == classesViewModel.Id);

		if (existingRegistration != null)
		{
			throw new InvalidOperationException("You have already signed up for this class.");
		}

		var classRegistration = new ClassRegistration
		{
			MemberId = userId,
			ClassId = classesViewModel.Id
		};

		await _context.ClassesRegistrations.AddAsync(classRegistration);
		await _context.SaveChangesAsync();
	}

	/// <summary>
	/// Remove class from user's classes
	/// </summary>
	public async Task RemoveFromMyClassesAsync(string userId, ClassesViewModel? classesViewModel)
	{
		var classRegistration = await _context.ClassesRegistrations
			.FirstOrDefaultAsync(cr => cr.MemberId == userId && cr.ClassId == classesViewModel.Id);

		if (classRegistration == null)
		{
			throw new InvalidOperationException("You are not registered for this class.");
		}

		_context.ClassesRegistrations.Remove(classRegistration);
		await _context.SaveChangesAsync();
	}
}