using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OroSmart.Data;
using OroSmart.Data.ViewModels;
using OroSmart.Models;
using System.Diagnostics;
using System.Globalization;

namespace OroSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        
        private readonly ILogger<HomeController> _logger2;

        public HomeController(ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AppDbContext context,
            ILogger<HomeController> logger2)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger2 = logger2;
        }

        [Authorize]
        public IActionResult Index(LoginVM loginVM)
        {
            //var user = _userManager.GetUserAsync(User);

            //var selectedLanguage = user.Language;

            //var culture = new CultureInfo(selectedLanguage);
            //Thread.CurrentThread.CurrentCulture = culture;
            //Thread.CurrentThread.CurrentUICulture = culture;

            _logger.LogInformation("User {UserName} accessed the Index action at {Timestamp}.", User.Identity.Name, DateTime.UtcNow);


            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}