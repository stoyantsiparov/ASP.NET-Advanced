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
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get spa procedure by id
        /// </summary>
        // TODO: Implement GetSpaProceduresByIdAsync (SpaProcedure) -> remove and add ViewModel
        public async Task<SpaProcedure?> GetSpaProceduresByIdAsync(int id)
        {
            return await _context.SpaProcedures.FindAsync(id);
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
        public async Task AddToMySpaAppointmentsAsync(string userId, SpaProcedure spaProcedure, DateTime appointmentDateTime)
        {
            var existingRegistration = await _context.SpaRegistrations
                .FirstOrDefaultAsync(sr => sr.MemberId == userId && sr.SpaProcedureId == spaProcedure.Id);

            if (existingRegistration != null)
            {
                throw new InvalidOperationException("This appointment has already been booked.");
            }

            var spaRegistration = new SpaRegistration
            {
                MemberId = userId,
                SpaProcedureId = spaProcedure.Id
            };

            await _context.SpaRegistrations.AddAsync(spaRegistration);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove spa procedure from user's appointments
        /// </summary>
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
