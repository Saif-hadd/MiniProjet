using Microsoft.EntityFrameworkCore;
using MiniProjet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProjet.Services
{
    public interface IInterventionService
    {
        Task<Intervention> CreateInterventionAsync(Intervention model);
        Task<List<Intervention>> GetInterventionsAsync();
        Task<Intervention> GetInterventionByIdAsync(int id);
        Task<bool> UpdateInterventionAsync(Intervention model);
        Task<bool> DeleteInterventionAsync(int id);
    }

    public class InterventionService : IInterventionService
    {
        private readonly ApplicationDbContext _context;

        public InterventionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Intervention> CreateInterventionAsync(Intervention model)
        {
            _context.Interventions.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<Intervention>> GetInterventionsAsync()
        {
            return await _context.Interventions
                .Include(i => i.Technicien)
                .Include(i => i.Reclamation)
                .Include(i => i.Article)
                .ToListAsync();
        }

        public async Task<Intervention> GetInterventionByIdAsync(int id)
        {
            return await _context.Interventions
                .Include(i => i.Technicien)
                .Include(i => i.Reclamation)
                .Include(i => i.Article)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> UpdateInterventionAsync(Intervention model)
        {
            var intervention = await _context.Interventions.FindAsync(model.Id);
            if (intervention == null)
                return false;

            intervention.IsUnderWarranty = model.IsUnderWarranty;
            intervention.TechnicienId = model.TechnicienId;
            intervention.ReclamationId = model.ReclamationId;
            intervention.ArticleId = model.ArticleId;
            intervention.Date = model.Date;
            intervention.TotalCost = model.TotalCost;

            _context.Interventions.Update(intervention);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteInterventionAsync(int id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention == null)
                return false;

            _context.Interventions.Remove(intervention);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
