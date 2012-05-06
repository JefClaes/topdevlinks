using System;
using System.Collections.Generic;
using System.Configuration;
using MongoDB.Driver;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Infrastructure
{
    public class MongoContext
    {
        private static readonly Dictionary<Type, string> _collectionMap = new Dictionary<Type, string>
        {
            // add a Type with its collection name here for every Document stored in a collection
            { typeof(Post), "posts" },
            { typeof(User), "users" },
            { typeof(Category), "categories" }
        };

        private string _connectionstring;
        private MongoDatabase _database;

        public MongoDatabase Database
        {
            get {
                if (_database == null)
                {
                    var con = new MongoConnectionStringBuilder(_connectionstring);

                    _database = MongoServer.Create(con).GetDatabase(con.DatabaseName);
                }
                return _database;              
            }
        }

        public MongoContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public MongoContext()
            : this(ConfigurationManager.ConnectionStrings["topdevlinks"].ConnectionString)
        {
        }

        public MongoCollection<T> GetCollection<T>()
        {
            return Database.GetCollection<T>(_collectionMap[typeof(T)]);
        }
    }
}