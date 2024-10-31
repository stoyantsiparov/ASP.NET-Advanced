using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.SpaProcedures;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Services.Data;

public class SpaProcedureService : ISpaProcedureService
{
    private readonly ApplicationDbContext _context;

    public SpaProcedureService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AllSpaProceduresViewModel>> GetAllSpaProceduresAsync()
    {
        return await _context.SpaProcedures
            .Select(sp => new AllSpaProceduresViewModel
            {
                Id = sp.Id,
                Name = sp.Name,
                ImageUrl = sp.ImageUrl,
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<AllSpaProceduresViewModel>> GetMySpaProceduresAsync(string userId)
    {
        return await _context.SpaRegistrations
            .Where(sr => sr.MemberId == userId)
            .Select(sr => new AllSpaProceduresViewModel
            {
                Id = sr.SpaProcedureId,
                Name = sr.SpaProcedure.Name,
                ImageUrl = sr.SpaProcedure.ImageUrl
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<SpaProceduresViewModel?> GetSpaProceduresByIdAsync(int id)
    {
        return await _context.SpaProcedures
            .Where(x => x.Id == id)
            .Select(x => new SpaProceduresViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

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
                Duration = x.Duration
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task AddToMySpaAppointmentsAsync(string userId, SpaProceduresViewModel spaProcedures)
    {
        bool isProcedureRegistered = await _context.SpaRegistrations
            .AnyAsync(sr => sr.MemberId == userId && sr.SpaProcedureId == spaProcedures.Id);

        if (isProcedureRegistered == false)
        {
            var spaRegistration = new SpaRegistration
            {
                MemberId = userId,
                SpaProcedureId = spaProcedures.Id
            };

            await _context.SpaRegistrations.AddAsync(spaRegistration);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveFromMySpaAppointmentsAsync(string userId, SpaProceduresViewModel spaProcedures)
    {
        var isProcedureRegistered = await _context.SpaRegistrations
            .FirstOrDefaultAsync(sr => sr.MemberId == userId && sr.SpaProcedureId == spaProcedures.Id);

        if (isProcedureRegistered != null)
        {
            _context.SpaRegistrations.Remove(isProcedureRegistered);
            await _context.SaveChangesAsync();
        }
    }
}