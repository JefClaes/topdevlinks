using NUnit.Framework;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Tests.Infrastructure
{
    [TestFixture]
    public class MongoMembershipProviderTests : MongoContext
    {
        private MongoMembershipProvider _provider = new MongoMembershipProvider();

        [SetUp]
        public void SetUp()
        {
            GetCollection<User>().Drop();
            var entityStore = new EntityStore();
            var user = new User("davybrion", "ralinx@davybrion.com");
            user.SetPassword("blabla");
            entityStore.Save(user);
        }

        [Test]
        public void ValidateUser_returns_true_if_user_exists_and_password_is_correct()
        {
            Assert.IsTrue(_provider.ValidateUser("davybrion", "blabla"));
        }

        [Test]
        public void ValidateUser_returns_false_if_user_exists_but_password_is_invalid()
        {
            Assert.IsFalse(_provider.ValidateUser("davybrion", "blablabla"));
        }

        [Test]
        public void ValidateUser_returns_false_if_user_does_not_exist()
        {
            Assert.IsFalse(_provider.ValidateUser("jefclaes", "blabla"));
        }
    }
}