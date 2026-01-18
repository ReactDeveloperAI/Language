using MultilingualMenuApp.Models;

namespace MultilingualMenuApp.Services
{
    /// <summary>
    /// Service interface for menu operations
    /// </summary>
    public interface IMenuService
    {
        Task<List<Menu>> GetAllMenusAsync();
        Task<Menu?> GetMenuByIdAsync(int id);
        Task<Menu?> GetMenuWithTranslationsAsync(int id);
        Task<Menu> CreateMenuAsync(Menu menu);
        Task UpdateMenuAsync(Menu menu);
        Task DeleteMenuAsync(int id);
        Task<List<Menu>> GetMenusWithTranslationsAsync(string languageCode);
    }
}
