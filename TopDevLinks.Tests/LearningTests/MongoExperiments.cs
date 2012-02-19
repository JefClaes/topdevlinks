using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using NUnit.Framework;
using TopDevLinks.Models.Documents;

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

            var post = new Post
                           {
                               Links = new List<Link>
                                           {
                                               new Link {Uri = new Uri("http://www.google.com"), Title = "Google"},
                                               new Link {Uri = new Uri("http://www.facebook.com"), Title = "Facebook"}
                                           },
                           };

            posts.Insert(post);

            var retrievedPost = posts.Find(Query.EQ("Published", false)).ElementAt(0);
            Assert.IsNotNull(retrievedPost);
        }
       
    }
}