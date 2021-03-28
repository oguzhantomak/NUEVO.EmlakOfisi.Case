using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NUEVO.EmlakOfisi.Case.Data.Concrete;
using NUEVO.EmlakOfisi.Case.Entity;
using NUEVO.EmlakOfisi.Case.Entity.DTO.Ilan;

namespace NUEVO.EmlakOfisi.Case.UI.Controllers
{
    public class AdminController : Controller
    {
        #region [ DI ]

        private UserManager<User> _userManager { get; }
        private SignInManager<User> _signInManager { get; }

        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        #endregion
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Ilanlar()
        {
            // TODO Bütün ilanlar listelenecek.
            var ilanlar = GetList();
            return View(ilanlar);
        }

        public IActionResult Emlakcilar()
        {
            //TODO Rolü emlakçı olanlar listelenecek.
            return View();
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
    }
}
