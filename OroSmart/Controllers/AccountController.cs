using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OroSmart.Data;
using OroSmart.Data.Pagination;
using OroSmart.Data.Static;
using OroSmart.Data.ViewModels;
using OroSmart.Models;
using System.Globalization;

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

                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        // Save user login history
                        await SaveUserLoginHistory(user);

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

        /*[Authorize(Roles = "Admin")] // Add this attribute to restrict access to administrators
        public IActionResult UserLoginHistory()
        {
            var userLoginHistory = _context.UserLoginHistories.ToList();
            return View(userLoginHistory);
        }*/


        [Authorize(Roles = "Admin")]
        public IActionResult UserLoginHistory(string userNameSearch, string ipAddressSearch, DateTime? loginTimeSearch, DateTime? logoutTimeSearch)
        {
            var query = _context.UserLoginHistories.AsQueryable();

            // Apply filters based on search parameters
            if (!string.IsNullOrEmpty(userNameSearch))
            {
                query = query.Where(u => u.FullName.Contains(userNameSearch));
            }

            if (!string.IsNullOrEmpty(ipAddressSearch))
            {
                query = query.Where(u => u.LoginIpAddress.Contains(ipAddressSearch));
            }

            if (loginTimeSearch.HasValue)
            {
                query = query.Where(u => u.LoginTime >= loginTimeSearch);
            }

            if (logoutTimeSearch.HasValue)
            {
                query = query.Where(u => u.LogoutTime.HasValue && u.LogoutTime.Value <= logoutTimeSearch);
            }

            var userLoginHistory = query.ToList();

            ViewBag.UserNameSearch = userNameSearch;
            ViewBag.IPAddressSearch = ipAddressSearch;
            ViewBag.LoginTimeSearch = loginTimeSearch?.ToString("yyyy-MM-ddTHH:mm");
            ViewBag.LogoutTimeSearch = logoutTimeSearch?.ToString("yyyy-MM-ddTHH:mm");

            return View(userLoginHistory);
        }

        private async Task SaveUserLoginHistory(ApplicationUser user)
        {
            var loginHistory = new UserLoginHistory
            {
                UserId = user.Id,
                FullName = user.FullName,
                LoginTime = DateTime.Now,
                LoginIpAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? HttpContext.Connection.RemoteIpAddress?.ToString()
            };

            // Save the UserLoginHistory record to the database
            _context.UserLoginHistories.Add(loginHistory);
            await _context.SaveChangesAsync();
        }


        // Helper method to save user logout history
        /*private async Task SaveUserLogoutHistory(ApplicationUser user)
        {
            var logoutHistory = new UserLoginHistory
            {
                UserId = user.Id,
                FullName = user.FullName,
                LogoutTime = DateTime.Now,
                // Capture the logout IP address here if needed
            };

            // Save the UserLoginHistory record to the database
            _context.UserLoginHistories.Add(logoutHistory);
            await _context.SaveChangesAsync();
        }*/

        // Helper method to save user logout history
        private async Task SaveUserLogoutHistory(ApplicationUser user)
        {
            // Retrieve the latest login history for the user
            var latestLoginHistory = _context.UserLoginHistories
                .Where(h => h.UserId == user.Id && h.LogoutTime == null) // Get the latest login record without a logout time
                .OrderByDescending(h => h.LoginTime)
                .FirstOrDefault();

            if (latestLoginHistory != null)
            {
                // Update the existing login history record with logout information
                latestLoginHistory.LogoutTime = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else
            {
                // If no matching login record is found, you may handle it accordingly (e.g., log an error)
            }
        }



        

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Retrieve the current user
            var user = await _userManager.GetUserAsync(User);

            // Save user logout history
            await SaveUserLogoutHistory(user);

            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
