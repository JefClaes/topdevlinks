using System.Web.Mvc;
using TopDevLinks.Areas.Admin.Models.ViewModels;
using TopDevLinks.Commands;
using TopDevLinks.Infrastructure;
using TopDevLinks.Infrastructure.Web;
using TopDevLinks.Models.Entities;
using TopDevLinks.Queries;
using MongoDB.Bson;
using System;

namespace TopDevLinks.Areas.Admin.Controllers
{
    [Authorize]
    public class PostsController : MongoContextController
    {      
        [RestoreModelStateFromTempData]
        public ActionResult Index()
        {
            Execute(new EnsureUpcomingPostExistsCommand());
            ViewData.Model = new PostsIndexViewModel()
            {
                Categories = new SelectList(Execute(new GetPrioritizedCategoriesQuery()), "Id", "Name"),
                UpcomingPost = Execute(new GetUpcomingPostQuery())
            };
            
            return View();
        }

        [HttpPost]
        [SetTempDataWhenModelStateInvalid]
        public ActionResult Index(PostsIndexViewModel inputModel)
        {
            Uri url;
            if (!Uri.TryCreate(inputModel.Url, UriKind.Absolute, out url))
                ModelState.AddModelError("Url", "The Url is not valid.");
            if (Execute(new AnyLinkByUriQuery(url)))
                ModelState.AddModelError("Url", "The url was already submitted.");

            if (ModelState.IsValid)
            {
                var userId = Execute(new FindUserByLoginQuery(HttpContext.User.Identity.Name)).Id;
                var link = new Link(
                    url,
                    inputModel.Title,
                    new ObjectId(inputModel.SelectedCategoryId),
                    userId);
                Execute(new AddLinkToUnpublishedPostCommand(link));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Publish()
        {
            Execute(new PublishCommand());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [SetTempDataWhenModelStateInvalid]
        public ActionResult Delete(string id)
        {
            Execute(new DeleteLinkCommand(id));

            return RedirectToAction("Index");
        }
    }
}
