using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Antlr.Runtime;
using ec.Reservation.Clients.Web.Models;
using ec.Reservation.Entities;

namespace ec.Reservation.Clients.Web.Controllers
{
    public partial class Event
    {
        public long EventID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public System.DateTime Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index(int? id)
        {
            if (id == null || id == -1)
            {
                TempData["RessourceId"] = -1;
            }
            else
            {
                using (EntitiesContext db = new EntitiesContext())
                {
                    string ressourceName = (from r in db.Resources
                        where r.Id == id
                        select r.Name).Single();

                    ViewBag.RessourceName = ressourceName;
                }
                TempData["RessourceId"] = id;
            }
            return View();
        }

      
    

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}