using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using OroSmart.Data;
<<<<<<< HEAD
using OroSmart.Data.Pagination;
=======
//using OroSmart.Data.Services;
>>>>>>> Semi
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

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AppDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login([Bind(nameof(LoginVM.EmailAddress), nameof(LoginVM.Password))] LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
<<<<<<< HEAD
=======
                    user.LastLoginTime = DateTime.Now;
                    user.LastLoginIpAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? HttpContext.Connection.RemoteIpAddress?.ToString();

                    if (string.IsNullOrEmpty(user.LastLoginIpAddress))
                    {
                        user.LastLoginIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                    }

                    await _userManager.UpdateAsync(user);
>>>>>>> Semi

                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
<<<<<<< HEAD
                        // Save user login history
                        await SaveUserLoginHistory(user);
=======

                        var selectedLanguage = user.Language;

                        var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(selectedLanguage));
                        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, cookieValue, new CookieOptions
                        {
                            Expires = DateTimeOffset.UtcNow.AddYears(1)
                        });

                        var culture = new CultureInfo(selectedLanguage);
                        Thread.CurrentThread.CurrentCulture = new CultureInfo(selectedLanguage); 
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);
>>>>>>> Semi

                        return RedirectToAction("Index", "Home");
                        //return View("Index", "Home", "_LayoutNew");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please try again.";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please try again.";
            return View(loginVM);
        }


<<<<<<< HEAD
        [Authorize(Roles = "Admin")]
        public IActionResult UserLoginHistory(string userNameSearch, string ipAddressSearch, DateTime? loginTimeSearch, DateTime? logoutTimeSearch, int pageNumber = 1, int pageSize = 10)
=======
        //To save user log in 
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult UserLogins()
>>>>>>> Semi
        {
            var query = _context.UserLoginHistories.AsQueryable();

<<<<<<< HEAD
            if (!string.IsNullOrEmpty(userNameSearch))
            {
                query = query.Where(u => u.FullName.Contains(userNameSearch));
            }
=======
            var UserLogins = _context.Users
                .Select(u => new UserLoginVM
                {
                    UserId = u.Id,
                    UserName = u.FullName,
                    IPAddress = u.LastLoginIpAddress,
                    LastLoginTime = u.LastLoginTime
>>>>>>> Semi

            if (!string.IsNullOrEmpty(ipAddressSearch))
            {
                query = query.Where(u => u.LoginIpAddress.Contains(ipAddressSearch));
            }

            if (loginTimeSearch.HasValue)
            {
                var searchDateTime = loginTimeSearch.Value;

                query = query.Where(u => u.LoginTime.HasValue &&
                                          u.LoginTime.Value.Year == searchDateTime.Year &&
                                          u.LoginTime.Value.Month == searchDateTime.Month &&
                                          u.LoginTime.Value.Day == searchDateTime.Day &&
                                          u.LoginTime.Value.Hour == searchDateTime.Hour &&
                                          u.LoginTime.Value.Minute == searchDateTime.Minute);
            }

            if (logoutTimeSearch.HasValue)
            {
                var searchDateTime = logoutTimeSearch.Value;

                query = query.Where(u => u.LogoutTime.HasValue &&
                                          u.LogoutTime.Value.Year == searchDateTime.Year &&
                                          u.LogoutTime.Value.Month == searchDateTime.Month &&
                                          u.LogoutTime.Value.Day == searchDateTime.Day &&
                                          u.LogoutTime.Value.Hour == searchDateTime.Hour &&
                                          u.LogoutTime.Value.Minute == searchDateTime.Minute);
            }

            var paginatedList = PaginatedList<UserLoginHistory>.Create(query, pageNumber, pageSize);

            ViewBag.UserNameSearch = userNameSearch;
            ViewBag.IPAddressSearch = ipAddressSearch;
            ViewBag.LoginTimeSearch = loginTimeSearch?.ToString("yyyy-MM-ddTHH:mm:ss");
            ViewBag.LogoutTimeSearch = logoutTimeSearch?.ToString("yyyy-MM-ddTHH:mm:ss");

            return View(paginatedList);
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

            _context.UserLoginHistories.Add(loginHistory);
            await _context.SaveChangesAsync();
        }

       
        private async Task SaveUserLogoutHistory(ApplicationUser user)
        {
            var latestLoginHistory = _context.UserLoginHistories
                .Where(h => h.UserId == user.Id && h.LogoutTime == null) 
                .OrderByDescending(h => h.LoginTime)
                .FirstOrDefault();

            if (latestLoginHistory != null)
            {
                latestLoginHistory.LogoutTime = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }




        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(User);
            await SaveUserLogoutHistory(user);

            await _signInManager.SignOutAsync();

<<<<<<< HEAD
=======
            HttpContext.Session.Remove("UserLanguage");

>>>>>>> Semi
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}
