using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using MongoDB.Bson;

namespace TopDevLinks.Commands.Users
{
    public class DeactivateUserCommand : Command
    {
        private string _id;

        public DeactivateUserCommand(string id)
        {
            _id = id;
        }

        public override void Execute()
        {
            var user = EntityStore.Get<User>(new ObjectId(_id));

            user.Activated = false;

            EntityStore.Save(user);
        }
    }
}