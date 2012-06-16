using System;
using System.Linq;
using NUnit.Framework;
using TopDevLinks.Commands.Posts;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Tests.Commands
{
    public class ToggleLinkFlagCommandTest : CommandTestFixture
    {
        private Post _post;
        private Link _link;

        [SetUp]
        public void Setup()
        {
            var generalCategory = AddGeneralCategory();
            var user = AddUser();

            _post = new Post();
            _post.Published = false;
            _link = new Link(new Uri("http://jefclaes.be"), "Jef Claes", generalCategory.Id, user.Id);
            _post.AddLink(_link);
            _post.AddLink(new Link(new Uri("http://google.be"), "Google", generalCategory.Id, user.Id));           
                      
            MongoContext.GetCollection<Post>().Save(_post);            
        }

        [Test]
        public void Command_toggles_flag()
        {
            Execute(new ToggleLinkFlagCommand(
                Convert.ToString(_post.Id), Convert.ToString(_link.Id)));

            var post = EntityStore.Get<Post>(_post.Id);
            var link = post.Links.Where(l => l.Id == _link.Id).Single();

            Assert.IsTrue(link.Flagged);
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
