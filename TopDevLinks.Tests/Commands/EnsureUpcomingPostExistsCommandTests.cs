using NUnit.Framework;
using TopDevLinks.Commands;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Tests.Commands
{
    public class EnsureUpcomingPostExistsCommandTests : CommandTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            MongoContext.GetCollection<Post>().Drop();
        }

        [Test]
        public void Command_doesnt_create_post_if_unpublished_post_exists()
        {
            EntityStore.Save(new Post {Published = false});
            var numberOfPosts = MongoContext.GetCollection<Post>().Count();
            Execute(new EnsureUpcomingPostExistsCommand());
            Assert.AreEqual(numberOfPosts, MongoContext.GetCollection<Post>().Count());
        }

        [Test]
        public void Command_creates_post_if_no_unpublished_post_exists()
        {
            Execute(new EnsureUpcomingPostExistsCommand());
            Assert.AreEqual(1, MongoContext.GetCollection<Post>().Count());
            Assert.IsFalse(MongoContext.GetCollection<Post>().FindOne().Published);
        }
    }
}