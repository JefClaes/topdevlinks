using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Queries
{
    public class FindUserByLoginQuery : Query<User>
    {
        private string _login;

        public FindUserByLoginQuery(string login)
        {
            _login = login;
        }

        public override User Execute()
        {
            return GetCollection<User>().Find(Query.EQ("Login", BsonValue.Create(_login))).FirstOrDefault();
        }
    }
}