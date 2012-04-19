using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TopDevLinks.Models.Entities;
using MongoDB.Driver.Builders;
using TopDevLinks.Models.ViewModels;
using TopDevLinks.Queries;

namespace TopDevLinks.Tests.Queries
{
    public class GetPostQueryTests : QueryTestFixture
    {
        private Category _dotNetCategory;
        private Category _nodeCategory;
        private Post _publishedPost;
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
            var model = Execute<PostsViewModel>(new GetPostsQuery(published: true));

            Assert.IsTrue(model.Posts.Count == 1);
        }

        [Test]
        public void Query_returns_one_unpublished_post()
        {
            var model = Execute<PostsViewModel>(new GetPostsQuery(published: false));

            Assert.IsTrue(model.Posts.Count == 1);
        }

        [Test]
        public void Query_returns_model_with_correct_categories()
        {
            var model = Execute<PostsViewModel>(new GetPostsQuery(published: true));

            var publishedPost = model.Posts.Single();

            Assert.IsTrue(publishedPost.Categories.Where(c => c.Name == _dotNetCategory.Name).Count() == 1);
            Assert.IsTrue(publishedPost.Categories.Where(c => c.Name == _nodeCategory.Name).Count() == 1);
        }

        [Test]
        public void Query_returns_model_with_correct_number_of_links()
        {
            var model = Execute<PostsViewModel>(new GetPostsQuery(published: true));

            var publishedPost = model.Posts.Single();

            Assert.IsTrue(publishedPost.Categories.Where(c => c.Name == _dotNetCategory.Name).First().Links.Count() == 3);
            Assert.IsTrue(publishedPost.Categories.Where(c => c.Name == _nodeCategory.Name).First().Links.Count() == 1);
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

            var unpublishedPost = new Post();
            unpublishedPost.PublishDate = _date.AddDays(7);
            unpublishedPost.Published = false;
            unpublishedPost.AddLink(new Link(new Uri("http://jefclaes.be/4"), "Jef Claes link 4", _dotNetCategory.Id, _user.Id));
            unpublishedPost.AddLink(new Link(new Uri("http://davybrion.com/2"), "Davy Brion link 2", _nodeCategory.Id, _user.Id));

            EntityStore.Save<Post>(_publishedPost);
            EntityStore.Save<Post>(unpublishedPost);
        }
    }
}
