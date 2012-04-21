using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver.Builders;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Commands
{
    public class PublishCommand : Command
    {
        public override void Execute()
        {
            var unpublishedPosts = MongoContext.GetCollection<Post>().Find(Query.EQ("Published", false));

            foreach (var unpublishedPost in unpublishedPosts)
            {
                unpublishedPost.Published = true;
                unpublishedPost.PublishDate = DateTimeProvider.Now;
                EntityStore.Save<Post>(unpublishedPost);
            }            

            EntityStore.Save<Post>(new Post() { Published = false });
        }
    }
}