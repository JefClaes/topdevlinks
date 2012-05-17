using System.Collections.Generic;
using MongoDB.Driver.Builders;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Queries
{
    public class GetPrioritizedCategoriesQuery : Query<IEnumerable<Category>>
    {
        public override IEnumerable<Category> Execute()
        {
            return MongoContext.GetCollection<Category>().FindAll()
                .SetSortOrder(SortBy.Descending("Priority"));
        }
    }
}