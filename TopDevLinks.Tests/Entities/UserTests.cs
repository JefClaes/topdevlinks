using NUnit.Framework;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Tests.Entities
{
    [TestFixture]
    public class UserTests
    {
        private User _user;

        [SetUp]
        public void SetUp()
        {
            _user = new User("davybrion", "ralinx@davybrion.com");
            _user.SetPassword("blabla");
        }

        [Test]
        public void CheckPassword_returns_true_for_valid_password()
        {
            Assert.IsTrue(_user.CheckPassword("blabla"));
        }

        [Test]
        public void CheckPassword_returns_false_for_invalid_password()
        {
            Assert.IsFalse(_user.CheckPassword("bla"));
        }
    }
}