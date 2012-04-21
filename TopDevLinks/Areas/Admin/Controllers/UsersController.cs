using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopDevLinks.Infrastructure;
using TopDevLinks.Queries;
using TopDevLinks.Models.Entities;
using TopDevLinks.Areas.Admin.Models.ViewModels;
using MongoDB.Bson;
using TopDevLinks.Commands;

namespace TopDevLinks.Areas.Admin.Controllers
{
    [Authorize]
    public class UsersController : MongoContextController
    {
        public ActionResult Index()
        {
            ViewData.Model = new UsersIndexViewModel(Execute(new GetUsersQuery()));

            return View();
        }

        [HttpPost]
        public ActionResult Index(UsersIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Implement
            }

            return View();
        }

        [HttpPost]
        public ActionResult Deactive(string id)
        {
            Execute(new DeactivateUserCommand(id));

            return RedirectToAction("Index");
        }
    }
}