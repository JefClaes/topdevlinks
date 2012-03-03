using System;
using System.Collections.Generic;
using System.Configuration;
using MongoDB.Driver;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Infrastructure
{
    public abstract class MongoContext
    {
        private static readonly Dictionary<Type, string> _collectionMap = new Dictionary<Type, string>
        {
            // add a Type with its collection name here for every Document stored in a collection
            { typeof(Post), "posts" },
            { typeof(User), "users" },
            { typeof(Category), "categories" }
        };

        protected MongoDatabase Database { get; private set; }

        protected MongoContext(string server, string databaseName)
        {
            Database = MongoServer.Create(server).GetDatabase(databaseName);
        }

        protected MongoContext() : this(ConfigurationManager.AppSettings["server"], ConfigurationManager.AppSettings["database"])
        {
        }

        protected MongoCollection<T> GetCollection<T>()
        {
            return Database.GetCollection<T>(_collectionMap[typeof(T)]);
        }
    }
}