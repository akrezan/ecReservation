using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ec.Reservation.Entities;

namespace ec.Reservation.Clients.Web.Helpers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params object[] roles)
        {
            if (roles.Any(r => r.GetType() != typeof(UserType)))
                throw new ArgumentException("roles");

            Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var db = new EntitiesContext();

            var currentUser = db.Users.FirstOrDefault(u => u.UserName == httpContext.User.Identity.Name);

            if (currentUser != null && currentUser.UserType.ToString() == Roles)
            {
                    
                    return httpContext.User.Identity.IsAuthenticated;
            }

            return false;
        }
    }
}
