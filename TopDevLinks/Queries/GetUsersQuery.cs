using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver.Builders;
using TopDevLinks.Areas.Admin.Models.ViewModels;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;

namespace TopDevLinks.Queries
{
    public class GetUsersQuery : Query<UsersViewModel>
    {
        public override UsersViewModel Execute()
        {
            var users = MongoContext.GetCollection<User>().FindAll();

            var model = new UsersViewModel();

            foreach (var user in users)
            {
                model.Items.Add(new UserViewModel()
                {
                    Id = user.Id.ToString(),
                    Activated = user.Activated,
                    CanBeDeactivated = user.Activated,
                    Email = user.Email,
                    Login = user.Login
                });
            }

            return model;
        }
    }
}