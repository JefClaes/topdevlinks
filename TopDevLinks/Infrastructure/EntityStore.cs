using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Infrastructure
{
    public class EntityStore
    {
        // TODO: move this to a seperate class because we'll also need this for our Query objects
        private static Dictionary<Type, string> _collectionMap = new Dictionary<Type, string>
        {
            // add a Type with its collection name here for every Document stored in a collection
            { typeof(Post), "posts" },
            { typeof(User), "users" },
            { typeof(Category), "categories" }
        };

        private MongoDatabase _database;

        // TODO: get these values from web.config
        public EntityStore() : this("mongodb://localhost", "topdevlinks")
        {
        }

        public EntityStore(string server, string databaseName)
        {
            _database = MongoServer.Create(server).GetDatabase(databaseName);
        }

        private MongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(_collectionMap[typeof(T)]);
        }

        public void Save<T>(T entity) where T : Entity
        {
            GetCollection<T>().Save(entity, SafeMode.True);
        }

        public void UnsafeSave<T>(T entity) where T : Entity
        {
            GetCollection<T>().Save(entity, SafeMode.False);
        }

        public T Get<T>(ObjectId id) where T : Entity
        {
            return GetCollection<T>().Find(Query.EQ("_id", id)).FirstOrDefault();
        }

        public void Remove<T>(T entity) where T : Entity
        {
            Remove<T>(entity.Id);
        }

        public void Remove<T>(ObjectId id) where T : Entity
        {
            GetCollection<T>().Remove(Query.EQ("_id", id), SafeMode.True);
        }

        public void UnsafeRemove<T>(T entity) where T : Entity
        {
            UnsafeRemove<T>(entity.Id);
        }

        public void UnsafeRemove<T>(ObjectId id) where T : Entity
        {
            GetCollection<T>().Remove(Query.EQ("_id", id), SafeMode.False);
        }
    }
}