using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IDataProtector _protector;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(
            IDataProtectionProvider dataProtectionProvider,
            UserManager<IdentityUser> userManager)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _protector = dataProtectionProvider.CreateProtector(nameof(HomeController));
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> InitCookie()
        {
            string cookieProtectedData = "SensitiveData";
            string? cookieContent = Request.Cookies[cookieProtectedData];

            if (string.IsNullOrEmpty(cookieContent))
            {
                var user = await _userManager.GetUserAsync(User);

                string protectedData = _protector.Protect(user.Email);
                Console.WriteLine($"Email : {user.Email}");
                Console.WriteLine($"Email protégé : {protectedData}");

                Response.Cookies.Append(
                    cookieProtectedData,
                    protectedData,
                    new CookieOptions
                    {
                        Secure = true,
                        HttpOnly = true,
                        MaxAge = TimeSpan.FromHours(1)
                    });
            }
            else
            {
                string unprotectedCookie = _protector.Unprotect(cookieContent);
                Console.WriteLine($"Contenu du cookie protégé : {cookieContent}");
                Console.WriteLine($"Contenu du cookie : {unprotectedCookie}");
            }

            return LocalRedirect("/");
        }
    }
}
