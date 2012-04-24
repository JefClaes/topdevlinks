using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using MongoDB.Driver.Builders;

namespace TopDevLinks.Queries
{
    public class GetPostsArchiveQuery : Query<IEnumerable<DateTime?>>
    {
        public override IEnumerable<DateTime?> Execute()
        {
            var results = MongoContext.GetCollection<Post>()
                .Find(Query.EQ("Published", true))
                .SetFields(Fields.Include("PublishDate"))
                .SetSortOrder(SortBy.Descending("PublishDate"));

            return results.Select(s => s.PublishDate);
        }
    }
}