using CryptoNewsApp.Core.Entities;
using CryptoNewsApp.Web.Areas.Author.Models.Vms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CryptoNewsApp.Web.Areas.Author.Controllers
{
    [Area("Author")]
    public class AuthorProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthorProfileController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var vm = new AuthorProfileVM
            {
                FullName = currentUser.Email,
                Email = currentUser.Email,
            };

            return View(vm);
        }
    }
}
