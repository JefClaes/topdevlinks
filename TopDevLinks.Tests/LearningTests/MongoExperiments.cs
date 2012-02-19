using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using NUnit.Framework;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Tests.LearningTests
{
    [TestFixture]
    public class MongoExperiments : MongoDbFixture
    {
        [SetUp]
        public void SetUp()
        {
            Database.GetCollection("posts").Drop();
        }

        [Test]
        public void can_create_post_document()
        {
            var posts = Database.GetCollection<Post>("posts");

            var post = new Post();
            post.AddLink(new Link(new Uri("http://www.google.com"), "Google"));
            post.AddLink(new Link(new Uri("http://www.facebook.com"), "Facebook"));

            posts.Insert(post);

            var retrievedPost = posts.Find(Query.EQ("Published", false)).ElementAt(0);
            Assert.AreEqual(post.Id, retrievedPost.Id);
            Assert.AreEqual(post.Links.ElementAt(0), retrievedPost.Links.ElementAt(0));
            Assert.AreEqual(post.Links.ElementAt(1), retrievedPost.Links.ElementAt(1));

            retrievedPost.AddLink(new Link(new Uri("http://www.bing.com"), "Bing"));
            posts.Save(retrievedPost);

            post = posts.Find(Query.EQ("Published", false)).ElementAt(0);
            Assert.AreEqual(post.Links.ElementAt(2), retrievedPost.Links.ElementAt(2));
        }
       
    }
}