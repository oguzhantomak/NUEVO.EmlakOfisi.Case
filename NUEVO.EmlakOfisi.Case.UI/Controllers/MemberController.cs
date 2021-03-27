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

namespace NUEVO.EmlakOfisi.Case.UI.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        #region [ DI ]


        private readonly EmlakfOfisiContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public MemberController(EmlakfOfisiContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
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
            var user =_userManager.FindByIdAsync(userId).Result;

            return View(user);
        }

        [HttpPost]
        public IActionResult Profil(UpdateUserDto model)
        {
            // TODO Profil update işlemi yapılacak.
            return View();
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
                    Tur = y.Tur
                }).ToList();

                return list;
            }
        }
    }
}
