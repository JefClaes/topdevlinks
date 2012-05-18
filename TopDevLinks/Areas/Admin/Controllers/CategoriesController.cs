using System.Linq;
using System.Web.Mvc;
using MongoDB.Bson;
using TopDevLinks.Areas.Admin.Models.ViewModels;
using TopDevLinks.Commands;
using TopDevLinks.Infrastructure;
using TopDevLinks.Infrastructure.Web;
using TopDevLinks.Models.Entities;
using TopDevLinks.Queries;

namespace TopDevLinks.Areas.Admin.Controllers
{
    public class CategoriesController : MongoContextController
    {       
        [RestoreModelStateFromTempData]
        public ActionResult Index()
        {
            ViewData.Model = new CategoriesIndexViewModel
                                 {
                                     Categories = Execute(new GetPrioritizedCategoriesQuery())
                                         .Select(c => new CategoryViewModel(c.Id.ToString(), c.Name, c.Priority))
                                 };
                
            return View();
        }

        [HttpPost]
        [SetTempDataWhenModelStateInvalid]
        public ActionResult Index(CategoriesIndexViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                Execute(new AddCategoryCommand(
                    inputModel.Name, inputModel.Priority.HasValue ? inputModel.Priority.Value : 0));
            }

            return RedirectToAction("Index");
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
            if (!ModelState.IsValid)
                return View();
                
            Execute(new UpdateCategoryCommand(inputModel.Id, inputModel.Name, inputModel.Priority));
                
            return RedirectToAction("Index");                                  
        }
    }
}
