using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultilingualMenuApp.Models;
using MultilingualMenuApp.Services;
using System.ComponentModel.DataAnnotations;

namespace MultilingualMenuApp.Pages.Admin
{
    public class MenuEditModel : PageModel
    {
        private readonly IMenuService _menuService;
        private readonly ILanguageService _languageService;

        public MenuEditModel(IMenuService menuService, ILanguageService languageService)
        {
            _menuService = menuService;
            _languageService = languageService;
        }

        [BindProperty]
        public MenuInputModel Menu { get; set; } = new();

        public List<Language> AvailableLanguages { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            AvailableLanguages = await _languageService.GetActiveLanguagesAsync();

            if (id.HasValue)
            {
                var menu = await _menuService.GetMenuWithTranslationsAsync(id.Value);
                if (menu == null)
                {
                    return NotFound();
                }

                Menu = new MenuInputModel
                {
                    Id = menu.Id,
                    DefaultName = menu.DefaultName,
                    DefaultDescription = menu.DefaultDescription,
                    Translations = menu.Translations.Select(t => new MenuTranslationInputModel
                    {
                        Id = t.Id,
                        Language = t.Language,
                        Name = t.Name,
                        Description = t.Description,
                        MenuId = t.MenuId
                    }).ToList()
                };
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AvailableLanguages = await _languageService.GetActiveLanguagesAsync();
                return Page();
            }

            var menu = new Menu
            {
                Id = Menu.Id,
                DefaultName = Menu.DefaultName,
                DefaultDescription = Menu.DefaultDescription,
                Translations = Menu.Translations.Where(t => !string.IsNullOrWhiteSpace(t.Language)).Select(t => new MenuTranslation
                {
                    Id = t.Id,
                    Language = t.Language,
                    Name = t.Name,
                    Description = t.Description,
                    MenuId = t.MenuId
                }).ToList()
            };

            if (Menu.Id == 0)
            {
                await _menuService.CreateMenuAsync(menu);
            }
            else
            {
                await _menuService.UpdateMenuAsync(menu);
            }

            return RedirectToPage("./Menus");
        }

        public async Task<IActionResult> OnPostAddTranslationAsync()
        {
            AvailableLanguages = await _languageService.GetActiveLanguagesAsync();
            Menu.Translations.Add(new MenuTranslationInputModel());
            ModelState.Clear();
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveTranslationAsync(int index)
        {
            AvailableLanguages = await _languageService.GetActiveLanguagesAsync();
            if (index >= 0 && index < Menu.Translations.Count)
            {
                Menu.Translations.RemoveAt(index);
            }
            ModelState.Clear();
            return Page();
        }

        public class MenuInputModel
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Default name is required")]
            [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
            public string DefaultName { get; set; } = string.Empty;

            [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
            public string DefaultDescription { get; set; } = string.Empty;

            public List<MenuTranslationInputModel> Translations { get; set; } = new();
        }

        public class MenuTranslationInputModel
        {
            public int Id { get; set; }
            public string Language { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public int MenuId { get; set; }
        }
    }
}
