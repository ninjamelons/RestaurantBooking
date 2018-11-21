
using DatabaseAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserWebClient.Controllers
{
   
    public class HomeController : Controller
    {
        JustFeastDbDataContext db = new JustFeastDbDataContext();
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Searches()
        {
            ViewBag.Message = "Your searches";

            return View(db.Restaurants.ToList());
        }
    }
}