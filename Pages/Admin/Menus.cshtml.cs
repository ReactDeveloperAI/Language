using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultilingualMenuApp.Models;
using MultilingualMenuApp.Services;

namespace MultilingualMenuApp.Pages.Admin
{
    public class MenusModel : PageModel
    {
        private readonly IMenuService _menuService;

        public MenusModel(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public List<Menu> Menus { get; set; } = new();

        public async Task OnGetAsync()
        {
            Menus = await _menuService.GetAllMenusAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _menuService.DeleteMenuAsync(id);
            return RedirectToPage();
        }
    }
}
