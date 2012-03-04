using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Queries
{
    public class GetAllUsersQuery : Query<IEnumerable<User>>
    {
        public override IEnumerable<User> Execute()
        {
            return MongoContext.GetCollection<User>().FindAll();
        }
    }
}