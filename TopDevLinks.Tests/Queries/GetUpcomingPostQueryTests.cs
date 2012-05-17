using NUnit.Framework;
using TopDevLinks.Models.Entities;
using TopDevLinks.Queries;

namespace TopDevLinks.Tests.Queries
{
    public class GetUpcomingPostQueryTests : QueryTestFixture
    {
        private Post _publishedPost = new Post {Published = true};
        private Post _unpublishedPost = new Post {Published = false};

        [SetUp]
        public void SetUp()
        {
            MongoContext.GetCollection<Post>().Drop();
            EntityStore.Save(_publishedPost);
            EntityStore.Save(_unpublishedPost);
        }

        [Test]
        public void Query_Returns_Unpublished_Post()
        {
            var upcomingPost = Execute(new GetUpcomingPostQuery());
            Assert.AreEqual(_unpublishedPost.Id.ToString(), upcomingPost.Id);
        }
    }
}