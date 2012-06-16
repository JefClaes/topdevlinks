using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Commands.Users
{
    public class AddUserCommand : Command
    {
        private string _login;
        private string _email;
        private string _password;

        public AddUserCommand(string login, string email, string password)
        {
            _login = login;
            _email = email;
            _password = password;
        }

        public override void Execute()
        {
            var user = new User(_login, _email);
            user.SetPassword(_password);

            EntityStore.Save(user);
        }
    }
}