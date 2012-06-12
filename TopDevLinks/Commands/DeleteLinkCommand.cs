using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace TopDevLinks.Commands
{
    public class DeleteLinkCommand : Command
    {
        private string _id;

        public DeleteLinkCommand(string id)
        {
            _id = id;
        }

        public override void Execute()
        {
            var post = MongoContext.GetCollection<Post>()
                .FindOne(Query.EQ("Links._id", new ObjectId(_id)));
            var link = post.Links
                .Where(l => Convert.ToString(l.Id) == _id)
                .First();

            post.RemoveLink(link);

            EntityStore.Save(post);            
        }
    }
}