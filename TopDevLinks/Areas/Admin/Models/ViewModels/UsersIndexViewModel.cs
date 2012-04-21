using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopDevLinks.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace TopDevLinks.Areas.Admin.Models.ViewModels
{
    public class UsersIndexViewModel
    {
        public UsersIndexViewModel(UsersViewModel users)
        {
            Users = users;
        }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public UsersViewModel Users { get; set; }
    }
}