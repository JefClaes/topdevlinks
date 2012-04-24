using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Areas.Admin.Models.ViewModels;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using MongoDB.Driver.Builders;

namespace TopDevLinks.Queries
{
    public class GetCategoriesQuery : Query<CategoriesViewModel>
    {
        public override CategoriesViewModel Execute()
        {
            var items = MongoContext
                .GetCollection<Category>()
                .FindAll()
                .SetSortOrder(SortBy.Descending("Priority"))
                .Select(c => new CategoryViewModel(c.Id.ToString(), c.Name, c.Priority));

            return new CategoriesViewModel(items);
        }
    }
}