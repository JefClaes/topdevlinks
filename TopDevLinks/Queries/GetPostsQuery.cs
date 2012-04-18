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

        public GetPostsQuery(bool published)
        {
            _published = published;
        }

        public override PostsViewModel Execute()
        {
            var publishedPosts = MongoContext.GetCollection<Post>()
                .Find(Query.EQ("Published", _published))
                .ToList();
            var categories = MongoContext.GetCollection<Category>().FindAll();

            var model = new PostsViewModel();

            foreach (var publishedPost in publishedPosts)
            {
                var post = new PostViewModel(publishedPost.PublishDate);

                foreach (var linkGroup in publishedPost.Links.GroupBy(l => l.CategoryId))
                {
                    var mappingCategory = categories.Where(c => c.Id == linkGroup.Key).FirstOrDefault();                 
                    var category = new PostCategoryViewModel(mappingCategory.Name)
                    {
                        Links = publishedPost.Links
                            .Where(l => l.CategoryId == linkGroup.Key)
                            .Select(l => l.Title)
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