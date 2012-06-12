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
        private string _postId;
        private string _linkId;

        public DeleteLinkCommand(string postId, string linkId)
        {
            _postId = postId;
            _linkId = linkId;
        }

        public override void Execute()
        {
            var post = EntityStore.Get<Post>(new ObjectId(_postId));               
            var link = post.Links
                .Where(l => Convert.ToString(l.Id) == _linkId)
                .First();

            post.RemoveLink(link);

            EntityStore.Save(post);            
        }
    }
}