﻿using System.Web.Mvc;
using System.Web.Http;

namespace TopDevLinks.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {          
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Posts", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
