using System;
using NUnit.Framework;
using TopDevLinks.Models.Entities;
using TopDevLinks.Commands;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace TopDevLinks.Tests.Commands
{
    [TestFixture]
    public class PublishCommandTest : CommandTestFixture
    {        
        private ObjectId _unpublishedPostId;

        [SetUp]
        public void Setup()
        {
            MongoContext.GetCollection<Post>().Drop();

            var publishedPost = new Post() { PublishDate = new DateTime(2011, 10, 10), Published = true };
            var unpublishedPost = new Post() { PublishDate = null, Published = false };

            EntityStore.Save(publishedPost);
            EntityStore.Save(unpublishedPost);
     
            _unpublishedPostId = unpublishedPost.Id;
        }

        [Test]
        public void Command_publishes_unpublished_post()
        {
            Execute(new PublishCommand());

            var unpublishedPost = EntityStore.Get<Post>(_unpublishedPostId);

            Assert.IsTrue(unpublishedPost.Published);
        }

        [Test]
        public void Command_sets_publish_date()
        {
            Execute(new PublishCommand());

            var unpublishedPost = EntityStore.Get<Post>(_unpublishedPostId);

            Assert.IsNotNull(unpublishedPost.PublishDate);
        }

        [Test]
        public void Command_adds_new_unpublished_post()
        {
            Execute(new PublishCommand());

            var postCount = MongoContext.GetCollection<Post>().Count();
            var unpublishedPost = MongoContext.GetCollection<Post>().Find(Query.EQ("Published", false));

            Assert.AreEqual(3, postCount);
            Assert.IsNotNull(unpublishedPost);
        }
    }
}
