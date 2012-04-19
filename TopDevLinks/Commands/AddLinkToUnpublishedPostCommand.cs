using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using MongoDB.Driver.Builders;

namespace TopDevLinks.Commands
{
    public class AddLinkToUnpublishedPostCommand : Command
    {
        private Link _link;

        public AddLinkToUnpublishedPostCommand(Link link)
        {
            _link = link;
        }

        public override void Execute()
        {
            var unpublishedPosts = MongoContext.GetCollection<Post>().Find(Query.EQ("Published", false));

            foreach (var unpublishedPost in unpublishedPosts)
            {
                unpublishedPost.AddLink(_link);
                EntityStore.Save<Post>(unpublishedPost);
            }
        }
    }
}