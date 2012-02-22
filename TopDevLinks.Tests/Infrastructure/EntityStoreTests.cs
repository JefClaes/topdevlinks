using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using NUnit.Framework;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Tests.Infrastructure
{
    [TestFixture]
    public class EntityStoreTests : MongoDbFixture
    {
        private EntityStore _entityStore = new EntityStore("mongodb://localhost", "topdevlinks_test");

        [SetUp]
        public void SetUp()
        {
            Database.GetCollection("posts").RemoveAll();
        }

        [Test]
        public void save_can_add_document()
        {
            var post = new Post();
            _entityStore.Save(post);
            Assert.IsNotNull(Database.GetCollection<Post>("posts").Find(Query.EQ("Id", post.Id)));
        }

        private Post GetPost(ObjectId id)
        {
            return Database.GetCollection<Post>("posts").Find(Query.EQ("_id", id)).FirstOrDefault();
        }

        [Test]
        public void save_can_update_document()
        {
            var post = new Post();
            _entityStore.Save(post);

            post.Published = true;
            _entityStore.Save(post);

            Assert.True(GetPost(post.Id).Published);
        }

        [Test]
        public void get_can_retrieve_existing_document()
        {
            var post = new Post();
            Database.GetCollection("posts").Save(post);

            Assert.IsNotNull(_entityStore.Get<Post>(post.Id));
        }

        [Test]
        public void remove_can_remove_existing_document_by_reference()
        {
            var post = new Post();
            Database.GetCollection("posts").Save(post);

            _entityStore.Remove(post);

            Assert.IsNull(GetPost(post.Id));
        }

        [Test]
        public void remove_can_remove_existing_document_by_id()
        {
            var post = new Post();
            Database.GetCollection("posts").Save(post);

            _entityStore.Remove<Post>(post.Id);

            Assert.IsNull(GetPost(post.Id));
        }
    }
}