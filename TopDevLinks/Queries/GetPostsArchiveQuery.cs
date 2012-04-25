using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using MongoDB.Driver.Builders;
using TopDevLinks.Models.ViewModels;

namespace TopDevLinks.Queries
{
    public class GetPostsArchiveQuery : Query<IEnumerable<ArchiveItemViewModel>>
    {
        public override IEnumerable<ArchiveItemViewModel> Execute()
        {
            var results = MongoContext.GetCollection<Post>()
                .Find(Query.EQ("Published", true))
                .SetFields(Fields.Include("Id", "PublishDate"))
                .SetSortOrder(SortBy.Descending("PublishDate"));

            return results.Select(s => new ArchiveItemViewModel(s.Id.ToString(), s.PublishDate.Value));
        }
    }
}