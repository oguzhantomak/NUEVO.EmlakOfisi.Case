using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NUEVO.EmlakOfisi.Case.Data.Concrete;
using NUEVO.EmlakOfisi.Case.Entity;
using NUEVO.EmlakOfisi.Case.Entity.DTO.Ilan;
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
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

            if (userId != null && userId != 0)
            {
                ViewBag.layout = "~/Views/Member/_MemberLayout.cshtml";
            }

            // Anasayfa ilanlarını listele.
            var ilanlar = GetList();
            return View(ilanlar);
        }

        public IActionResult Login(string returnUrl)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (userId != 0 && userId != 0)
            {
                return RedirectToAction("Profil", "Member");
            }
            // Kullanıcı login olduktan sonra hangi urlde kaldıysa oraya geri yönlendirilecek url
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

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
                        TempData["status"] = "success";
                        return RedirectToAction("Profil", "Member");
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

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordDto model)
        {
            // DB'de bu email ile bir kayıt var mı kontrolü
            var user = _userManager.FindByEmailAsync(model.Email).Result;

            if (user != null)
            {
                // Kullanıcı bilgileri ile bir token oluştur
                string passwordResetToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;

                string passwordResetLink = Url.Action("ResetPasswordConfirm", "Home", new
                {
                    userId = user.Id,
                    token = passwordResetToken
                }, HttpContext.Request.Scheme);

                // oluşturulan şifre yenileme linkini static metodumuza gönderip metotu çalıştırıyoruz.
                Helper.PasswordReset.PasswordResetSendEmail(passwordResetLink, model.Email);

                ViewBag.status = "success";
            }
            else
            {
                ModelState.AddModelError("", "Bu mail adresi sistemde kayıtlı değildir!");
            }

            return View(model);
        }

        public IActionResult ResetPasswordConfirm(string userId, string token)
        {
            // Tempdataya kaydedip post metodunda kullanacağız.
            TempData["userId"] = userId;
            TempData["token"] = token;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm([Bind("Password")] ResetPasswordDto model)
        {
            // Tempdatadan token ve userId bilgilerini al
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();

            //Db'de bu id'ye ait user var mı diye kontrol et
            var user = await _userManager.FindByIdAsync(userId);

            //Eğer böyle bir user var ise
            if (user != null)
            {
                // O user'ın passwordunu resetle
                var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

                // Şifre sıfırlama başarılı ise
                if (result.Succeeded)
                {
                    // Kullanıcının security stampini update et. Bunu yapmamızın amacı kullanıcının cookiesinde kayıtlı security stamp ile sitede dolaşamaması, logine yönlendirilmesi. Eski şifre ile sitede dolaşmamalı :)
                    await _userManager.UpdateSecurityStampAsync(user);

                    ViewBag.status = "success";
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
                ModelState.AddModelError("", "Bir hata meydana geldi. Tekrar deneyiniz.");
            }
            return View(model);
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
                    EmlakciSoyadi = y.User.Soyad,
                    Id = y.Id
                }).OrderByDescending(x => x.OlusturmaTarihi).ToList();

                return list;
            }
        }

    }
}
