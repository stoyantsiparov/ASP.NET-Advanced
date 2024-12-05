using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.ClassViewModels;
using FitnessApp.Web.ViewModels.InstructorViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.ApplicationsConstants;
using static FitnessApp.Common.EntityValidationConstants.Class;
using static FitnessApp.Common.SuccessfulValidationMessages.Class;
using static FitnessApp.Common.ErrorMessages.Class;
using static FitnessApp.Common.ErrorMessages.Roles;

namespace FitnessApp.Services.Data;

public class ClassService : IClassService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ClassService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
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
                Schedule = c.Schedule.ToString(ScheduleDateTimeFormat),
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
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                ImageUrl = c.ImageUrl,
                Schedule = c.Schedule.ToString(ScheduleDateTimeFormat),
                Duration = c.Duration,
                InstructorId = c.InstructorId
            })
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
                Description = c.Description,
                Price = c.Price,
                ImageUrl = c.ImageUrl,
                Schedule = c.Schedule.ToString(ScheduleDateTimeFormat),
                Duration = c.Duration,
                Instructor = new InstructorViewModel
                {
                    Id = c.Instructor.Id,
                    FirstName = c.Instructor.FirstName,
                    LastName = c.Instructor.LastName,
                    ImageUrl = c.Instructor.ImageUrl,
                    Bio = c.Instructor.Bio,
                    Specialization = c.Instructor.Specialization
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
                Schedule = cr.Class.Schedule.ToString(ScheduleDateTimeFormat),
                Duration = cr.Class.Duration
            })
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Add class to user's classes
    /// </summary>
    public async Task AddToMyClassesAsync(string userId, ClassesViewModel? classesViewModel)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException(UserIdCannotBeEmpty, nameof(userId));
        }

        if (classesViewModel == null)
        {
            throw new ArgumentNullException(nameof(classesViewModel), ClassViewModelCannotBeNull);
        }

        var classEntity = await _context.Classes.FindAsync(classesViewModel.Id);

        if (classEntity == null)
        {
            throw new InvalidOperationException(FitnessClassDoesNotExist);
        }

        var existingRegistration = await _context.ClassesRegistrations
            .FirstOrDefaultAsync(cr => cr.MemberId == userId && cr.ClassId == classesViewModel.Id);

        if (existingRegistration != null)
        {
            throw new InvalidOperationException(AlreadyRegisteredForClass);
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains(MemberRole))
            {
                throw new InvalidOperationException(OnlyMembersCanRegisterForThisClass);
            }
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
            .FirstOrDefaultAsync(cr => classesViewModel != null && cr.MemberId == userId && cr.ClassId == classesViewModel.Id);

        if (classRegistration == null)
        {
            throw new InvalidOperationException(UserNotRegisteredForClass);
        }

        _context.ClassesRegistrations.Remove(classRegistration);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get class for add
    /// </summary>
    public async Task<AddClassViewModel> GetClassForAddAsync()
    {
        var instructors = await _context.Instructors
            .Select(i => new InstructorViewModel
            {
                Id = i.Id,
                Specialization = i.Specialization
            })
            .AsNoTracking()
            .ToListAsync();

        var model = new AddClassViewModel
        {
            Instructors = instructors
        };

        return model;
    }

    /// <summary>
    /// Add class
    /// </summary>
    public async Task AddClassAsync(AddClassViewModel model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains(AdminRole))
            {
                throw new UnauthorizedAccessException(YouAreNotAuthorizedToAdd);
            }
        }

        if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Schedule))
        {
            throw new ArgumentException(ClassNameAndScheduleAreRequired);
        }

        DateTime schedule;
        if (!DateTime.TryParse(model.Schedule, out schedule))
        {
            throw new ArgumentException(InvalidScheduleFormat);
        }

        if (await _context.Classes.AnyAsync(c => c.Name == model.Name && c.Schedule == schedule))
        {
            throw new InvalidOperationException(ClassWithTheSameNameAndScheduleAlreadyExists);
        }

        Class classEntity = new Class
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            ImageUrl = model.ImageUrl,
            Schedule = schedule,
            Duration = model.Duration,
            InstructorId = model.InstructorId
        };

        await _context.Classes.AddAsync(classEntity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Edit class
    /// </summary>
    public async Task EditClassAsync(ClassesViewModel model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains(AdminRole))
            {
                throw new UnauthorizedAccessException(YouAreNotAuthorizedToEdit);
            }
        }

        var classEntity = await _context.Classes.FirstOrDefaultAsync(c => c.Id == model.Id);

        if (classEntity != null)
        {
            classEntity.Name = model.Name;
            classEntity.ImageUrl = model.ImageUrl;
            classEntity.Description = model.Description;
            classEntity.Price = model.Price;
            classEntity.Schedule = DateTime.Parse(model.Schedule);
            classEntity.Duration = model.Duration;
            classEntity.InstructorId = model.InstructorId;

            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Get class for delete
    /// </summary>
    public async Task<DeleteClassViewModel?> GetClassForDeleteAsync(int id)
    {
        return await _context.Classes
            .Where(c => c.Id == id)
            .Select(c => new DeleteClassViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Delete class
    /// </summary>
    public async Task DeleteClassAsync(int id, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains(AdminRole))
            {
                throw new UnauthorizedAccessException(YouAreNotAuthorizedToDelete);
            }
        }

        var classEntity = await _context.Classes.FindAsync(id);

        if (classEntity != null)
        {
            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync();
        }
    }
}