using System.Web.Mvc;

namespace TopDevLinks.Areas.Admin.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {      
        public ActionResult Index()
        {
            return View();
        }
    }
}
