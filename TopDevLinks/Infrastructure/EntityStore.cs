using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Infrastructure
{
    public class EntityStore : MongoContext
    {
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