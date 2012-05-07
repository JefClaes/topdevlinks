using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TopDevLinks.Models.Entities;
using TopDevLinks.Infrastructure;
using TopDevLinks.Queries;

namespace TopDevLinks.Tests.Queries
{
    [TestFixture]
    public class AnyLinkByUriQueryTests : QueryTestFixture
    {      
        [SetUp]
        public void SetUp()
        {
            DropCollections();

            var category = new Category() {
                Name = "Test", 
                Priority = 1
            };
            EntityStore.Save(category);

            var user = new User("Jef", "jef.claes@gmail.com");
            EntityStore.Save(user);

            var post1 = new Post()
            {
                PublishDate = DateTimeProvider.Now,
                Published = true
            };
            post1.AddLink(new Link(new Uri("http://www.jefclaes.be"), "Jef Claes", category.Id, user.Id));
            
            var post2 = new Post()
            {
                Published = false
            };
            post2.AddLink(new Link(new Uri("http://www.davybrion.com"), "Davy Brion", category.Id, user.Id));

            EntityStore.Save(post1);            
            EntityStore.Save(post2);
        }
        
        [Test]
        public void Query_returns_false_if_uri_not_in_one_of_the_documents()
        {
            Assert.AreEqual(false, Execute(new AnyLinkByUriQuery(new Uri("http://www.google.be"))));
        }

        [Test]
        public void Query_returns_true_if_uri_already_in_one_of_the_documents()
        {            
            // I can't figure out why this doesn't work right here, but works in the PostsController.
            Assert.AreEqual(true, Execute(new AnyLinkByUriQuery(new Uri("http://www.davybrion.com"))));
        }

        private void DropCollections()
        {
            MongoContext.GetCollection<Post>().Drop();
            MongoContext.GetCollection<Category>().Drop();
            MongoContext.GetCollection<User>().Drop();
        }       
    }
}
