using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using OroSmart.Data;
//using OroSmart.Data.Services;
using OroSmart.Data.ViewModels;
using OroSmart.Models;
using System.Globalization;
using System.Security.Claims;

namespace OroSmart.Controllers
{
    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStringLocalizer<SettingsController> _localizer;

        public SettingsController(UserManager<ApplicationUser> usermanager,
            SignInManager<ApplicationUser> signInManager,
            AppDbContext context,
            IStringLocalizer<SettingsController> localizer
            )
        {
            _userManager = usermanager;
            _signInManager = signInManager;
            _context = context;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> userInfo()
        {

            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                return View(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            else
            {
                TempData["SuccessMessage"] = "Password changed successfully!";
                return RedirectToAction("ChangePassword");
            }
        }

        public async Task<IActionResult> DisplayLanguages()
        {
            var availableLanguages = new List<string> { "en-US", "it-IT" };

            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {

                string currentUserLanguage = user.Language;
                ViewData["CurrentCulture"] = currentUserLanguage;
            }

            ViewData["AvailableLanguages"] = availableLanguages;

            return View("DisplayLanguages");
        }


        //[HttpPost]
        //public async Task<IActionResult> ChangeLanguage(string culture)
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    if (user != null)
        //    {
        //        await _languageInterface.SetUserLanguageAsync(user.Id, culture);
        //    }

        //    return RedirectToAction("DisplayLanguages", "Settings");
        //}



        [HttpPost]
        public async Task<IActionResult> ChangeLanguage(string culture)
        {
            var user = await _userManager.GetUserAsync(User);

            if (string.IsNullOrEmpty(culture))
            {
                culture = Request.HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name;
            }

        //    Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
        //CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        //new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            if (user != null)
            {
                user.Language = culture; // Update user's language preference
                await _context.SaveChangesAsync(); // Save changes to the database
            }

            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture));
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, cookieValue, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            });

            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);


            ViewData["CurrentCulture"] = culture;

            return View("DisplayLanguages");
        }
    }
}

