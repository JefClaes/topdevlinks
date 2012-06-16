using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Commands.Categories
{
    public class AddCategoryCommand : Command
    {
        private string _name;
        private int _priority;

        public AddCategoryCommand(string name, int priority)
        {
            _name = name;
            _priority = priority;
        }

        public override void Execute()
        {
            var category = new Category() { Name = _name, Priority = _priority };

            EntityStore.Save(category);
        }
    }
}