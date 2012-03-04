using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopDevLinks.Infrastructure;
using TopDevLinks.Areas.Admin.Models.ViewModels;
using TopDevLinks.Queries;

namespace TopDevLinks.Areas.Admin.Controllers
{
    [Authorize]
    public class UsersController : MongoContextController
    {
        public ActionResult Index()
        {          
            ViewData.Model = new UsersIndexViewModel()
            {
                Users = Execute(new GetAllUsersQuery())
                    .Select(u => new UserIndexViewModel(u.Login, u.Email))
            };

            return View();
        }
    }
}