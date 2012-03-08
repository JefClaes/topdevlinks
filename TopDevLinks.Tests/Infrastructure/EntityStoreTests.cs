using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NUnit.Framework;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Tests.Infrastructure
{
    [TestFixture]
    public class EntityStoreTests
    {
        private MongoContext _mongoContext;
        private EntityStore _entityStore;

        public EntityStoreTests()
        {
            _mongoContext = new MongoContext();
            _entityStore = new EntityStore(_mongoContext);
        }

        [SetUp]
        public void SetUp()
        {
            _mongoContext.Database.GetCollection("posts").RemoveAll();
            _mongoContext.Database.GetCollection("posts").CreateIndex(new IndexKeysBuilder().Ascending("PublishDate"),
                                                        new IndexOptionsBuilder().SetUnique(true));
        }

        private Post GetPost(ObjectId id)
        {
            return _mongoContext.Database.GetCollection<Post>("posts").Find(Query.EQ("_id", id)).FirstOrDefault();
        }

        [Test]
        public void Save_can_add_document()
        {
            var post = new Post();
            _entityStore.Save(post);
            Assert.IsNotNull(GetPost(post.Id));
        }

        [Test]
        public void Save_can_update_document()
        {
            var post = new Post();
            _entityStore.Save(post);

            post.Published = true;
            _entityStore.Save(post);

            Assert.True(GetPost(post.Id).Published);
        }

        [Test]
        public void Save_throws_exception_when_insert_fails()
        {
            var post1 = new Post();
            var post2 = new Post();

            _entityStore.Save(post1);
            // both posts' PublishDate is null, but there's a unique index on PublishDate, so the second save should fail
            Assert.Throws<MongoSafeModeException>(() => _entityStore.Save(post2));
        }

        [Test]
        public void UnsafeSave_can_add_document()
        {
            var post = new Post();
            _entityStore.UnsafeSave(post);
            Assert.IsNotNull(GetPost(post.Id));
        }

        [Test]
        public void UnsafeSave_can_update_document()
        {
            var post = new Post();
            _entityStore.UnsafeSave(post);

            post.Published = true;
            _entityStore.Save(post);

            Assert.True(GetPost(post.Id).Published);
        }

        [Test]
        public void UnsafeSave_does_not_throw_exception_when_insert_fails()
        {
            var post1 = new Post();
            var post2 = new Post();

            _entityStore.UnsafeSave(post1);
            _entityStore.UnsafeSave(post2);

            Assert.AreEqual(1, _mongoContext.Database.GetCollection("posts").FindAll().Count());
        }

        [Test]
        public void Get_can_retrieve_existing_document()
        {
            var post = new Post();
            _mongoContext.Database.GetCollection("posts").Save(post);

            Assert.IsNotNull(_entityStore.Get<Post>(post.Id));
        }

        [Test]
        public void Remove_can_remove_existing_document_by_reference()
        {
            var post = new Post();
            _mongoContext.Database.GetCollection("posts").Save(post);

            _entityStore.Remove(post);

            Assert.IsNull(GetPost(post.Id));
        }

        [Test]
        public void Remove_can_remove_existing_document_by_id()
        {
            var post = new Post();
            _mongoContext.Database.GetCollection("posts").Save(post);

            _entityStore.Remove<Post>(post.Id);

            Assert.IsNull(GetPost(post.Id));
        }

        [Test]
        public void UnsafeRemove_can_remove_existing_document_by_reference()
        {
            var post = new Post();
            _mongoContext.Database.GetCollection("posts").Save(post);

            _entityStore.UnsafeRemove(post);

            Assert.IsNull(GetPost(post.Id));
        }

        [Test]
        public void UnsafeRemove_can_remove_existing_document_by_id()
        {
            var post = new Post();
            _mongoContext.Database.GetCollection("posts").Save(post);

            _entityStore.UnsafeRemove<Post>(post.Id);

            Assert.IsNull(GetPost(post.Id));
        }

        [Test]
        public void Get_returns_all_the_documents()
        {
            _mongoContext.Database.GetCollection("posts").Save(new Post() { Published = true, PublishDate = DateTime.Now });
            _mongoContext.Database.GetCollection("posts").Save(new Post() { Published = true, PublishDate = DateTime.Now.AddDays(7) });

            var posts = _entityStore.Get<Post>();

            Assert.AreEqual(2, posts.Count());
        }
    }
}