using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ec.Reservation.Entities;
using System.Web.Routing;

namespace ec.Reservation.Clients.Web.Helpers
    {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CheckCreatorAttribute : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var reservationId = Convert.ToInt32(filterContext.ActionParameters["id"]);//"von filterContext finden";
            var db = new EntitiesContext();
            string userName = filterContext.HttpContext.User.Identity.Name;
            var creatorId = Convert.ToInt32((from r in db.Reservations
                             where r.Id == reservationId
                             select r.CreatorId
                             ).Single());

            var creatorName = (from r in db.Reservations
                                join u in db.Users on r.CreatorId equals u.Id
                                where u.Id == creatorId
                                select new {u.UserName}).FirstOrDefault();
           

           

            if (userName.ToLower() != creatorName.UserName.ToLower())
            {
                filterContext.Controller.TempData["FlashMessage"] = "Sie haben keine Berechtigung, Sie sind nicht der Ersteller !!";

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
               
            }
            base.OnActionExecuting(filterContext);
        }
    }
}


