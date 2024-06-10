using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Pages.Profile
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ProfileModel> _logger;

        public ProfileModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<ProfileModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public string? DisplayName { get; set; } = default;
        public string? Email { get; set; } = default;

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity == null)
            {
                return Redirect("/login");
            }
            
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/login");
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/login");
            }

            DisplayName = user.DisplayName!;
            Email = user.Email!;

            return Page();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Redirect("/Mainpage");
        }
    }
}
