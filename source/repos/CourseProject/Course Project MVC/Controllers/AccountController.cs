using Business_Logic.Abstract;
using Course_Project_MVC.Models;
using DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Course_Project_MVC.Controllers
{
    public class AccountController : Controller
    {
        private Manager? loggedIn;
        private readonly MyIAuthenticationService _authenticationService;

        public AccountController(MyIAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public IActionResult Login(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var smth = new LoginModel();
            return View(smth);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel smth, string? returnUrl)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "THESE FIELDS ARE MANDATORY!");
                    return View(smth);
                }
                loggedIn = _authenticationService.Authentication(smth.UserName, smth.Password);
                if(loggedIn == null)
                {
                    ModelState.AddModelError(string.Empty, "LOGIN FAILED! YOU ARE NOT OUR EMPLOYEE!");
                    return View(smth);
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, smth.UserName),
                    new Claim(ClaimTypes.NameIdentifier, loggedIn.ManagerID.ToString()),
                    new Claim(ClaimTypes.Role,"RULER")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProps = new AuthenticationProperties();
                //test drops here because of null reference
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authenticationProps);
                string? ReturnUrl = returnUrl;
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    return Redirect(ReturnUrl);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "oopssssss, ssssomething went wrong...");
                return View(smth);
            }
        }
    }
}
