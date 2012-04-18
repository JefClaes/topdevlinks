using System.Web.Mvc;
using System.Linq;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using TopDevLinks.Areas.Admin.Models.ViewModels;

namespace TopDevLinks.Areas.Admin.Controllers
{
    [Authorize]
    public class PostsController : MongoContextController
    {      
        public ActionResult Index()
        {
            var categories = EntityStore.Get<Category>();

            var viewModel = new PostsIndexViewModel()
            {
                Categories = new SelectList(categories, "Id", "Name")
            };

            ViewData.Model = viewModel;

            return View();
        }
    }
}
