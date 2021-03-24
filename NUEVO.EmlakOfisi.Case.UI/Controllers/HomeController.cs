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

        private  UserManager<User> _userManager { get; }

        public HomeController(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
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
                        ModelState.AddModelError("",item.Description);
                    }
                }

            }

            return View(model);
        }

    }
}
