using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopDevLinks.Areas.Admin.Models.ViewModels;
using TopDevLinks.Infrastructure;
using TopDevLinks.Queries;

namespace TopDevLinks.Areas.Admin.Controllers
{
    public class CategoriesController : MongoContextController
    {       
        public ActionResult Index()
        {
            ViewData.Model = new CategoriesIndexViewModel() { Categories = Execute(new GetCategoriesQuery()) };

            return View();
        }
    }
}
