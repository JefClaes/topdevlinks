using System.Web.Mvc;
using System.Linq;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using TopDevLinks.Areas.Admin.Models.ViewModels;
using TopDevLinks.Queries;

namespace TopDevLinks.Areas.Admin.Controllers
{
    [Authorize]
    public class PostsController : MongoContextController
    {      
        public ActionResult Index()
        {
            var categories = EntityStore.Get<Category>();
            var unpublishedPosts = Execute(new GetPostsQuery(published: false));

            var viewModel = new PostsIndexViewModel()
            {
                Categories = new SelectList(categories, "Id", "Name"),
                UnpublishedPosts = unpublishedPosts
            };

            ViewData.Model = viewModel;

            return View();
        }
    }
}
