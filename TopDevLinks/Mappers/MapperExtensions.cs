using System.Collections.Generic;
using System.Linq;
using TopDevLinks.Models.Entities;
using TopDevLinks.Models.ViewModels;

namespace TopDevLinks.Mappers
{
    public static class MapperExtensions
    {
        public static PostsViewModel MapToPostsViewModel(this IEnumerable<Post> posts, IEnumerable<Category> categories)
        {
            var model = new PostsViewModel();

            foreach (var publishedPost in posts)
            {
                var post = new PostViewModel(publishedPost.Id.ToString(), publishedPost.PublishDate);

                foreach (var linkGroup in publishedPost.Links.GroupBy(l => l.CategoryId))
                {
                    var mappingCategory = categories.FirstOrDefault(c => c.Id == linkGroup.Key);
                    var category = new PostCategoryViewModel(mappingCategory.Name, mappingCategory.Priority)
                    {
                        Links = publishedPost.Links
                            .Where(l => l.CategoryId == linkGroup.Key)
                            .Select(l => new PostLinkViewModel(l.Title, l.Uri.AbsoluteUri))
                            .ToList()
                    };

                    post.Categories.Add(category);
                    post.Categories = post.Categories.OrderByDescending(c => c.Priority).ToList();
                }

                model.Posts.Add(post);
            }

            return model;
        }
    }
}