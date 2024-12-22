using Microsoft.EntityFrameworkCore;
using MiniProjet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProjet.Services
{
    public interface IArticleService
    {
        Task<Article> CreateArticleAsync(Article model);
        Task<List<Article>> GetArticlesAsync();
        Task<Article> GetArticleByIdAsync(int id);
        Task<bool> UpdateArticleAsync(Article model);
        Task<bool> DeleteArticleAsync(int id);
    }

    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Article> CreateArticleAsync(Article model)
        {
            _context.Articles.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<Article>> GetArticlesAsync()
        {
            return await _context.Articles
                .Include(a => a.Reclamations)
                .Include(a => a.Interventions)
                .Include(a => a.PiecesDeRechange)
                .ToListAsync();
        }

        public async Task<Article> GetArticleByIdAsync(int id)
        {
            return await _context.Articles
                .Include(a => a.Reclamations)
                .Include(a => a.Interventions)
                .Include(a => a.PiecesDeRechange)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> UpdateArticleAsync(Article model)
        {
            var article = await _context.Articles.FindAsync(model.Id);
            if (article == null) return false;

            article.Name = model.Name;
            article.IsUnderWarranty = model.IsUnderWarranty;

            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return false;

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
