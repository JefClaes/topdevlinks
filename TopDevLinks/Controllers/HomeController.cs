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
            ViewData.Model = Execute(new GetPostsQuery(published: true));

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
