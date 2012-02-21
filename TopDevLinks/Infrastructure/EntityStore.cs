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
        // TODO: add Unsafe methods (AddUnsafe, UpdateUnsafe, RemoveUnsafe)
        // TODO: inspect and deal with SafeModeResult from each operation
        
        private static Dictionary<Type, string> _collectionMap = new Dictionary<Type, string>
        {
            // add a Type with its collection name here for every Document stored in a collection
            { typeof(Post), "posts" }
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
    }
}