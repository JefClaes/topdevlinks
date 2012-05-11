using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TopDevLinks.Queries
{
    public class AnyLinkByUriQuery : Query<bool>
    {
        private Uri _uri;

        public AnyLinkByUriQuery(Uri uri)
        {
            _uri = uri;
        }

        public override bool Execute()
        {
            return MongoContext
                .GetCollection<Post>()
                .Find(Query.EQ("Links.Uri", _uri.OriginalString))
                .Any();                
        }
    }
}