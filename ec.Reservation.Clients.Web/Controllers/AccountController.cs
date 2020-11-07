
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ec.Reservation.Clients.Web.Models;
using ec.Reservation.Entities;
using System.Configuration;

namespace ec.Reservation.Clients.Web.Controllers

{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private bool CheckAdmin(string userName, string password)
        {
            if(userName!="admin")
            {
                return false;
            }
            if(password != ConfigurationManager.AppSettings["password"])
            {
                return false;
            }

            return true;

        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var db = new EntitiesContext();
                if (!db.Users.Any(x => x.UserName == model.UserName))
                {
                    return RedirectToAction("Create", "Users", new { username = model.UserName });
                }

                if (Membership.ValidateUser(model.UserName, model.Password) || CheckAdmin(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    var user = db.Users.Single(u => u.UserName == model.UserName);
                    HttpContext.Session["userId"] = user.Id;
                    HttpContext.Session["userType"] = user.UserType.ToString();
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\""))
                    {
                       
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect");
                }
            }
            
            return View(model);
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["userId"] = null;
            Session["userType"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
