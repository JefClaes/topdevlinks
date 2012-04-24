using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopDevLinks.Areas.Admin.Models.ViewModels;
using TopDevLinks.Infrastructure;
using TopDevLinks.Queries;
using TopDevLinks.Commands;
using TopDevLinks.Models.Entities;
using MongoDB.Bson;

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
                Execute(new AddCategoryCommand(
                    inputModel.Name, inputModel.Priority.HasValue ? inputModel.Priority.Value : 0));
                ModelState.Clear();
            }

            ViewData.Model = new CategoriesIndexViewModel() { Categories = Execute(new GetCategoriesQuery()) };

            return View();
        }

        public ActionResult Edit(string id)
        {
            var category = EntityStore.Get<Category>(new ObjectId(id));

            var model = new EditCategoryViewModel()
            {
                Id = category.Id.ToString(),
                Name = category.Name,
                Priority = category.Priority
            };

            ViewData.Model = model;

            return View();
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel inputModel)
        {         
            if (ModelState.IsValid)
            {
                // TODO: Implement update
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
