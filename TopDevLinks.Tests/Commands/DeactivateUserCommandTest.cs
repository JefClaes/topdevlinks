using MongoDB.Bson;
using NUnit.Framework;
using TopDevLinks.Commands;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Tests.Commands
{
    [TestFixture]
    public class DeactivateUserCommandTest : CommandTestFixture
    {
        private ObjectId _userId;

        [SetUp]
        public void SetUp()
        {
            MongoContext.GetCollection<User>().Drop();

            var user = new User("JefClaes", "jef.claes@gmail.com");
            user.SetPassword("abc.123");

            EntityStore.Save(user);

            _userId = user.Id;
        }

        [Test]
        public void Command_deactivates_user()
        {
            Execute(new DeactivateUserCommand(_userId.ToString()));

            var user = EntityStore.Get<User>(_userId);

            Assert.IsFalse(user.Activated);
        }
    }
}
