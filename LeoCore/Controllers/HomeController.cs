using LeoCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LeoCore.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        //[Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Over deze applicatie";

            return View();
        }

        [HttpGet]
        //[Authorize]
        public ActionResult contact()
        {


            return View();

        }

        [HttpGet]
        //[Authorize]
        public ActionResult index()
        {
            return View();
        }
    }
}