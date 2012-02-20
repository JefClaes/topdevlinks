﻿using MongoDB.Bson;

namespace TopDevLinks.Models.Entities
{
    public class User
    {
        public ObjectId Id { get; private set; }
        public string Login { get; private set; }

        // TODO: add hashed password and salt

        public User(string login)
        {
            Login = login;
        }
    }
}