using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NUEVO.EmlakOfisi.Case.Data.Concrete;
using NUEVO.EmlakOfisi.Case.Entity;
using NUEVO.EmlakOfisi.Case.Entity.DTO.Ilan;
using NUEVO.EmlakOfisi.Case.Entity.DTO.Role;
using NUEVO.EmlakOfisi.Case.Entity.DTO.User;

namespace NUEVO.EmlakOfisi.Case.UI.Controllers
{
    public class AdminController : Controller
    {
        #region [ DI ]

        private UserManager<User> _userManager { get; }
        private SignInManager<User> _signInManager { get; }

        private RoleManager<Role> _roleManager { get; }

        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        #endregion
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Ilanlar()
        {
            var ilanlar = GetList();
            return View(ilanlar);
        }

        public IActionResult Emlakcilar()
        {
            var list = GetUserList();
            return View(list);
        }

        public IActionResult EmlakciEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmlakciEkle(CreateUserDtoForAdmin model)
        {
            model.Password = "123456";
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
                    return RedirectToAction("Emlakcilar");
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

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRole(CreateRoleDto model)
        {
            if (ModelState.IsValid)
            {
                var role = new Role()
                {
                    Name = model.Name
                };

                var result = _roleManager.CreateAsync(role).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("Roller");
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

        public IActionResult Roller()
        {
            var list = GetRolesList();
            return View(list);
        }

        public List<ListIlanProfileDto> GetList()
        {
            using (var ctx = new EmlakfOfisiContext())
            {
                var list = ctx.Set<Ilan>().Select(y => new ListIlanProfileDto()
                {
                    Fiyat = y.Fiyat,
                    BanyoSayisi = y.BanyoSayisi,
                    CityName = y.City.CityName,
                    CountryName = y.Country.CountryName,
                    EmlakTuru = y.EmlakTuru.EmlakTuruAdi,
                    BinaToplamKatSayisi = y.BinaToplamKatSayisi,
                    BulunduguKat = y.BulunduguKat,
                    GorselLinki = y.GorselLinki,
                    IlanBasligi = y.IlanBasligi,
                    IlanIcerigi = y.IlanIcerigi,
                    OdaSayisi = y.OdaSayisi,
                    Tur = y.Tur,
                    EmlakYasi = y.EmlakYasi,
                    Metrekare = y.Metrekare,
                    OlusturmaTarihi = y.CreatedDate,
                    EmlakciAdi = y.User.Ad,
                    EmlakciSoyadi = y.User.Soyad
                }).OrderByDescending(x => x.OlusturmaTarihi).ToList();

                return list;
            }
        }

        public List<UpdateUserDto> GetUserList()
        {
            using (var ctx = new EmlakfOfisiContext())
            {
                var list = ctx.Set<User>().Select(y => new UpdateUserDto()
                {
                    Ad = y.Ad,
                    Soyad = y.Soyad,
                    Email = y.Email,
                    FirmaAdi = y.FirmaAdi
                }).ToList();

                return list;
            }
        }

        public List<CreateRoleDto> GetRolesList()
        {
            using (var ctx = new EmlakfOfisiContext())
            {
                var list = ctx.Set<Role>().Select(y => new CreateRoleDto()
                {
                    Name = y.Name
                }).ToList();

                return list;
            }
        }
    }
}
