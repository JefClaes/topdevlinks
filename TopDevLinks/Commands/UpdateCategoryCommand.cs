using MongoDB.Bson;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Commands
{
    public class UpdateCategoryCommand : Command
    {
        private string _id;
        private string _name;
        private int _priority;

        public UpdateCategoryCommand(string id, string name, int priority)
        {
            _id = id;
            _name = name;
            _priority = priority;
        }

        public override void Execute()
        {
            var category = EntityStore.Get<Category>(new ObjectId(_id));

            category.Name = _name;
            category.Priority = _priority;

            EntityStore.Save(category);
        }
    }
}