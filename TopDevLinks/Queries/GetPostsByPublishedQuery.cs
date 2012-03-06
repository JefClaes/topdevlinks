using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Models.Entities;
using TopDevLinks.Infrastructure;
using MongoDB.Driver.Builders;

namespace TopDevLinks.Queries
{
    public class GetPostsByPublishedQuery : Query<IEnumerable<Post>>
    {
        private bool _published;

        public GetPostsByPublishedQuery(bool published)
        {
            _published = published;
        }

        public override IEnumerable<Post> Execute()
        {
            return MongoContext.GetCollection<Post>().Find(Query.EQ("Published", _published)).ToList();
        }
    }
}