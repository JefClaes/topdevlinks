using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Commands
{
    public class AddCategoryCommand : Command
    {
        private string _name;

        public AddCategoryCommand(string name)
        {
            _name = name;
        }

        public override void Execute()
        {
            var category = new Category() { Name = _name };

            EntityStore.Save(category);
        }
    }
}