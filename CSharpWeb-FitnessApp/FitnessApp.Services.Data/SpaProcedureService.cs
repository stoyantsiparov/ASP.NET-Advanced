using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.SpaProcedureViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.ApplicationsConstants;
using static FitnessApp.Common.EntityValidationConstants.SpaProcedure;
using static FitnessApp.Common.ErrorMessages.SpaProcedure;
using static FitnessApp.Common.ErrorMessages.Roles;

namespace FitnessApp.Services.Data;

public class SpaProcedureService : ISpaProcedureService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;


    public SpaProcedureService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Get all spa procedures with pagination
    /// </summary>
    public async Task<PaginatedSpaProceduresViewModel> GetAllSpaProceduresPaginationAsync(string? searchQuery, int pageNumber, int pageSize)
    {
        var query = _context.SpaProcedures.AsQueryable();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            query = query.Where(sp => sp.Name.Contains(searchQuery) || sp.Description.Contains(searchQuery));
        }

        var totalProcedures = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalProcedures / (double)pageSize);

        List<AllSpaProceduresViewModel> spaProcedures = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(sp => new AllSpaProceduresViewModel
            {
                Id = sp.Id,
                Name = sp.Name,
                Description = sp.Description,
                ImageUrl = sp.ImageUrl,
                AppointmentDateTime = sp.AppointmentDateTime.ToString(AppointmentDateTimeFormat)
            })
            .AsNoTracking()
            .ToListAsync();

        var model = new PaginatedSpaProceduresViewModel
        {
            SpaProcedures = spaProcedures,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            SearchQuery = searchQuery
        };

        return model;
    }

    /// <summary>
    /// Get all spa procedures
    /// </summary>
    public async Task<IEnumerable<AllSpaProceduresViewModel>> GetAllSpaProceduresAsync()
    {
        return await _context.SpaProcedures
            .Select(sp => new AllSpaProceduresViewModel
            {
                Id = sp.Id,
                Name = sp.Name,
                ImageUrl = sp.ImageUrl,
                Description = sp.Description
            })
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Get spa procedure by id
    /// </summary>
    public async Task<SpaProceduresViewModel?> GetSpaProceduresByIdAsync(int id)
    {
        return await _context.SpaProcedures
            .Where(sp => sp.Id == id)
            .Select(sp => new SpaProceduresViewModel
            {
                Id = sp.Id,
                Name = sp.Name,
                ImageUrl = sp.ImageUrl,
                Description = sp.Description,
                Price = sp.Price,
                Duration = sp.Duration,
                AppointmentDateTime = sp.AppointmentDateTime.ToString(AppointmentDateTimeFormat)
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get spa procedure details
    /// </summary>
    public async Task<SpaProceduresDetailsViewModel?> GetSpaProceduresDetailsAsync(int id)
    {
        return await _context.SpaProcedures
            .Where(x => x.Id == id)
            .Select(x => new SpaProceduresDetailsViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Description = x.Description,
                Price = x.Price,
                Duration = x.Duration,
                AppointmentDateTime = null
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get user's spa procedures
    /// </summary>
    public async Task<IEnumerable<AllSpaProceduresViewModel>> GetMySpaProceduresAsync(string userId)
    {
        return await _context.SpaRegistrations
            .Where(sr => sr.MemberId == userId)
            .Select(sr => new AllSpaProceduresViewModel
            {
                Id = sr.SpaProcedureId,
                Name = sr.SpaProcedure.Name,
                ImageUrl = sr.SpaProcedure.ImageUrl,
                Description = sr.SpaProcedure.Description,
                AppointmentDateTime = sr.SpaProcedure.AppointmentDateTime.ToString(AppointmentDateTimeFormat)
            })
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Add spa procedure to user's appointments
    /// </summary>
    public async Task AddToMySpaAppointmentsAsync(string userId, SpaProceduresViewModel spaProcedure, DateTime appointmentDateTime)
    {
        if (appointmentDateTime < DateTime.Now)
        {
            throw new InvalidOperationException(PastAppointmentDate);
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null || !await _userManager.IsInRoleAsync(user, MemberRole))
        {
            throw new InvalidOperationException(OnlyMembersCanBookSpaProcedures);
        }

        var existingRegistration = await _context.SpaRegistrations
            .FirstOrDefaultAsync(sr => sr.MemberId == userId && sr.SpaProcedureId == spaProcedure.Id);

        if (existingRegistration != null)
        {
            throw new InvalidOperationException(AlreadyBookedAppointment);
        }

        var spaRegistration = new SpaRegistration
        {
            MemberId = userId,
            SpaProcedureId = spaProcedure.Id
        };

        await _context.SpaRegistrations.AddAsync(spaRegistration);
        await _context.SaveChangesAsync();

        var procedureToUpdate = await _context.SpaProcedures
            .FirstOrDefaultAsync(sp => sp.Id == spaProcedure.Id);

        if (procedureToUpdate != null)
        {
            procedureToUpdate.AppointmentDateTime = appointmentDateTime;
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Remove spa procedure from user's appointments
    /// </summary>
    public async Task RemoveFromMySpaAppointmentsAsync(string userId, SpaProceduresViewModel spaProcedure)
    {
        var registration = await _context.SpaRegistrations
            .FirstOrDefaultAsync(sr => sr.MemberId == userId && sr.SpaProcedureId == spaProcedure.Id);

        if (registration == null)
        {
            throw new InvalidOperationException(SpaAppointmentNotBooked);
        }

        _context.SpaRegistrations.Remove(registration);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get spa procedure for add
    /// </summary>
    public async Task<AddSpaProcedureViewModel> GetSpaProcedureForAddAsync()
    {
        var model = new AddSpaProcedureViewModel
        {
            Name = string.Empty,
            ImageUrl = string.Empty,
            Description = string.Empty,
            Duration = 0,
            Price = 0.0m,
        };

        return await Task.FromResult(model);
    }

    /// <summary>
    /// Add spa procedure
    /// </summary>
    public async Task AddSpaProcedureAsync(AddSpaProcedureViewModel model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var isAdmin = user != null && await _userManager.IsInRoleAsync(user, AdminRole);

        if (!isAdmin)
        {
            throw new UnauthorizedAccessException(YouAreNotAuthorizedToAdd);
        }

        var spaProcedure = new SpaProcedure
        {
            Name = model.Name,
            ImageUrl = model.ImageUrl,
            Description = model.Description,
            Duration = model.Duration,
            Price = model.Price,
        };

        await _context.SpaProcedures.AddAsync(spaProcedure);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Edit spa procedure
    /// </summary>
    public async Task EditSpaProcedureAsync(SpaProceduresViewModel model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var isAdmin = user != null && await _userManager.IsInRoleAsync(user, AdminRole);

        if (!isAdmin)
        {
            throw new UnauthorizedAccessException(YouAreNotAuthorizedToEdit);
        }

        var spaProcedure = await _context.SpaProcedures.FindAsync(model.Id);

        if (spaProcedure == null)
        {
            throw new InvalidOperationException(SpaProcedureNotFound);
        }

        spaProcedure.Name = model.Name;
        spaProcedure.ImageUrl = model.ImageUrl;
        spaProcedure.Description = model.Description;
        spaProcedure.Duration = model.Duration;
        spaProcedure.Price = model.Price;

        _context.SpaProcedures.Update(spaProcedure);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get spa procedure for delete
    /// </summary>
    public async Task<DeleteSpaProcedureViewModel?> GetSpaProcedureForDeleteAsync(int id)
    {
        return await _context.SpaProcedures
            .Where(sp => sp.Id == id)
            .Select(sp => new DeleteSpaProcedureViewModel
            {
                Id = sp.Id,
                Name = sp.Name,
                Description = sp.Description
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Delete spa procedure
    /// </summary>
    public async Task DeleteSpaProcedureAsync(int id, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var isAdmin = user != null && await _userManager.IsInRoleAsync(user, AdminRole);

        if (!isAdmin)
        {
            throw new UnauthorizedAccessException(YouAreNotAuthorizedToDelete);
        }

        var spaProcedure = await _context.SpaProcedures.FirstOrDefaultAsync(sp => sp.Id == id);

        if (spaProcedure == null)
        {
            throw new InvalidOperationException(SpaProcedureNotFound);
        }

        _context.SpaProcedures.Remove(spaProcedure);
        await _context.SaveChangesAsync();
    }
}