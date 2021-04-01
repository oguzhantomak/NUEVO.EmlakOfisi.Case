using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NUEVO.EmlakOfisi.Case.Business.Abstract;
using NUEVO.EmlakOfisi.Case.Data.Concrete;
using NUEVO.EmlakOfisi.Case.Entity;
using NUEVO.EmlakOfisi.Case.Entity.DTO.Ilan;

namespace NUEVO.EmlakOfisi.Case.UI.Controllers
{
    [Authorize]
    public class IlanController : Controller
    {
        #region [ DI ]

        private readonly IIlanService _ilanService;

        private readonly EmlakfOfisiContext _context;

        public IlanController(IIlanService ilanService, EmlakfOfisiContext context)
        {
            this._ilanService = ilanService;
            this._context = context;
        }

        #endregion

        public IActionResult Create()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

            if (userId != null && userId != 0)
            {
                ViewBag.layout = "~/Views/Member/_MemberLayout.cshtml";
            }

            var model = new CreateIlanDto()
            {
                EmlakTurus = _context.Set<EmlakTuru>().ToList(),
                Cities = _context.Set<City>().ToList(),
                Countries = _context.Set<Country>().ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateIlanDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Cities = _context.Set<City>().ToList();
                    model.EmlakTurus = _context.Set<EmlakTuru>().ToList();
                    model.Countries = _context.Set<Country>().ToList();

                    var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                    var ilan = new Ilan()
                    {
                        CountryId = model.CountryId,
                        CityId = model.CityId,
                        BanyoSayisi = model.BanyoSayisi,
                        BinaToplamKatSayisi = model.BinaToplamKatSayisi,
                        BulunduguKat = model.BulunduguKat,
                        EmlakTuruId = model.EmlakTuruId,
                        Fiyat = model.Fiyat,
                        IlanBasligi = model.IlanBasligi,
                        IlanIcerigi = model.IlanIcerigi,
                        OdaSayisi = model.OdaSayisi,
                        Tur = model.Tur,
                        UserId = userId,
                        GorselLinki = model.GorselLinki,
                        EmlakYasi = model.EmlakYasi,
                        Metrekare = model.Metrekare
                    };

                    var result = _ilanService.Create(ilan);

                    //using (var context = new EmlakfOfisiContext())
                    //{
                    //    context.Set<Ilan>().Add(ilan);
                    //    context.SaveChanges();
                    //};
                    if (result != null)
                    {
                        return RedirectToAction("Ilanlarim", "Member");
                    }
                    else
                    {
                        return View(model);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var ilan = _context.Set<Ilan>().First(x => x.Id == id);
            if (ilan == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = _context.Set<Ilan>().Where(x=>x.Id==id).Select(y => new ListIlanProfileDto()
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
            }).First();
            return View(model);
        }
    }
}