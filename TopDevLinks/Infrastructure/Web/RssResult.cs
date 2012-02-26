using System;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace TopDevLinks.Infrastructure.Web
{
    public class RssResult : FileResult
    {
        private readonly SyndicationFeed _feed;

        public RssResult(SyndicationFeed feed)
            : base("application/rss+xml")
        {
            if (feed == null)
                throw new NullReferenceException("feed");

            _feed = feed;
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            using (var writer = XmlWriter.Create(response.OutputStream))            
                _feed.GetRss20Formatter().WriteTo(writer);            
        }
    }
}