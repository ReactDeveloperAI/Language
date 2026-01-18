using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultilingualMenuApp.Models;
using MultilingualMenuApp.Services;

namespace MultilingualMenuApp.Pages.Customer
{
    public class MenuModel : PageModel
    {
        private readonly IMenuService _menuService;
        private readonly ILanguageService _languageService;
        private const string LanguageSessionKey = "SelectedLanguage";

        public MenuModel(IMenuService menuService, ILanguageService languageService)
        {
            _menuService = menuService;
            _languageService = languageService;
        }

        public List<Menu> MenuItems { get; set; } = new();
        public List<Language> AvailableLanguages { get; set; } = new();
        public string CurrentLanguage { get; set; } = "en";
        public string CurrentLanguageName { get; set; } = "English";

        public async Task OnGetAsync()
        {
            // Get current language from session, default to "en"
            CurrentLanguage = HttpContext.Session.GetString(LanguageSessionKey) ?? "en";
            
            // Load available languages
            AvailableLanguages = await _languageService.GetActiveLanguagesAsync();
            
            // Set current language name
            var currentLang = AvailableLanguages.FirstOrDefault(l => l.Code == CurrentLanguage);
            CurrentLanguageName = currentLang?.Name ?? "English";

            // Load menu items with translations
            MenuItems = await _menuService.GetAllMenusAsync();
        }

        public async Task<IActionResult> OnPostChangeLanguageAsync(string languageCode)
        {
            // Validate language code
            var language = await _languageService.GetLanguageByCodeAsync(languageCode);
            if (language != null && language.IsActive)
            {
                // Store selected language in session
                HttpContext.Session.SetString(LanguageSessionKey, languageCode);
            }

            return RedirectToPage();
        }
    }
}
