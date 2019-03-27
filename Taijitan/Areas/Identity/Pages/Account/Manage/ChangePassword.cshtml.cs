using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace Taijitan.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Huidig wachtwoord is verplicht in te vullen")]
            [DataType(DataType.Password, ErrorMessage = "Geen geldig wachtwoord")]
            [Display(Name = "Huidig Wachtwoord")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = "Nieuw wachtwoord is verplicht in te vullen")]
            [StringLength(100, ErrorMessage = "Het wachtwoord moet tussen minstens 6 en maximum 100 tekens lang zijn", MinimumLength = 6)]
            [DataType(DataType.Password, ErrorMessage = "Geen geldig wachtwoord")]
            [Display(Name = "Nieuw Wachtwoord")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password, ErrorMessage = "Geen geldig wachtwoord")]
            [Display(Name = "Bevestig nieuw wachtwoord")]
            [Compare("NewPassword", ErrorMessage = "Het nieuwe wachtwoord en de bevestiging komen niet overeen")]
            public string ConfirmPassword { get; set; }
        }
        public IActionResult OnGet()
        {

            return RedirectToPage("/Account/login");

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToPage();
        }
    }
}
