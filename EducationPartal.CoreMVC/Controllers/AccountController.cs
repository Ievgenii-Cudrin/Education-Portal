using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPartal.CoreMVC.Interfaces;
using EducationPartal.CoreMVC.ModelsView;
using EducationPortal.BLL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EducationPartal.CoreMVC.Controllers
{
    public class AccountController : Controller
    {
        private ILogInService logInService;
        private IUserService userService;
        private IAutoMapperService autoMapperService;

        public AccountController(
            ILogInService logInService,
            IUserService userService,
            IAutoMapperService autoMapperService)
        {
            this.logInService = logInService;
            this.userService = userService;
            this.autoMapperService = autoMapperService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.logInService.LogIn(model.Email, model.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Email)
                    };

                    var identity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties();
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = this.autoMapperService.CreateMapFromVMToDomain<RegisterViewModel, User>(model);
                bool success = this.userService.CreateUser(user);

                if (success)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            this.logInService.LogOut();
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Login", "Account");
        }
    }
}
