using System.Web.Mvc;

namespace TopDevLinks.Infrastructure.Web
{
    public class SetTempDataWhenModelStateInvalidAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                filterContext.Controller.TempData["ModelState"] = filterContext.Controller.ViewData.ModelState;
            }
        }
    }
}