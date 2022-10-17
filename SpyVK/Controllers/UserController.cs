using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpyVK.Entities;
using SpyVK.ViewModels;

namespace SpyVK.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpApplicationUser userSignUpViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByNameAsync(userSignUpViewModel.UserName) == null)
                {
                    ModelState.AddModelError("UserName", $"User with name {userSignUpViewModel.UserName} is already exist.");
                    return View(userSignUpViewModel);
                }
                ApplicationUser user = new ApplicationUser
                {
                    UserName = userSignUpViewModel.UserName
                };
                var result = await _userManager.CreateAsync(user, userSignUpViewModel.Password);
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }
            }
            return View(userSignUpViewModel);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInApplicationUser userModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userModel.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("UserName", $"User with name {userModel.UserName} is not exist.");
                    return View(userModel);
                }
                var result = await _signInManager.PasswordSignInAsync(user, userModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "lox");
                }
            }
            return View(userModel);
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
            return Ok();
        }
    }
}
