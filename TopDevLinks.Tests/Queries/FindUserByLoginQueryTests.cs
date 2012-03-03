using NUnit.Framework;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using TopDevLinks.Queries;

namespace TopDevLinks.Tests.Queries
{
    [TestFixture]
    public class FindUserByLoginQueryTests : MongoContext
    {
        private EntityStore _entityStore = new EntityStore();
        private User _user;

        [SetUp]
        public void SetUp()
        {
            GetCollection<User>().Drop();
            _user = new User("davybrion", "ralinx@davybrion.com");
            _entityStore.Save(_user);
        }

        [Test]
        public void Query_returns_null_if_no_matching_documents_found()
        {
            Assert.IsNull(new FindUserByLoginQuery("jefclaes").Execute());   
        }

        [Test]
        public void Query_returns_correct_document_if_present()
        {
            Assert.IsNotNull(new FindUserByLoginQuery("davybrion").Execute());
        }
    }
}