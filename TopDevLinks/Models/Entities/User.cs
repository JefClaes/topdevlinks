using DevOne.Security.Cryptography.BCrypt;
using MongoDB.Bson.Serialization.Attributes;

namespace TopDevLinks.Models.Entities
{
    public class User : Entity
    {
        public string Login { get; private set; }
        public string Email { get; private set; }

        [BsonElement("HashedPassword")]
        private string _hashedPassword;

        public User(string login, string email)
        {
            Login = login;
            Email = email;
        }

        public void SetPassword(string password)
        {
            _hashedPassword = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt(10));
        }

        public bool CheckPassword(string password)
        {
            return BCryptHelper.CheckPassword(password, _hashedPassword);
        }
    }
}