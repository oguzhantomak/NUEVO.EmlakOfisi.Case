using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Ilanlar()
        {
            // TODO Bütün ilanlar listelenecek.
            return View();
        }

        public IActionResult Emlakcilar()
        {
            //TODO Rolü emlakçı olanlar listelenecek.
            return View();
        }
    }
}
