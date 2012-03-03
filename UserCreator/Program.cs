﻿using System;
using System.Configuration;
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

            var entityStore = new EntityStore(ConfigurationManager.AppSettings["server"],
                                              ConfigurationManager.AppSettings["database"]);

            entityStore.Save(user);
        }
    }
}
