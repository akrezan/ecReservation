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
    public class CheckListsController : Controller
    {
        private EntitiesContext db = new EntitiesContext();

        // GET: CheckLists
        public ActionResult Index()
        {
            var checkListViewModel = db.CheckLists.Select(Mapper.Map<CheckListsViewModel>).ToList();
            var orderedcheckListViewModel = checkListViewModel.OrderBy(c => c.Name);
            return View(orderedcheckListViewModel);
        }

        // GET: CheckLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckList checkList = db.CheckLists.Find(id);
            if (checkList == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<CheckListsViewModel>(checkList));
        }

        // GET: CheckLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckLists/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Details")] CheckList checkList)
        {
            if (ModelState.IsValid)
            {
                checkList.CreationDateTime = DateTime.Now;
                db.CheckLists.Add(checkList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Mapper.Map<CheckListsViewModel>(checkList));
        }

        // GET: CheckLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckList checkList = db.CheckLists.Find(id);
            if (checkList == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<CheckListsViewModel>(checkList));
        }

        // POST: CheckLists/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Details,CreationDateTime")] CheckList checkList)
        {
            if (ModelState.IsValid)
            {
                checkList.ModifiedDateTime = DateTime.Now;
                db.Entry(checkList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Mapper.Map<CheckListsViewModel>(checkList));
        }

        // GET: CheckLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckList checkList = db.CheckLists.Find(id);
            if (checkList == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<CheckListsViewModel>(checkList));
        }

        // POST: CheckLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckList checkList = db.CheckLists.Find(id);
            db.CheckLists.Remove(checkList);
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
