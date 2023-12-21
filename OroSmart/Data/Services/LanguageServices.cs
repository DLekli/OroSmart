//using Microsoft.AspNetCore.Identity;
//using OroSmart.Models;

//namespace OroSmart.Data.Services
//{
//    public class LanguageServices : ILanguageInterface
//    {
//        private readonly IServiceProvider _serviceProvider;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public LanguageServices(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
//        {
//            _serviceProvider = serviceProvider;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<string> GetUserLanguageAsync(string userId)
//        {
//            using var scope = _serviceProvider.CreateScope();
//            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

//            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
//            return user?.Language ?? "en-US";
//        }

//        public async Task SetUserLanguageAsync(string userId, string culture)
//        {
//            var httpContext = _httpContextAccessor.HttpContext;

//            var cookieOptions = new CookieOptions
//            {
//                Expires = DateTimeOffset.UtcNow.AddYears(1), // Set cookie expiration as needed
//                IsEssential = true // Mark the cookie as essential
//            };

//            httpContext.Response.Cookies.Append("UserLanguage", culture, cookieOptions);


//            using var scope = _serviceProvider.CreateScope();
//            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

//            var user = await userManager.FindByIdAsync(userId);
//            if (user != null)
//            {
//                user.Language = culture;
//                await userManager.UpdateAsync(user);
//            }
//        }

//        public void RemoveLanguageCookie()
//        {
//            var httpContext = _httpContextAccessor.HttpContext;
//            httpContext.Response.Cookies.Delete("UserLanguage");
//        }
//    }
//}
