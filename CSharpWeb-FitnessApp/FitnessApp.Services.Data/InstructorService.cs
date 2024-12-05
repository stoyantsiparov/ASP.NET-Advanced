using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.InstructorViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.ApplicationsConstants;
using static FitnessApp.Common.ErrorMessages.Instructor;
using static FitnessApp.Common.ErrorMessages.Roles;

namespace FitnessApp.Services.Data;

public class InstructorService : IInstructorService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public InstructorService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Get all instructors.
    /// </summary>
    public async Task<IEnumerable<AllInstructorsViewModel>> GetAllInstructorsAsync()
    {
        return await _context.Instructors
            .Select(i => new AllInstructorsViewModel
            {
                Id = i.Id,
                FirstName = i.FirstName,
                LastName = i.LastName,
                ImageUrl = i.ImageUrl,
                Specialization = i.Specialization
            })
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Get instructor by id.
    /// </summary>
    public async Task<InstructorViewModel?> GetInstructorByIdAsync(int id)
    {
        return await _context.Instructors
            .Where(i => i.Id == id)
            .Select(i => new InstructorViewModel
            {
                Id = i.Id,
                FirstName = i.FirstName,
                LastName = i.LastName,
                ImageUrl = i.ImageUrl,
                Bio = i.Bio,
                Specialization = i.Specialization
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get instructor details by id.
    /// </summary>
    public async Task<InstructorDetailsViewModel?> GetInstructorDetailsAsync(int id)
    {
        return await _context.Instructors
            .Where(i => i.Id == id)
            .Select(i => new InstructorDetailsViewModel
            {
                Id = i.Id,
                FirstName = i.FirstName,
                LastName = i.LastName,
                ImageUrl = i.ImageUrl,
                Bio = i.Bio,
                Specialization = i.Specialization
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get instructor for add.
    /// </summary>
    public async Task<AddInstructorViewModel> GetInstructorForAddAsync()
    {
        var model = new AddInstructorViewModel
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            ImageUrl = string.Empty,
            Bio = string.Empty,
            Specialization = string.Empty
        };

        return await Task.FromResult(model);
    }

    /// <summary>
    /// Add instructor.
    /// </summary>
    public async Task AddInstructorAsync(AddInstructorViewModel model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || !await _userManager.IsInRoleAsync(user, AdminRole))
        {
            throw new InvalidOperationException(YouAreNotAuthorizedToAdd);
        }

        if (model == null)
        {
            throw new ArgumentNullException(InstructorViewModelCannotBeNull);
        }

        var instructor = new Instructor
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            ImageUrl = model.ImageUrl,
            Bio = model.Bio,
            Specialization = model.Specialization,
        };

        await _context.Instructors.AddAsync(instructor);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Edit instructor.
    /// </summary>
    public async Task EditInstructorAsync(InstructorViewModel model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || !await _userManager.IsInRoleAsync(user, AdminRole))
        {
            throw new InvalidOperationException(YouAreNotAuthorizedToEdit);
        }

        if (model == null)
        {
            throw new ArgumentNullException(InstructorViewModelCannotBeNull);
        }

        var instructor = await _context.Instructors.FindAsync(model.Id);

        if (instructor == null)
        {
            throw new InvalidOperationException(InstructorNotFound);
        }

        instructor.FirstName = model.FirstName;
        instructor.LastName = model.LastName;
        instructor.ImageUrl = model.ImageUrl;
        instructor.Bio = model.Bio;
        instructor.Specialization = model.Specialization;

        _context.Instructors.Update(instructor);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get instructor for delete.
    /// </summary>
    public async Task<DeleteInstructorViewModel?> GetInstructorForDeleteAsync(int id)
    {
        return await _context.Instructors
            .Where(i => i.Id == id)
            .Select(i => new DeleteInstructorViewModel
            {
                Id = i.Id,
                FirstName = i.FirstName,
                LastName = i.LastName,
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Delete instructor.
    /// </summary>
    public async Task DeleteInstructorAsync(int id, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || !await _userManager.IsInRoleAsync(user, AdminRole))
        {
            throw new InvalidOperationException(YouAreNotAuthorizedToDelete);
        }

        var instructor = await _context.Instructors.FirstOrDefaultAsync(i => i.Id == id);

        if (instructor == null)
        {
            throw new InvalidOperationException(InstructorNotFound);
        }

        _context.Instructors.Remove(instructor);
        await _context.SaveChangesAsync();
    }
}