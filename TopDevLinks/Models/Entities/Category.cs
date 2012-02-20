using MongoDB.Bson;

namespace TopDevLinks.Models.Entities
{
    public class Category
    {
        public ObjectId Id { get; private set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }
}