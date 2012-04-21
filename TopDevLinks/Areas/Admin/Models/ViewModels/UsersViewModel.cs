using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopDevLinks.Areas.Admin.Models.ViewModels
{
    public class UsersViewModel
    {
        public UsersViewModel()
        {
            Items = new List<UserViewModel>();
        }

        public IList<UserViewModel> Items { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public bool Activated { get; set; }

        public bool CanBeDeactivated { get; set; }
    }
}