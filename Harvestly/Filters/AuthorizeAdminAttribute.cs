using System.Web;
using System.Web.Mvc;
using Harvestly.Helpers;

namespace Harvestly.Filters
{
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return SessionHelper.IsAdmin();
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Account/Login?returnUrl=" + HttpUtility.UrlEncode(filterContext.HttpContext.Request.RawUrl));
        }
    }
}

