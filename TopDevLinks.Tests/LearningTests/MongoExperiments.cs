using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using NUnit.Framework;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using TopDevLinks.Tests.Queries;
using TopDevLinks.Queries;

namespace TopDevLinks.Tests.LearningTests
{
    [TestFixture]
    [Explicit]
    public class MongoExperiments : QueryTestFixture
    {
        private MongoTestContext _mongoContext = new MongoTestContext();

        [SetUp]
        public void SetUp()
        {
            _mongoContext.Database.GetCollection("posts").Drop();
        }

        [Test]
        public void can_create_post_document()
        {
            var posts = _mongoContext.Database.GetCollection<Post>("posts");

            var post = new Post();
            post.AddLink(new Link(new Uri("http://www.google.com"), "Google", ObjectId.GenerateNewId(), ObjectId.GenerateNewId()));
            post.AddLink(new Link(new Uri("http://www.facebook.com"), "Facebook", ObjectId.GenerateNewId(), ObjectId.GenerateNewId()));

            posts.Insert(post);

            var retrievedPost = posts.Find(Query.EQ("Published", false)).ElementAt(0);
            Assert.AreEqual(post.Id, retrievedPost.Id);
            Assert.AreEqual(post.Links.ElementAt(0), retrievedPost.Links.ElementAt(0));
            Assert.AreEqual(post.Links.ElementAt(1), retrievedPost.Links.ElementAt(1));

            retrievedPost.AddLink(new Link(new Uri("http://www.bing.com"), "Bing", ObjectId.GenerateNewId(), ObjectId.GenerateNewId()));
            posts.Save(retrievedPost);

            post = posts.Find(Query.EQ("Published", false)).ElementAt(0);
            Assert.AreEqual(post.Links.ElementAt(2), retrievedPost.Links.ElementAt(2));
        }        
    }
}