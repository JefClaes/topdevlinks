using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TopDevLinks.Areas.Admin.Models.ViewModels
{
    public class UsersIndexViewModel
    {
        public UsersIndexViewModel() { }

        public UsersIndexViewModel(UsersViewModel users)
        {
            Users = users;
        }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password again")]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string PasswordAgain { get; set; }

        public UsersViewModel Users { get; set; }
    }
}