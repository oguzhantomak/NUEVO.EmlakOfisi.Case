using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NUEVO.EmlakOfisi.Case.Entity;
using NUEVO.EmlakOfisi.Case.Entity.DTO.User;

namespace NUEVO.EmlakOfisi.Case.UI.Controllers
{
    public class HomeController : Controller
    {

        #region [ DI ]

        private UserManager<User> _userManager { get; }
        private SignInManager<User> _signInManager { get; }

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            // Kullanıcı login olduktan sonra hangi urlde kaldıysa oraya geri yönlendirilecek url
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {

            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }
                        return RedirectToAction("Index", "Member");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz email veya şifre.");
                }
            }

            return View(model);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(CreateUserDto model)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = model.Username,
                    Ad = model.Ad,
                    Email = model.Email,
                    Soyad = model.Soyad,
                    FirmaAdi = model.FirmaAdi
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }

            return View(model);
        }

    }
}
