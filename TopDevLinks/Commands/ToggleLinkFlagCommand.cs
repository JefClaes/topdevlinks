using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using MongoDB.Bson;

namespace TopDevLinks.Commands
{
    public class ToggleLinkFlagCommand : Command
    {
        private string _postId;
        private string _linkId;

        public ToggleLinkFlagCommand(string postId, string linkId)
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
            
            link.Flag(!link.Flagged);

            post.AddLink(link);

            EntityStore.Save(post);   
        }
    }
}