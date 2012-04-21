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
            var categories = EntityStore.Get<Category>();
            var unpublishedPosts = Execute(new GetPostsQuery(published: false));

            ViewData.Model = new PostsIndexViewModel()
            {
                Categories = new SelectList(categories, "Id", "Name"),
                UnpublishedPosts = unpublishedPosts
            };
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(string selectedCategoryId, string url, string title)
        {
            if (ModelState.IsValid)
            {
                var link = new Link(new Uri(url), title, new ObjectId(selectedCategoryId), new ObjectId("4f8ee5b7fb1e371e880cd88b"));
                Execute(new AddLinkToUnpublishedPostCommand(link));
            }

            var categories = EntityStore.Get<Category>();
            var unpublishedPosts = Execute(new GetPostsQuery(published: false));

            ViewData.Model = new PostsIndexViewModel()
            {
                Categories = new SelectList(categories, "Id", "Name"),
                UnpublishedPosts = unpublishedPosts,
                SelectedCategoryId = selectedCategoryId,
                Url = url,
                Title = title
            };

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
