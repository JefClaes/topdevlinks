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
            var categories = Execute(new GetCategoriesQuery());
            var unpublishedPosts = Execute(new GetPostsQuery(published: false));

            ViewData.Model = new PostsIndexViewModel()
            {
                Categories = new SelectList(categories.Items, "Id", "Name"),
                UnpublishedPosts = unpublishedPosts
            };
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(PostsIndexViewModel inputModel)
        {
            var model = new PostsIndexViewModel();            

            if (ModelState.IsValid)
            {
                var link = new Link(
                    new Uri(inputModel.Url),
                    inputModel.Title,
                    new ObjectId(inputModel.SelectedCategoryId),
                    new ObjectId("4f8ee5b7fb1e371e880cd88b"));
                Execute(new AddLinkToUnpublishedPostCommand(link));
                ModelState.Clear();
            }            

            model.Categories = new SelectList(Execute(new GetCategoriesQuery()).Items, "Id", "Name");
            model.UnpublishedPosts = Execute(new GetPostsQuery(published: false));
            
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
