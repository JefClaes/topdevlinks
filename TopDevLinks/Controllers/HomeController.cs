using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Linq;
using TopDevLinks.Infrastructure;
using TopDevLinks.Infrastructure.Web;
using TopDevLinks.Queries;
using System.IO;
using System.Text;
using System.Web;

namespace TopDevLinks.Controllers
{
    public class HomeController : MongoContextController
    {
        public ActionResult Index()
        {
            ViewData.Model = Execute(new GetPostsQuery(published: true, take: 3));

            return View();
        }

        public ActionResult Post(string id)
        {
            ViewData.Model = Execute(new GetPostsQuery(id));

            return View();
        }

        public ActionResult Archive()
        {
            ViewData.Model = Execute(new GetPostsArchiveQuery());

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Feed()
        {
            var model = Execute(new GetPostsQuery(published: true));                            

            var items = new List<SyndicationItem>();
            foreach (var post in model.Posts)
            {                  
                var content = new StringBuilder();
                foreach (var category in post.Categories) 
                {                    
                    content.AppendLine("<b>" + category.Name + "</b>");
                    content.AppendLine("<ul>");
                    foreach (var link in category.Links)                     
                        content.Append(string.Format("<li><a href='{0}'>{1}</a></li>", link.Url, link.Title));
                    content.AppendLine("</ul>");
                    content.AppendLine("<br/>");                 
                }

                items.Add(
                    new SyndicationItem()
                    {
                        Id = post.Id,
                        LastUpdatedTime = post.PublishDate.Value,
                        PublishDate = post.PublishDate.Value,                        
                        Title = new TextSyndicationContent(post.PublishDate.Value.ToShortDateString()),
                        Content = new TextSyndicationContent(content.ToString(), TextSyndicationContentKind.Html)
                    }
                );                
            }

            var lastUpdatedTime = model.Posts.Any() ?
                model.Posts.OrderByDescending(p => p.PublishDate).First().PublishDate.Value : DateTime.MinValue;
            var feedUri = new Uri(Url.Action("Feed", "Home", null, Request.Url.Scheme));
            
            var feed = new SyndicationFeed("TopDevLinks", "TopDevLinks feed", feedUri, null, lastUpdatedTime)
            {
                Language = "eng",                
                Items = items,
                BaseUri = feedUri,                
            };            

            return new RssResult(feed);
        }        
    }
}
