using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TopDevLinks.Infrastructure;
using System.Diagnostics;

namespace TopDevLinks
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //TODO: bootstrap.js has a dependency on jQuery 1.7.1
            //BundleTable.Bundles.Add(BuildBootstrapCssBundle());
            BundleTable.Bundles.Add(BuildBootstrapJsBundle());
            BundleTable.Bundles.RegisterTemplateBundles();            
        }

        private Bundle BuildBootstrapJsBundle()
        {
            var bootstrapBundle = new Bundle("~/Scripts/bootstrap/js", new JsMinify());
            bootstrapBundle.AddFile("~/Scripts/bootstrap.js");

            return bootstrapBundle;
        }

        private Bundle BuildBootstrapCssBundle()
        {
            var bootstrapBundle = new Bundle("~/Content/bootstrap/css", new CssMinify());
            bootstrapBundle.AddFile("~/Content/bootstrap/bootstrap.css", true);
            bootstrapBundle.AddFile("~/Content/bootstrap/bootstrap-responsive.css", true);

            return bootstrapBundle;
        }
    }
}