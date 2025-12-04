using System.Web;
using Harvestly.Models;

namespace Harvestly.Helpers
{
    public static class SessionHelper
    {
        private const string UserIdKey = "UserId";
        private const string UsernameKey = "Username";
        private const string UserRoleKey = "UserRole";

        public static void SetUserSession(User user)
        {
            HttpContext.Current.Session[UserIdKey] = user.Id;
            HttpContext.Current.Session[UsernameKey] = user.Username;
            HttpContext.Current.Session[UserRoleKey] = user.Role;
        }

        public static int? GetUserId()
        {
            return HttpContext.Current.Session[UserIdKey] as int?;
        }

        public static string GetUsername()
        {
            return HttpContext.Current.Session[UsernameKey] as string;
        }

        public static UserRole? GetUserRole()
        {
            return HttpContext.Current.Session[UserRoleKey] as UserRole?;
        }

        public static bool IsAdmin()
        {
            return GetUserRole() == UserRole.Admin;
        }

        public static bool IsFarmer()
        {
            return GetUserRole() == UserRole.Farmer;
        }

        public static bool IsAuthenticated()
        {
            return GetUserId().HasValue;
        }

        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}

