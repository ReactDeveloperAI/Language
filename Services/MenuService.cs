using Microsoft.EntityFrameworkCore;
using MultilingualMenuApp.Data;
using MultilingualMenuApp.Models;

namespace MultilingualMenuApp.Services
{
    /// <summary>
    /// Service implementation for menu operations
    /// </summary>
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;

        public MenuService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Menu>> GetAllMenusAsync()
        {
            return await _context.Menus
                .Include(m => m.Translations)
                .ToListAsync();
        }

        public async Task<Menu?> GetMenuByIdAsync(int id)
        {
            return await _context.Menus.FindAsync(id);
        }

        public async Task<Menu?> GetMenuWithTranslationsAsync(int id)
        {
            return await _context.Menus
                .Include(m => m.Translations)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Menu> CreateMenuAsync(Menu menu)
        {
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            _context.Entry(menu).State = EntityState.Modified;
            
            // Update translations
            var existingMenu = await _context.Menus
                .Include(m => m.Translations)
                .FirstOrDefaultAsync(m => m.Id == menu.Id);

            if (existingMenu != null)
            {
                existingMenu.DefaultName = menu.DefaultName;
                existingMenu.DefaultDescription = menu.DefaultDescription;

                // Remove translations that no longer exist
                var translationsToRemove = existingMenu.Translations
                    .Where(t => !menu.Translations.Any(mt => mt.Id == t.Id))
                    .ToList();
                
                foreach (var translation in translationsToRemove)
                {
                    _context.MenuTranslations.Remove(translation);
                }

                // Add or update translations
                foreach (var translation in menu.Translations)
                {
                    var existingTranslation = existingMenu.Translations
                        .FirstOrDefault(t => t.Id == translation.Id);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Language = translation.Language;
                        existingTranslation.Name = translation.Name;
                        existingTranslation.Description = translation.Description;
                    }
                    else
                    {
                        translation.MenuId = menu.Id;
                        existingMenu.Translations.Add(translation);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Menu>> GetMenusWithTranslationsAsync(string languageCode)
        {
            return await _context.Menus
                .Include(m => m.Translations.Where(t => t.Language == languageCode))
                .ToListAsync();
        }
    }
}
