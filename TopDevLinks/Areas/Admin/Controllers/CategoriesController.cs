using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopDevLinks.Areas.Admin.Models.ViewModels;
using TopDevLinks.Infrastructure;
using TopDevLinks.Queries;
using TopDevLinks.Commands;

namespace TopDevLinks.Areas.Admin.Controllers
{
    public class CategoriesController : MongoContextController
    {       
        public ActionResult Index()
        {
            ViewData.Model = new CategoriesIndexViewModel() { Categories = Execute(new GetCategoriesQuery()) };

            return View();
        }

        [HttpPost]
        public ActionResult Index(CategoriesIndexViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                Execute(new AddCategoryCommand(inputModel.Name));
                ModelState.Clear();
            }

            ViewData.Model = new CategoriesIndexViewModel() { Categories = Execute(new GetCategoriesQuery()) };

            return View();
        }
    }
}
