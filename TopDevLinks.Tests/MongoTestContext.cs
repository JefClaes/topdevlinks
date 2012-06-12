using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Tests
{
    public class MongoTestContext : MongoContext
    {
        public MongoTestContext() : base(
            new Dictionary<Type, string>
            {            
                { typeof(Post), "test_posts" },
                { typeof(User), "test_users" },
                { typeof(Category), "test_categories" }
            })
        {
        }
    }
}
