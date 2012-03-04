using System;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace UserCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("You need to provide 3 arguments: the login, the emailaddress and the password");
                return;
            }

            var user = new User(args[0], args[1]);
            user.SetPassword(args[2]);

            var entityStore = new EntityStore(new MongoContext());

            entityStore.Save(user);
        }
    }
}
