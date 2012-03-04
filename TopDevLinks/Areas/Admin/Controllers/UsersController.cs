using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopDevLinks.Infrastructure;
using TopDevLinks.Queries;

namespace TopDevLinks.Areas.Admin.Controllers
{
    [Authorize]
    public class UsersController : MongoContextController
    {
        public ActionResult Index()
        {         
            return View();
        }
    }
}