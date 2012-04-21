using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.ViewModels;
using TopDevLinks.Models.Entities;
using MongoDB.Driver.Builders;

namespace TopDevLinks.Queries
{
    public class GetPostsQuery : Query<PostsViewModel>
    {
        private bool _published;
        private int? _take;

        public GetPostsQuery(bool published, int? take = null)
        {
            _published = published;
            _take = take;
        }

        public override PostsViewModel Execute()
        {
            var publishedQuery = Query.EQ("Published", _published);
            var publishedCursor = MongoContext.GetCollection<Post>().Find(publishedQuery);
            publishedCursor.SetSortOrder(SortBy.Descending("PublishDate"));
            if (_take.HasValue) publishedCursor.SetLimit(_take.Value);
              
            var categories = MongoContext.GetCollection<Category>().FindAll();

            var model = new PostsViewModel();

            foreach (var publishedPost in publishedCursor)
            {
                var post = new PostViewModel(publishedPost.PublishDate);

                foreach (var linkGroup in publishedPost.Links.GroupBy(l => l.CategoryId))
                {
                    var mappingCategory = categories.Where(c => c.Id == linkGroup.Key).FirstOrDefault();                 
                    var category = new PostCategoryViewModel(mappingCategory.Name)
                    {
                        Links = publishedPost.Links
                            .Where(l => l.CategoryId == linkGroup.Key)
                            .Select(l => new PostLinkViewModel(l.Title, l.Uri.AbsoluteUri))
                            .ToList()
                    };

                    post.Categories.Add(category);
                }

                model.Posts.Add(post);
            }

            return model;
        }
    }
}