using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace NUEVO.EmlakOfisi.Case.UI.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
