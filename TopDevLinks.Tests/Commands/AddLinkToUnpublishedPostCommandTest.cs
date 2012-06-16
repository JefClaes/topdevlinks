using System;
using System.Linq;
using MongoDB.Driver.Builders;
using NUnit.Framework;
using TopDevLinks.Commands.Posts;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Tests.Commands
{
    [TestFixture]
    public class AddLinkToUnpublishedPostCommandTest : CommandTestFixture
    {
        private User _user;
        private Post _unpublishedPost;
        private Category _category;

        [SetUp]
        public void SetUp()
        {
            SetUpUser();
            SetUpCategories();           
            SetupPosts();
        }

        [Test]
        public void Command_adds_link_to_unpublished_posts()
        {
            var link = new Link(new Uri("http://jefclaes.be/Post"), "Post", _category.Id, _user.Id);

            Execute(new AddLinkToUnpublishedPostCommand(link));

            var unpublishedPosts = MongoContext.GetCollection<Post>().Find(Query.EQ("Published", false));

            foreach (var unpublishedPost in unpublishedPosts)
            {
                Assert.AreEqual(link, unpublishedPost.Links.First());
            }            
        }

        private void SetUpUser()
        {
            MongoContext.GetCollection<User>().Drop();

            _user = new User("Nemmie", "jef.claes@gmail.com");

            EntityStore.Save<User>(_user);
        }

        private void SetupPosts()
        {
            MongoContext.GetCollection<Post>().Drop();
         
            _unpublishedPost = new Post();
            _unpublishedPost.PublishDate = DateTime.Now.AddDays(7);
            _unpublishedPost.Published = false;          
                        
            EntityStore.Save<Post>(_unpublishedPost);
        }

        private void SetUpCategories()
        {
            MongoContext.GetCollection<Category>().Drop();

            _category = new Category() { Name = "Some category", Priority = 100 };

            EntityStore.Save<Category>(_category);            
        }
    }
}
