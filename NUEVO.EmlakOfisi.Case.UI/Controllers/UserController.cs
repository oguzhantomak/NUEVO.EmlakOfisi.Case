using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NUEVO.EmlakOfisi.Case.Entity;
using NUEVO.EmlakOfisi.Case.Entity.DTO.Role;
using NUEVO.EmlakOfisi.Case.Entity.DTO.User;

namespace NUEVO.EmlakOfisi.Case.UI.Controllers
{
    public class UserController : Controller
    {

        #region [ DI ]

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }
        

        public async Task<IActionResult> Create(CreateUserDto model)
        {

            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    FirmaAdi = model.FirmaAdi,
                    Ad = model.Ad,
                    Email = model.Email,
                    Soyad = model.Soyad,
                    UserName = model.Username
                };


                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
            }
            return BadRequest("Girdiğiniz bilgileri kontrol ediniz!");
        }
        
        public async Task<IActionResult> CreateRole(CreateRoleDto model)
        {
            var result = await _roleManager.CreateAsync(new Role() { Name = model.Name});

            if (result.Succeeded)
            {
                return RedirectToAction("Index","Admin");
            }
            return BadRequest("Girdiğiniz bilgileri kontrol ediniz!");
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
