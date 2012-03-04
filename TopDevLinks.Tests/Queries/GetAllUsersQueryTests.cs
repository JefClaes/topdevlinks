using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TopDevLinks.Models.Entities;
using TopDevLinks.Queries;

namespace TopDevLinks.Tests.Queries
{
    public class GetAllUsersQueryTests : QueryTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            MongoContext.GetCollection<User>().Drop();

            EntityStore.Save(new User("JefClaes", "jef.claes@gmail.com"));
            EntityStore.Save(new User("DavyBrion", "ralinckx@davybrion.com"));
            EntityStore.Save(new User("AnotherUser", "anotheruser@topdevlinks.com"));            
        }

        [Test]
        public void Query_returns_three_documents()
        {
            Assert.IsTrue(Execute(new GetAllUsersQuery()).Count() == 3);
        }
    }
}
