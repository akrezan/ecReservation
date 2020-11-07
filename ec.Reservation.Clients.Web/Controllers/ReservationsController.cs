using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml.Schema;
using AutoMapper;
using ec.Reservation.Clients.Web.Models;
using ec.Reservation.Entities;
using ec.Reservation.Services.Reservation;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;
using ec.Reservation.Clients.Web.Helpers;

namespace ec.Reservation.Clients.Web.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private EntitiesContext db = new EntitiesContext();
        private ReservationService _reservationService = new ReservationService();

        // GET: Reservations
       
        public ActionResult Index()
        {

            var reservations = db.Reservations.Include(r => r.Creator).Include(r => r.Resource).ToList();
            var reservationViewModelList = reservations.Select(Mapper.Map<ReservationViewModel>).ToList();
            var orderedreservationViewModelList = reservationViewModelList.OrderByDescending(r => r.CreationDateTime);
            return View(orderedreservationViewModelList);
        }

        // GET: Reservations/Details/5
      //  [CheckCreatorAttribute]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
           
            return View(Mapper.Map<ReservationViewModel>(reservation));
        }

        // GET: Reservations/Create
        public ActionResult Create(string startTime, string endTime)
        {
            if (Request.IsAuthenticated)
            {
                var identity = User.Identity.Name;
                ViewBag.LoggedInUser = db.Users.FirstOrDefault(x => x.UserName.ToString() == identity);

            }

         //   ViewBag.StartTime = startTime + " 08:00:00";
            ViewBag.CreatorId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.ResourceId = new SelectList(db.Resources, "Id", "Name");
            ViewBag.AttendeeIds = new MultiSelectList(db.Users, "Id", "FirstName");
            ViewBag.CheckListIds = new MultiSelectList(db.CheckLists, "Id", "Name");

            return View();
        }

        // POST: Reservations/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,StartTime,EndTime,ResourceId,CreatorId, ChecklistIds, AttendeeIds, PollIds")] ReservationViewModel reservationViewModel)
        {
            if (ModelState.IsValid)
            {
                var reservation = Mapper.Map<Entities.Reservation>(reservationViewModel);
                var creator = db.Users.FirstOrDefault(x =>x.Id == reservationViewModel.CreatorId);
                reservation.CreatorFullName = creator.FirstName + " " + creator.LastName;
                _reservationService.CreateReservation(reservation, reservationViewModel.AttendeeIds, reservationViewModel.ChecklistIds);

                return RedirectToAction("Index");
            }

            ViewBag.CreatorId = new SelectList(db.Users, "Id", "FirstName", reservationViewModel.CreatorId);
            ViewBag.ResourceId = new SelectList(db.Resources, "Id", "Name", reservationViewModel.ResourceId);
            ViewBag.AttendeeIds = new MultiSelectList(db.Users, "Id", "FirstName", reservationViewModel.AttendeeIds);
            ViewBag.CheckListIds = new MultiSelectList(db.CheckLists, "Id", "Name", reservationViewModel.ChecklistIds);
            return View(reservationViewModel);
        }

        // GET: Reservations/Edit/5
        [CheckCreatorAttribute]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatorId = new SelectList(db.Users, "Id", "FirstName", reservation.CreatorId);
            ViewBag.ResourceId = new SelectList(db.Resources, "Id", "Name", reservation.ResourceId);
            ViewBag.AttendeeIds = new MultiSelectList(db.Users, "Id", "FirstName",reservation.Attendees.Select(a => a.Id));
            ViewBag.CheckListIds = new MultiSelectList(db.CheckLists, "Id", "Name", reservation.CheckLists.Select(a => a.Id));
           
            return View(reservation);
        }

    

        // POST: Reservations/Edit/5re
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,StartTime,EndTime,ResourceId,CreatorId,CreationDateTime,ModifiedDateTime,AttendeeIds,ChecklistIds")] Entities.Reservation reservation)
        {
            if (ModelState.IsValid)
            {
         
                _reservationService.UpdateReservation(reservation);

                return RedirectToAction("Index");
            }
            ViewBag.CreatorId = new SelectList(db.Users, "Id", "FirstName", reservation.CreatorId);
            ViewBag.ResourceId = new SelectList(db.Resources, "Id", "Name", reservation.ResourceId);
            ViewBag.AttendeeIds = new MultiSelectList(db.Users, "Id", "FirstName", reservation.AttendeeIds);
            ViewBag.CheckListIds = new MultiSelectList(db.CheckLists, "Id", "Name", reservation.ChecklistIds);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [CheckCreatorAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entities.Reservation reservation = db.Reservations.Find(id);
            reservation.DeletePolls();
            reservation.ClearAttendees();
            db.Reservations.Remove(reservation);
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
