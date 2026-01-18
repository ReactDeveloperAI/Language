using Microsoft.EntityFrameworkCore;
using MultilingualMenuApp.Data;
using MultilingualMenuApp.Models;

namespace MultilingualMenuApp.Services
{
    /// <summary>
    /// Service implementation for language operations
    /// </summary>
    public class LanguageService : ILanguageService
    {
        private readonly ApplicationDbContext _context;

        public LanguageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Language>> GetAllLanguagesAsync()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task<List<Language>> GetActiveLanguagesAsync()
        {
            return await _context.Languages
                .Where(l => l.IsActive)
                .ToListAsync();
        }

        public async Task<Language?> GetLanguageByIdAsync(int id)
        {
            return await _context.Languages.FindAsync(id);
        }

        public async Task<Language?> GetLanguageByCodeAsync(string code)
        {
            return await _context.Languages
                .FirstOrDefaultAsync(l => l.Code == code);
        }

        public async Task<Language> CreateLanguageAsync(Language language)
        {
            _context.Languages.Add(language);
            await _context.SaveChangesAsync();
            return language;
        }

        public async Task UpdateLanguageAsync(Language language)
        {
            _context.Entry(language).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLanguageAsync(int id)
        {
            var language = await _context.Languages.FindAsync(id);
            if (language != null)
            {
                _context.Languages.Remove(language);
                await _context.SaveChangesAsync();
            }
        }
    }
}
