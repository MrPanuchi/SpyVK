using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpyVK.Entities;
using SpyVK.Models.VK.Api;
using SpyVK.Services;
using SpyVK.Services.Interfaces;
using SpyVK.ViewModels;
using System.Net;
using System.Security.Claims;

namespace SpyVK.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUserIdentity> _userManager;
        private readonly SignInManager<ApplicationUserIdentity> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IVKApiService _VKApiService;
        private readonly IQueueOfTask _queueOfTask;
        public UserController(
            UserManager<ApplicationUserIdentity> userManager , 
            SignInManager<ApplicationUserIdentity> signInManager, 
            RoleManager<ApplicationRole> roleManager,
            IVKApiService VKApiService,
            IQueueOfTask queueOfTask)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _VKApiService = VKApiService;
            _queueOfTask = queueOfTask;
        }
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(string returnUrl)
        {
            var autScheme = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(new SignInApplicationUser
            {
                ReturnUrl = "User/SignIn",
                ExternalProviders = autScheme
            });
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalSignIn(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action(nameof(ExternalSignInCallback), "User", new { returnUrl });
            AuthenticationProperties properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ExternalSignInCallback(string returnUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return Redirect(returnUrl);
            }
            
            Task<User> task = new Task<User>(() => _VKApiService.GetSelfAccount(info.AuthenticationTokens.First().Value));
            _queueOfTask.AddPrimaryTask(task);

            task.Wait();

            User userResult = task.Result;

            var result = await _userManager.FindByIdAsync(userResult.Id.ToString());

            if (result == null)
            {
                // TO DO Registration
            }
            else
            {

            }

            ApplicationUserIdentity user = new ApplicationUserIdentity();
            return Ok();
        }
    }
}
