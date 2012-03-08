using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TopDevLinks.Infrastructure.Web;
using System.ServiceModel.Syndication;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.ViewModels;
using TopDevLinks.Queries;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Controllers
{
    public class HomeController : MongoContextController
    {      
        public ActionResult Index()
        {
            var publishedPosts = Execute(new GetPostsByPublishedQuery(true));            
            var categories = EntityStore.Get<Category>();

            var model = new PostsViewModel();

            foreach (var publishedPost in publishedPosts)
            {
                var post = new PostViewModel(publishedPost.PublishDate);                

                foreach (var linkGroup in publishedPost.Links.GroupBy(l => l.CategoryId))
                {
                    var mappingCategory = categories.Where(c => c.Id == linkGroup.Key).FirstOrDefault();
                    var categoryName = mappingCategory == null ? "Not defined" : mappingCategory.Name;

                    var category = new PostCategoryViewModel(categoryName)
                    {
                        Links = publishedPost.Links
                            .Where(l => l.CategoryId == linkGroup.Key)
                            .Select(l => l.Title)
                            .ToList()
                    };

                    post.Categories.Add(category);
                }
                
                model.Posts.Add(post);
            }           
            
            ViewData.Model = model;

            return View();
        }

        public ActionResult Archive(DateTime? date)
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Feed()
        {
            var feed = new SyndicationFeed("Test", "Test", null, "Id", DateTime.Now);
            feed.Items = new List<SyndicationItem>()
            {
                new SyndicationItem("Title 1", "Content", null),
                new SyndicationItem("Title 2", "Content", null)
            };          
            
            return new RssResult(feed);
        }
    }
}
