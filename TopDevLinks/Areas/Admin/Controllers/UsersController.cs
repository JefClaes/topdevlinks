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
        public ActionResult Index(UsersIndexViewModel inputModel)
        {
            var model = new UsersIndexViewModel();

            if (ModelState.IsValid)
            {
                Execute(new AddUserCommand(inputModel.Login, inputModel.Email, inputModel.Password));
                ModelState.Clear();
            }

            model.Users = Execute(new GetUsersQuery());
            ViewData.Model = model;

            return View();
        }

        [HttpPost]
        public ActionResult Deactivate(string id)
        {
            Execute(new DeactivateUserCommand(id));            

            return RedirectToAction("Index");
        }
    }
}