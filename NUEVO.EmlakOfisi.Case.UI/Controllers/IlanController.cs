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
                        GorselLinki = model.GorselLinki
                    };

                    //var result = _ilanService.Create(ilan);

                    using (var context = new EmlakfOfisiContext())
                    {
                        context.Set<Ilan>().Add(ilan);
                        context.SaveChanges();
                    };

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return View(model);
        }
    }
}