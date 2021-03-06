﻿using NUnit.Framework;
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
        public void Query_returns_unpublished_post()
        {
            var upcomingPost = Execute(new GetUpcomingPostQuery());
            Assert.AreEqual(_unpublishedPost.Id.ToString(), upcomingPost.Id);
        }

        [Test]
        public void Query_returns_first_unpublished_post_if_multiple_exist()
        {
            EntityStore.Save(new Post() {Published = false});
            var upcomingPost = Execute(new GetUpcomingPostQuery());
            Assert.AreEqual(_unpublishedPost.Id.ToString(), upcomingPost.Id);
        }

        [Test]
        public void Query_returns_null_if_no_unpublished_post_exists()
        {
            MongoContext.GetCollection<Post>().Drop();
            Assert.IsNull(Execute(new GetUpcomingPostQuery()));
        }
    }
}