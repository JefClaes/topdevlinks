using NUnit.Framework;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using TopDevLinks.Queries;

namespace TopDevLinks.Tests.Queries
{
    [TestFixture]
    public class FindUserByLoginQueryTests : QueryTestFixture
    {
        private User _user;

        [SetUp]
        public void SetUp()
        {
            MongoContext.GetCollection<User>().Drop();
            _user = new User("davybrion", "ralinx@davybrion.com");
            EntityStore.Save(_user);
        }

        [Test]
        public void Query_returns_null_if_no_matching_documents_found()
        {
            Assert.IsNull(Execute(new FindUserByLoginQuery("jefclaes")));   
        }

        [Test]
        public void Query_returns_correct_document_if_present()
        {
            Assert.IsNotNull(Execute(new FindUserByLoginQuery("davybrion")));
        }
    }
}