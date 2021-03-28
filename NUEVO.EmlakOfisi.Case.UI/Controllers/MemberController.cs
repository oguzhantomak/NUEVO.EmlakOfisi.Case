using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NUEVO.EmlakOfisi.Case.Data.Concrete;
using NUEVO.EmlakOfisi.Case.Entity;
using NUEVO.EmlakOfisi.Case.Entity.DTO.Ilan;
using NUEVO.EmlakOfisi.Case.Entity.DTO.User;
using Mapster;

namespace NUEVO.EmlakOfisi.Case.UI.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        #region [ DI ]


        private readonly EmlakfOfisiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public MemberController(EmlakfOfisiContext context, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
        }

        #endregion

        public IActionResult Index()
        {
            //var userId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return View();
        }

        public IActionResult Profil()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _userManager.FindByIdAsync(userId).Result;

            // veya aşağıdaki gibi name üzerinden çekebiliriz.
            //var user2 = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // Mapster ile mapleme
            var model = user.Adapt<UpdateUserDto>();

            if (user != null)
            {
                ViewBag.layout = "~/Views/Member/_MemberLayout.cshtml";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profil(UpdateUserDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                user.Ad = model.Ad;
                user.Soyad = model.Soyad;
                user.FirmaAdi = model.FirmaAdi;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);

                    ViewBag.success = "true";
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

        public IActionResult Ilanlarim()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var list = GetList(Convert.ToInt32(userId));

            return View(list);
        }

        public List<ListIlanProfileDto> GetList(int userId)
        {
            using (var ctx = new EmlakfOfisiContext())
            {
                var list = ctx.Set<Ilan>().Where(x => x.UserId == userId).Select(y => new ListIlanProfileDto()
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
                    OlusturmaTarihi = y.CreatedDate
                }).ToList();

                return list;
            }
        }

        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PasswordChange(PasswordChangeDto model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

                //Kullanıcı eski şifresi doğruluk kontrolü
                bool exist = _userManager.CheckPasswordAsync(user, model.OldPassword).Result;
                if (exist)
                {
                    var result = _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword)
                        .Result;

                    if (result.Succeeded)
                    {
                        // Aşağıdaki işlemşer ile önce kullanıcının dbdeki security stampini yeni bilgiler ile değiştiriyoruz. Sonra kullanıcıyı signout yapıyoruz, daha sonra yeni şifre ile signin yapıp cookie'yi değiştirmiş oluyoruz.
                        _userManager.UpdateSecurityStampAsync(user);

                        _signInManager.SignOutAsync();
                        _signInManager.PasswordSignInAsync(user, model.NewPassword, true, false);

                        ViewBag.success = "true";
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eski şifreniz yanlış");
                }
            }
            return View(model);
        }

        public void LogOut()
        {
            _signInManager.SignOutAsync();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
