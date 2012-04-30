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
            var categories = MongoContext.GetCollection<Category>().FindAll();
            
            var posts = MongoContext.GetCollection<Post>().Find(BuildPostQuery());
            posts.SetSortOrder(SortBy.Descending("PublishDate"));
            if (_take.HasValue) posts.SetLimit(_take.Value);

            var model = TransformDataToModel(posts, categories);

            return model;
        }

        private static PostsViewModel TransformDataToModel(MongoCursor<Post> posts, MongoCursor<Category> categories)
        {
            var model = new PostsViewModel();

            // TODO: This should be changed to take the category priority into account
            foreach (var publishedPost in posts)
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

        private IMongoQuery BuildPostQuery()
        {
            IMongoQuery idQuery = null;
            IMongoQuery query;

            var publishedQuery = Query.EQ("Published", _published);
            if (!string.IsNullOrEmpty(_id)) idQuery = Query.EQ("_id", new ObjectId(_id));

            query = idQuery != null ? Query.And(idQuery, publishedQuery) : publishedQuery;
          
            return query;
        }        
    }
}