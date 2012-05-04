using System.Web.Mvc;
using System.Web.Security;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.ViewModels;
using TopDevLinks.Queries;

namespace TopDevLinks.Controllers
{
    public class AccountController : MongoContextController
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = Execute(new FindUserByLoginQuery(model.UserName));
                if (user != null && user.CheckPassword(model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && 
                        returnUrl.Length > 1 && 
                        returnUrl.StartsWith("/") && 
                        !returnUrl.StartsWith("//") && 
                        !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
