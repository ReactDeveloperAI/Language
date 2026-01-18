using MultilingualMenuApp.Models;

namespace MultilingualMenuApp.Services
{
    /// <summary>
    /// Service interface for language operations
    /// </summary>
    public interface ILanguageService
    {
        Task<List<Language>> GetAllLanguagesAsync();
        Task<List<Language>> GetActiveLanguagesAsync();
        Task<Language?> GetLanguageByIdAsync(int id);
        Task<Language?> GetLanguageByCodeAsync(string code);
        Task<Language> CreateLanguageAsync(Language language);
        Task UpdateLanguageAsync(Language language);
        Task DeleteLanguageAsync(int id);
    }
}
