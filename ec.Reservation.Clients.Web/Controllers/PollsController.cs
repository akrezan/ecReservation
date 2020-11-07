using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ec.Reservation.Clients.Web.Models;
using ec.Reservation.Entities;

namespace ec.Reservation.Clients.Web.Controllers
{
    [Authorize]
    public class PollsController : Controller
    {
        private EntitiesContext db = new EntitiesContext();

        // GET: Polls
        public ActionResult Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var attendeeId = (from p in db.Polls
                              join u in db.Users on p.UserId equals u.Id
                              where u.UserName == userName
                              select new { u.Id }).FirstOrDefault();

            if (attendeeId != null)
            {
                var polls = db.Polls.Include(p => p.Reservation).Include(p => p.User).Where(p => p.UserId == attendeeId.Id);
                var pollViewModelList = polls.Select(Mapper.Map<PollsViewModel>).ToList();
                var orderedpollViewModelList = pollViewModelList.OrderByDescending(p => p.CreationDateTime);
                return View(orderedpollViewModelList);
            }
            else
            {   
                List<PollsViewModel> pollViewModelList = new List<PollsViewModel>();
                return View(pollViewModelList);
            }
        }

        // GET: Polls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poll poll = db.Polls.Find(id);
            if (poll == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<PollsViewModel>(poll));
        }

        // GET: Polls/Create
        public ActionResult Create()
        {
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Polls/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Answer,Comment,UserId,ReservationId")] PollsViewModel pollViewModel)
        {
            if (ModelState.IsValid)
            {
                var poll = Mapper.Map<Poll>(pollViewModel);
                poll.CreationDateTime = DateTime.Now;
                db.Polls.Add(poll);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "Name", pollViewModel.ReservationId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", pollViewModel.UserId);
            return View(pollViewModel);
        }

        // GET: Polls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poll poll = db.Polls.Find(id);
            if (poll == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "Name", poll.ReservationId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", poll.UserId);

            //            var pollViewModel = Mapper.Map<PollsViewModel>(poll);
            return View(Mapper.Map<PollsViewModel>(poll));
        }

        // POST: Polls/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Answer,Comment,UserId,ReservationId,ModifiedDateTime,CreationDateTime")] Poll poll)
        {
            if (ModelState.IsValid)
            {
                poll.ModifiedDateTime = DateTime.Now;
                db.Entry(poll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "Name", poll.ReservationId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", poll.UserId);
            return View(Mapper.Map<PollsViewModel>(poll));
        }

        // GET: Polls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poll poll = db.Polls.Find(id);
            if (poll == null)
            {
                return HttpNotFound();
            }

            var pollViewModel = Mapper.Map<PollsViewModel>(poll);
            return View(pollViewModel);
        }

        // POST: Polls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Poll poll = db.Polls.Find(id);
            db.Polls.Remove(poll);
            db.SaveChanges();
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
