using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OroSmart.Data;
using OroSmart.Data.Static;
using OroSmart.Data.ViewModels;
using OroSmart.Models;

namespace OroSmart.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login([Bind(nameof(LoginVM.EmailAddress), nameof(LoginVM.Password))] LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {

                    user.LastLoginTime = DateTime.Now;
                    user.LastLoginIpAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                    if (string.IsNullOrEmpty(user.LastLoginIpAddress))
                    {
                        user.LastLoginIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                    }
                    await _userManager.UpdateAsync(user);

                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                        //return View("Index", "Home", "_LayoutNew");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again";
            return View(loginVM);
        }

        //To save user log in 
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult UserLogins()
        {

            var UserLogins = _context.Users
                .Select(u => new UserLoginVM
                {
                    UserName = u.FullName, 
                    IPAddress = u.LastLoginIpAddress,
                    LastLoginTime = u.LastLoginTime

                })
                .ToList();

            return View(UserLogins);
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction("Login", "Account");
        }
    }
}
