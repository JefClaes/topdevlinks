using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using TopDevLinks.Infrastructure;
using TopDevLinks.Infrastructure.Web;
using TopDevLinks.Queries;

namespace TopDevLinks.Controllers
{
    public class HomeController : MongoContextController
    {      
        public ActionResult Index()
        {            
            ViewData.Model = Execute(new GetPostsQuery(published: true, take: 3));          

            return View();
        }       

        public ActionResult Archive()
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
