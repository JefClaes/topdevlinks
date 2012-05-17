using System.Web.Mvc;
using TopDevLinks.Areas.Admin.Models.ViewModels;
using TopDevLinks.Commands;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using TopDevLinks.Queries;
using MongoDB.Bson;
using System;

namespace TopDevLinks.Areas.Admin.Controllers
{
    [Authorize]
    public class PostsController : MongoContextController
    {      
        public ActionResult Index()
        {
            ViewData.Model = new PostsIndexViewModel()
            {
                Categories = new SelectList(Execute(new GetPrioritizedCategoriesQuery()), "Id", "Name"),
                UpcomingPost = Execute(new GetUpcomingPostQuery())
            };
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(PostsIndexViewModel inputModel)
        {
            var model = new PostsIndexViewModel();

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
                ModelState.Clear();
            }            

            // TODO: get rid of this and use PRG
            model.Categories = new SelectList(Execute(new GetPrioritizedCategoriesQuery()), "Id", "Name");
            model.UpcomingPost = Execute(new GetUpcomingPostQuery());
            
            ViewData.Model = model;

            return View();
        }

        [HttpPost]
        public ActionResult Publish()
        {
            Execute(new PublishCommand());

            return RedirectToAction("Index");
        }
    }
}
