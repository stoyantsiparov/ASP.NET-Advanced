using FitnessApp.Data;
using FitnessApp.Data.Models;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.SpaProcedures;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Services.Data
{
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
                    Description = sp.Description
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<string>> GetTreatmentDaysAsync()
        {
            return await Task.FromResult(new List<string> { "Saturday", "Sunday" });
        }

        public async Task<SpaProceduresDetailsViewModel?> GetSpaProceduresDetailsAsync(int id)
        {
            var treatmentDaysOptions = new List<string> { "Saturday", "Sunday" };

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
                    TreatmentDaysOptions = treatmentDaysOptions
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<SpaProcedure?> GetSpaProceduresByIdAsync(int id)
        {
            return await _context.SpaProcedures.FindAsync(id);
        }

        public async Task<IEnumerable<AllSpaProceduresViewModel>> GetMySpaProceduresAsync(string userId)
        {
            return await _context.SpaRegistrations
                .Where(sr => sr.MemberId == userId)
                .Select(sr => new AllSpaProceduresViewModel
                {
                    Id = sr.SpaProcedureId,
                    Name = sr.SpaProcedure.Name,
                    ImageUrl = sr.SpaProcedure.ImageUrl,
                    TreatmentDay = sr.TreatmentDay,
                    Description = sr.SpaProcedure.Description
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddToMySpaAppointmentsAsync(string userId, SpaProcedure spaProcedure, string treatmentDay)
        {
            var spaRegistration = new SpaRegistration
            {
                MemberId = userId,
                SpaProcedureId = spaProcedure.Id,
                TreatmentDay = treatmentDay
            };

            await _context.SpaRegistrations.AddAsync(spaRegistration);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromMySpaAppointmentsAsync(string userId, SpaProcedure spaProcedure)
        {
            var registration = await _context.SpaRegistrations
                .FirstOrDefaultAsync(sr => sr.MemberId == userId && sr.SpaProcedureId == spaProcedure.Id);

            if (registration != null)
            {
                _context.SpaRegistrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
        }
    }
}