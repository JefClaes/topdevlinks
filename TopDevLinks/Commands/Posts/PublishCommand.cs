using MongoDB.Driver.Builders;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Commands.Posts
{
    public class PublishCommand : Command
    {
        public override void Execute()
        {
            var upcomingPost = MongoContext.GetCollection<Post>()
                .FindOne(Query.EQ("Published", false));

            upcomingPost.Published = true;
            upcomingPost.PublishDate = DateTimeProvider.Now;
            EntityStore.Save(upcomingPost);

            Execute(new EnsureUpcomingPostExistsCommand());
        }
    }
}