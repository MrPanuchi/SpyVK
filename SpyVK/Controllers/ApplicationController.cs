using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpyVK.Entities;

namespace SpyVK.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly UserManager<ApplicationUserIdentity> _userManager;

        public ApplicationController(UserManager<ApplicationUserIdentity> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetInfo()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            HttpClient client = new HttpClient();
            var smt = await client.GetAsync($"https://api.vk.com/method/account.getinfo&access_token={user.tokenVK}&v=5/131");
            return Ok(smt);
        }
    }
}
