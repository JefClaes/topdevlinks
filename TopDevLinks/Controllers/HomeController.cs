using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TopDevLinks.Controllers
{
    public class HomeController : Controller
    {      
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Archive(DateTime? date)
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
