using System.Web;
using System.Web.Mvc;
using Harvestly.Helpers;

namespace Harvestly.Filters
{
    public class AuthorizeAdminOrFarmerAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return SessionHelper.IsAdmin() || SessionHelper.IsFarmer();
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Check if the request is for AdminIndex (farmer-related page)
            string loginType = filterContext.HttpContext.Request.RawUrl.Contains("AdminIndex") ? "farmer" : null;
            string returnUrl = HttpUtility.UrlEncode(filterContext.HttpContext.Request.RawUrl);
            string loginUrl = "~/Account/Login?returnUrl=" + returnUrl;
            if (!string.IsNullOrEmpty(loginType))
            {
                loginUrl += "&loginType=" + loginType;
            }
            filterContext.Result = new RedirectResult(loginUrl);
        }
    }
}

