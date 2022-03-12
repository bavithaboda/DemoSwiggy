using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSwiggy.Controllers
{
    public class SupportEngineersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
