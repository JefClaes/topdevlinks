using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopDevLinks.Areas.Admin.Models.ViewModels
{
    public class UsersIndexViewModel
    {        
        public IEnumerable<UserIndexViewModel> Users { get; set; }        
    }

    public class UserIndexViewModel
    {
        public UserIndexViewModel(string username, string emailAddress)
        {
            Username = username;
            EmailAddress = emailAddress;
        }

        public string Username { get; set; }

        public string EmailAddress { get; set; }
    }
}