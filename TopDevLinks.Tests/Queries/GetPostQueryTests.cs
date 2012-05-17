using System;
using System.Linq;
using NUnit.Framework;
using TopDevLinks.Models.Entities;
using TopDevLinks.Models.ViewModels;
using TopDevLinks.Queries;

namespace TopDevLinks.Tests.Queries
{
    public class GetPostQueryTests : QueryTestFixture
    {
        private Category _dotNetCategory;
        private Category _nodeCategory;
        private Post _publishedPost;
        private Post _unpublishedPost;
        private User _user;
        private DateTime _date = new DateTime(2011, 3, 4);

        [SetUp]
        public void SetUp()
        {         
            SetUpCategories();
            SetUpUser();
            SetupPosts();
        }

        [Test]
        public void Query_returns_one_published_post()
        {
            var model = Execute<PostsViewModel>(new GetPostsQuery());

            Assert.IsTrue(model.Posts.Count == 1);
        }

        // TODO: get rid of this and cover the scenario with dedicated tests for GetUnpublishedPost query
        //[Test]
        //public void Query_returns_one_unpublished_post()
        //{
        //    var model = Execute<PostsViewModel>(new GetPostsQuery(published: false));

        //    Assert.IsTrue(model.Posts.Count == 1);
        //}

        [Test]
        public void Query_returns_model_with_correct_categories()
        {
            var model = Execute<PostsViewModel>(new GetPostsQuery());

            var publishedPost = model.Posts.Single();

            Assert.IsTrue(publishedPost.Categories.Where(c => c.Name == _dotNetCategory.Name).Count() == 1);
            Assert.IsTrue(publishedPost.Categories.Where(c => c.Name == _nodeCategory.Name).Count() == 1);
        }

        [Test]
        public void Query_returns_published_model_by_id()
        {
            var model = Execute<PostsViewModel>(new GetPostsQuery(_publishedPost.Id.ToString()));

            Assert.AreEqual(1, model.Posts.Count);            
        }

        [Test]
        public void Query_doesnt_return_unpublished_posts_by_id()
        {
            var model = Execute<PostsViewModel>(new GetPostsQuery(_unpublishedPost.Id.ToString()));

            Assert.AreEqual(0, model.Posts.Count);
        }

        [Test]
        public void Query_returns_model_with_correct_number_of_links()
        {
            var model = Execute<PostsViewModel>(new GetPostsQuery());

            var publishedPost = model.Posts.Single();

            Assert.IsTrue(publishedPost.Categories.Where(c => c.Name == _dotNetCategory.Name).First().Links.Count() == 3);
            Assert.IsTrue(publishedPost.Categories.Where(c => c.Name == _nodeCategory.Name).First().Links.Count() == 1);
        }

        [Test]
        public void Query_returns_one_model_when_take_is_set_to_one()
        {
            var model = Execute<PostsViewModel>(new GetPostsQuery(take: 1));

            Assert.AreEqual(1, model.Posts.Count);
        }

        private void SetUpUser() 
        {
            MongoContext.GetCollection<User>().Drop();

            _user = new User("Nemmie", "jef.claes@gmail.com");

            EntityStore.Save<User>(_user);
        }

        private void SetUpCategories()
        {
            MongoContext.GetCollection<Category>().Drop();

            _dotNetCategory = new Category() { Name = ".NET", Priority = 100 };
            _nodeCategory = new Category() { Name = "Node.js", Priority = 10 };

            EntityStore.Save<Category>(_dotNetCategory);
            EntityStore.Save<Category>(_nodeCategory);
        }

        private void SetupPosts()
        {
            MongoContext.GetCollection<Post>().Drop();

            _publishedPost = new Post();
            _publishedPost.PublishDate = _date;
            _publishedPost.Published = true;
            _publishedPost.AddLink(new Link(new Uri("http://jclaes.blogspot.com/1"), "Jef Claes link 1", _dotNetCategory.Id, _user.Id));
            _publishedPost.AddLink(new Link(new Uri("http://jclaes.blogspot.com/2"), "Jef Claes link 2", _dotNetCategory.Id, _user.Id));
            _publishedPost.AddLink(new Link(new Uri("http://jclaes.blogspot.com/3"), "Jef Claes link 3", _dotNetCategory.Id, _user.Id));
            _publishedPost.AddLink(new Link(new Uri("http://davybrion.com/1"), "Davy Brion link 1", _nodeCategory.Id, _user.Id));

            _unpublishedPost = new Post();
            _unpublishedPost.PublishDate = _date.AddDays(7);
            _unpublishedPost.Published = false;
            _unpublishedPost.AddLink(new Link(new Uri("http://jefclaes.be/4"), "Jef Claes link 4", _dotNetCategory.Id, _user.Id));
            _unpublishedPost.AddLink(new Link(new Uri("http://davybrion.com/2"), "Davy Brion link 2", _nodeCategory.Id, _user.Id));

            EntityStore.Save<Post>(_publishedPost);
            EntityStore.Save<Post>(_unpublishedPost);
        }
    }
}
