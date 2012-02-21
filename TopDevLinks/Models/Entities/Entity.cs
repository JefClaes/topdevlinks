using MongoDB.Bson;

namespace TopDevLinks.Models.Entities
{
    public abstract class Entity
    {
        public ObjectId Id { get; protected set; }
    }
}