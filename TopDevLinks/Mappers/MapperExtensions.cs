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
            var postsViewModel = new PostsViewModel();

            foreach (var publishedPost in posts)
            {
                postsViewModel.Posts.Add(publishedPost.MapToPostViewModel(categories));
            }

            return postsViewModel;
        }

        public static PostViewModel MapToPostViewModel(this Post post, IEnumerable<Category> categories)
        {
            if (post == null) return null;

            var postViewModel = new PostViewModel(post.Id.ToString(), post.PublishDate);

            foreach (var linkGroup in post.Links.GroupBy(l => l.CategoryId))
            {
                var mappingCategory = categories.FirstOrDefault(c => c.Id == linkGroup.Key);
                // TODO: make sure we're protected against missing categories... shouldn't happen, but we don't have referential integrity like in a relational DB
                var category = new PostCategoryViewModel(mappingCategory.Name, mappingCategory.Priority)
                {
                    Links = post.Links
                        .Where(l => l.CategoryId == linkGroup.Key)
                        .Select(l => new PostLinkViewModel(l.Id.ToString(), l.Title, l.Uri.AbsoluteUri, l.Flagged))
                        .ToList()
                };

                postViewModel.Categories.Add(category);
                postViewModel.Categories = postViewModel.Categories.OrderByDescending(c => c.Priority).ToList();
            }

            return postViewModel;
        }
    }
}