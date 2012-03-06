using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TopDevLinks.Models.Entities;
using TopDevLinks.Queries;

namespace TopDevLinks.Tests.Queries
{
    public class GetPostsByPublishedQueryTests : QueryTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            MongoContext.GetCollection<Post>().Drop();

            EntityStore.Save(new Post() { Published = false });
            EntityStore.Save(new Post() { Published = true });
            EntityStore.Save(new Post() { Published = true });
        }

        [Test]
        public void Query_for_published_returns_two_documents()
        {
            Assert.IsTrue(Execute(new GetPostsByPublishedQuery(true)).Count() == 2);
        }

        [Test]
        public void Query_for_not_published_returns_one_document()
        {
            Assert.IsTrue(Execute(new GetPostsByPublishedQuery(false)).Count() == 1);
        }
    }
}
