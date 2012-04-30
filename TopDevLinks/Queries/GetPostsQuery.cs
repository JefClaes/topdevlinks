using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using TopDevLinks.Models.ViewModels;
using MongoDB.Bson;

namespace TopDevLinks.Queries
{
    public class GetPostsQuery : Query<PostsViewModel>
    {
        private bool _published;
        private int? _take;
        private string _id;

        public GetPostsQuery(bool published, int? take = null)
        {
            _published = published;
            _take = take;
        }

        public GetPostsQuery(string id)
        {
            _published = true;
            _id = id;
        }

        public override PostsViewModel Execute()
        {
            var categories = GetCategories();
            var posts = GetPosts();

            return TransformDocumentsToModel(posts, categories);            
        }

        private MongoCursor<Post> GetPosts()
        {
            var posts = MongoContext.GetCollection<Post>().Find(BuildPostQuery());
            posts.SetSortOrder(SortBy.Descending("PublishDate"));
            if (_take.HasValue) posts.SetLimit(_take.Value);

            return posts;
        }

        private MongoCursor<Category> GetCategories()
        {
            var categories = MongoContext.GetCollection<Category>().FindAll();
            categories.SetSortOrder(SortBy.Descending("Priority"));

            return categories;
        }

        private static PostsViewModel TransformDocumentsToModel(MongoCursor<Post> posts, MongoCursor<Category> categories)
        {
            var model = new PostsViewModel();            
            
            foreach (var publishedPost in posts)
            {
                var post = new PostViewModel(publishedPost.PublishDate);

                foreach (var linkGroup in publishedPost.Links.GroupBy(l => l.CategoryId))
                {
                    var mappingCategory = categories.Where(c => c.Id == linkGroup.Key).FirstOrDefault();
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

        private IMongoQuery BuildPostQuery()
        {
            IMongoQuery idQuery = null;
            IMongoQuery query = null;

            var publishedQuery = Query.EQ("Published", _published);
            if (!string.IsNullOrEmpty(_id)) idQuery = Query.EQ("_id", new ObjectId(_id));

            query = idQuery != null ? Query.And(idQuery, publishedQuery) : publishedQuery;
          
            return query;
        }        
    }
}