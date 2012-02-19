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
                    var server = MongoServer.Create("mongodb://localhost");
                    _database = server.GetDatabase("topdevlinks_test");
                }

                return _database;
            }
        }
    }
}