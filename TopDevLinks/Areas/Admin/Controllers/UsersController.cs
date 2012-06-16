using System.Web.Mvc;
using TopDevLinks.Areas.Admin.Models.ViewModels;
using TopDevLinks.Commands.Users;
using TopDevLinks.Infrastructure;
using TopDevLinks.Infrastructure.Web;
using TopDevLinks.Queries;

namespace TopDevLinks.Areas.Admin.Controllers
{
    [Authorize]
    public class UsersController : MongoContextController
    {
        [RestoreModelStateFromTempData]
        public ActionResult Index()
        {
            ViewData.Model = new UsersIndexViewModel(Execute(new GetUsersQuery()));

            return View();
        }

        [HttpPost]
        [SetTempDataWhenModelStateInvalid]
        public ActionResult AddUser(UsersIndexViewModel inputModel)
        {
            var model = new UsersIndexViewModel();

            if (ModelState.IsValid)
            {
                Execute(new AddUserCommand(inputModel.Login, inputModel.Email, inputModel.Password));
                ModelState.Clear();
            }

            model.Users = Execute(new GetUsersQuery());
            ViewData.Model = model;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Deactivate(string id)
        {
            Execute(new DeactivateUserCommand(id));            

            return RedirectToAction("Index");
        }
    }
}