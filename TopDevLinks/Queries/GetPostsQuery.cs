using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TopDevLinks.Infrastructure;
using TopDevLinks.Mappers;
using TopDevLinks.Models.Entities;
using TopDevLinks.Models.ViewModels;
using MongoDB.Bson;

namespace TopDevLinks.Queries
{
    public class GetPostsQuery : Query<PostsViewModel>
    {
        private int? _take;
        private string _id;

        public GetPostsQuery(int? take = null)
        {
            _take = take;
        }

        public GetPostsQuery(string id)
        {
            _id = id;
        }

        public override PostsViewModel Execute()
        {
            return GetPosts().MapToPostsViewModel(Execute(new GetPrioritizedCategoriesQuery()));
        }

        private MongoCursor<Post> GetPosts()
        {
            var posts = MongoContext.GetCollection<Post>().Find(BuildPostQuery());
            posts.SetSortOrder(SortBy.Descending("PublishDate"));
            if (_take.HasValue) posts.SetLimit(_take.Value);

            return posts;
        }

        private IMongoQuery BuildPostQuery()
        {
            IMongoQuery idQuery = null;

            var publishedQuery = Query.EQ("Published", true);
            if (!string.IsNullOrEmpty(_id)) idQuery = Query.EQ("_id", new ObjectId(_id));

            return idQuery != null ? Query.And(idQuery, publishedQuery) : publishedQuery;
        }        
    }
}