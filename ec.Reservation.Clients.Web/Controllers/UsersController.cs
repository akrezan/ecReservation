using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using ec.Reservation.Clients.Web.Helpers;
using ec.Reservation.Clients.Web.Models;
using ec.Reservation.Entities;
using ec.Reservation.Services.User;

namespace ec.Reservation.Clients.Web.Controllers
{
    [CustomAuthorize(UserType.Administrator)]
    public class UsersController : Controller
    {
        private EntitiesContext db = new EntitiesContext();
        private UserService _userService = new UserService();

        // GET: Users
        public ActionResult Index()
        {
            var userViewModelList = db.Users.Select(Mapper.Map<UserViewModel>).ToList();
            var orderedUserViewModelList= userViewModelList.OrderBy(u => u.FirstName).ThenBy(u => u.LastName);
            return View(orderedUserViewModelList);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userViewModel = Mapper.Map<UserViewModel>(user);
            
            return View(userViewModel);
        }

        // GET: Users/Create
        [AllowAnonymous]
        public ActionResult Create(string username)
        {
            ViewBag.UserName = username;

            return View();
        }

        // POST: Users/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,UserName,Password,Email,EmailConfirm,UserType")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
              
                if (Membership.ValidateUser(userViewModel.UserName, userViewModel.Password))
                {
                    var user = Mapper.Map<User>(userViewModel);
                    user.CreationDateTime = DateTime.Now;
                    user.UserType = UserType.NormalUser;
                    var emailService = new Services.Helper.EmailService();
                    var userName = user.UserName;
                    emailService.SendNewUserNotificationEmail(userName, ConfigurationManager.AppSettings["host"]);
                    db.Users.Add(user);
                    db.SaveChanges();

                    return RedirectToAction("index","home");
                    
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect");
                }
               
             
            }
            
            return View(userViewModel);
        }

        // GET: Users/Edit/5
        [AllowAnonymous]
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userViewModel = Mapper.Map<UserViewModel>(user);
            string logendUserName = User.Identity.Name;
            var logendUserType = (from u in db.Users
                    where u.UserName == logendUserName
                    select u.UserType
                ).Single();
            ViewBag.LogendUserType = logendUserType.ToString();
            ViewBag.UserType = user.UserType;
            ViewBag.UserName = logendUserName.ToString();
            return View(userViewModel);
        }

        // POST: Users/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,UserName,Password,Email,UserType,CreationDateTime")] User user)
        {
            if (ModelState.IsValid)
            {
                string logendUserName = User.Identity.Name;
                var logendUserType = (from u in db.Users
                        where u.UserName == logendUserName
                        select u.UserType
                    ).Single();

                user.ModifiedDateTime = DateTime.Now;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                if (logendUserType == UserType.NormalUser)
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index");
            }
            return View(Mapper.Map<UserViewModel>(user));
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userViewModel = Mapper.Map<UserViewModel>(user);
           
            return View(userViewModel);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
          
            _userService.Remove(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
