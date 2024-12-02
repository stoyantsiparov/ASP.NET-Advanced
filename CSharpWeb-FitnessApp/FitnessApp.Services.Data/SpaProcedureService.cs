using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.SpaProcedureViewModels;
using Microsoft.EntityFrameworkCore;
using static FitnessApp.Common.ErrorMessages.SpaProcedure;
using static FitnessApp.Common.EntityValidationConstants.SpaProcedure;

namespace FitnessApp.Services.Data;
public class SpaProcedureService : ISpaProcedureService
{
    private readonly ApplicationDbContext _context;

    public SpaProcedureService(ApplicationDbContext context)
    {
        _context = context;
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
                AppointmentDateTime = sr.SpaProcedure.AppointmentDateTime
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
}