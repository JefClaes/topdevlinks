﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using TopDevLinks.Commands;
using MongoDB.Driver.Builders;

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
        public void adds_link_to_unpublished_posts()
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
