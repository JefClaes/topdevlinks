﻿using System.Web.Mvc;
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
            // TODO: replace with command that creates unpublished post if it doesn't exist yet, and then query the one unpublished post
            var unpublishedPosts = Execute(new GetPostsQuery());

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

            model.Categories = new SelectList(Execute(new GetCategoriesQuery()).Items, "Id", "Name");
            // TODO: replace with command that creates unpublished post if it doesn't exist yet, and then query the one unpublished post
            model.UnpublishedPosts = Execute(new GetPostsQuery());
            
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
