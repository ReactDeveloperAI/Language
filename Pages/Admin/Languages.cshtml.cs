using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultilingualMenuApp.Models;
using MultilingualMenuApp.Services;

namespace MultilingualMenuApp.Pages.Admin
{
    public class LanguagesModel : PageModel
    {
        private readonly ILanguageService _languageService;

        public LanguagesModel(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public List<Language> Languages { get; set; } = new();

        public async Task OnGetAsync()
        {
            Languages = await _languageService.GetAllLanguagesAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _languageService.DeleteLanguageAsync(id);
            return RedirectToPage();
        }
    }
}
