using System.Configuration;
using MongoDB.Driver;

namespace TopDevLinks.Tests
{
    public abstract class MongoDbFixture
    {
        private MongoDatabase _database;

        public MongoDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    var server = MongoServer.Create(ConfigurationManager.AppSettings["server"]);
                    _database = server.GetDatabase(ConfigurationManager.AppSettings["database"]);
                }

                return _database;
            }
        }
    }
}