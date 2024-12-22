using Microsoft.EntityFrameworkCore;
using MiniProjet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProjet.Services
{
    public interface IReclamationService
    {
        Task<ReclamationModel> CreateReclamationAsync(ReclamationModel model);
        Task<List<ReclamationModel>> GetReclamationsAsync();
        Task<ReclamationModel> GetReclamationByIdAsync(int id);
        Task<bool> UpdateReclamationAsync(ReclamationModel model);
        Task<bool> DeleteReclamationAsync(int id);
    }

    public class ReclamationService : IReclamationService
    {
        private readonly ApplicationDbContext _context;

        public ReclamationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReclamationModel> CreateReclamationAsync(ReclamationModel model)
        {
            _context.Reclamations.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<ReclamationModel>> GetReclamationsAsync()
        {
            return await _context.Reclamations
                .Include(r => r.Client)          // Inclure les détails du Client
                .Include(r => r.Interventions)   // Inclure les interventions associées
                .ToListAsync();
        }

        public async Task<ReclamationModel> GetReclamationByIdAsync(int id)
        {
            return await _context.Reclamations
                .Include(r => r.Client)          // Inclure le Client
                .Include(r => r.Interventions)   // Inclure les Interventions
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> UpdateReclamationAsync(ReclamationModel model)
        {
            var reclamation = await _context.Reclamations.FindAsync(model.Id);
            if (reclamation == null) return false;

            // Mise à jour des propriétés
            reclamation.Description = model.Description;
            reclamation.IsResolved = model.IsResolved;
            reclamation.ClientId = model.ClientId;
            reclamation.DateCreated = model.DateCreated;

            _context.Reclamations.Update(reclamation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReclamationAsync(int id)
        {
            var reclamation = await _context.Reclamations.FindAsync(id);
            if (reclamation == null) return false;

            _context.Reclamations.Remove(reclamation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
