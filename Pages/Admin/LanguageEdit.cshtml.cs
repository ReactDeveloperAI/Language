using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultilingualMenuApp.Models;
using MultilingualMenuApp.Services;
using System.ComponentModel.DataAnnotations;

namespace MultilingualMenuApp.Pages.Admin
{
    public class LanguageEditModel : PageModel
    {
        private readonly ILanguageService _languageService;

        public LanguageEditModel(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [BindProperty]
        public LanguageInputModel Language { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                var language = await _languageService.GetLanguageByIdAsync(id.Value);
                if (language == null)
                {
                    return NotFound();
                }
                
                Language = new LanguageInputModel
                {
                    Id = language.Id,
                    Code = language.Code,
                    Name = language.Name,
                    IsActive = language.IsActive
                };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var language = new Language
            {
                Id = Language.Id,
                Code = Language.Code,
                Name = Language.Name,
                IsActive = Language.IsActive
            };

            if (Language.Id == 0)
            {
                await _languageService.CreateLanguageAsync(language);
            }
            else
            {
                await _languageService.UpdateLanguageAsync(language);
            }

            return RedirectToPage("./Languages");
        }

        public class LanguageInputModel
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Language code is required")]
            [StringLength(10, ErrorMessage = "Language code cannot exceed 10 characters")]
            public string Code { get; set; } = string.Empty;

            [Required(ErrorMessage = "Language name is required")]
            [StringLength(100, ErrorMessage = "Language name cannot exceed 100 characters")]
            public string Name { get; set; } = string.Empty;

            public bool IsActive { get; set; } = true;
        }
    }
}
