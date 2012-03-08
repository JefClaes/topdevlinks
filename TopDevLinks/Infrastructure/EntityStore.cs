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
        private readonly MongoContext _mongoContext;

        public EntityStore(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public void Save<T>(T entity) where T : Entity
        {
            _mongoContext.GetCollection<T>().Save(entity, SafeMode.True);
        }

        public void UnsafeSave<T>(T entity) where T : Entity
        {
            _mongoContext.GetCollection<T>().Save(entity, SafeMode.False);
        }

        public IEnumerable<T> Get<T>() where T : Entity
        {
            return _mongoContext.GetCollection<T>().FindAll();
        }

        public T Get<T>(ObjectId id) where T : Entity
        {
            return _mongoContext.GetCollection<T>().Find(Query.EQ("_id", id)).FirstOrDefault();
        }

        public void Remove<T>(T entity) where T : Entity
        {
            Remove<T>(entity.Id);
        }

        public void Remove<T>(ObjectId id) where T : Entity
        {
            _mongoContext.GetCollection<T>().Remove(Query.EQ("_id", id), SafeMode.True);
        }

        public void UnsafeRemove<T>(T entity) where T : Entity
        {
            UnsafeRemove<T>(entity.Id);
        }

        public void UnsafeRemove<T>(ObjectId id) where T : Entity
        {
            _mongoContext.GetCollection<T>().Remove(Query.EQ("_id", id), SafeMode.False);
        }
    }
}