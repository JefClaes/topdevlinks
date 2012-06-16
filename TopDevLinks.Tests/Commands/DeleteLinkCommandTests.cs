using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TopDevLinks.Models.Entities;
using TopDevLinks.Infrastructure;
using TopDevLinks.Commands;
using TopDevLinks.Commands.Posts;

namespace TopDevLinks.Tests.Commands
{
    [TestFixture]
    public class DeleteLinkCommandTests : CommandTestFixture
    {
        private Link _firstLinkAdded;
        private Post _firstPost;

        [SetUp]
        public void SetUp()
        {
            DropCollections();

            var generalCategory = AddGeneralCategory();
            var user = AddUser();

            _firstPost = new Post();
            _firstPost.Published = false;
            _firstLinkAdded = new Link(new Uri("http://jefclaes.be"), "Jef Claes", generalCategory.Id, user.Id);
            _firstPost.AddLink(_firstLinkAdded);
            _firstPost.AddLink(new Link(new Uri("http://google.be"), "Google", generalCategory.Id, user.Id));
            _firstPost.AddLink(new Link(new Uri("http://dummy.be"), "I hate dummy data", generalCategory.Id, user.Id));

            var anotherPost = new Post();
            anotherPost.PublishDate = DateTimeProvider.Now.AddDays(-1);
            anotherPost.Published = true;
            anotherPost.AddLink(new Link(new Uri("http://www.yahoo.be"), "Yahoo", generalCategory.Id, user.Id));

            MongoContext.GetCollection<Post>().Save(_firstPost);
            MongoContext.GetCollection<Post>().Save(anotherPost);
        }       

        [Test]
        public void Command_deletes_link()
        {
            Execute(new DeleteLinkCommand(
                Convert.ToString(_firstPost.Id),
                Convert.ToString(_firstLinkAdded.Id)));

            var post = EntityStore.Get<Post>(_firstPost.Id);

            Assert.IsTrue(post.Links.Any(l => l.Id != _firstPost.Id));
        }

        private void DropCollections()
        {
            MongoContext.GetCollection<Post>().Drop();
            MongoContext.GetCollection<Category>().Drop();
            MongoContext.GetCollection<User>().Drop();
        }

        private User AddUser()
        {
            var user = new User("JefClaes", "jef.claes@gmail.com");
            MongoContext.GetCollection<User>().Save(user);

            return user;
        }

        private Category AddGeneralCategory()
        {
            var generalCategory = new Category() { Name = "General", Priority = 0 };
            MongoContext.GetCollection<Category>().Save(generalCategory);

            return generalCategory;
        }       
    }
}
