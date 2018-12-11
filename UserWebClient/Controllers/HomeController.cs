using ModelLibrary;
using ControllerLibrary;
using DatabaseAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserWebClient.Models;

namespace UserWebClient.Controllers
{
   
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new HomeViewModel { Zipcode = 0 });
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel model)
        {
            return RedirectToAction("Browse", "Restaurant", new { zipcode = model.Zipcode });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}