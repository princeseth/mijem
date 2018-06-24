using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mijem;

namespace Mijem.Controllers
{
    public class ReservationsController : Controller
    {
        private mijemEntities db = new mijemEntities();

        // GET: Reservations

        public ActionResult Index(string sortOrder)
        {
            
            var res = from s in db.reservations.Include(r => r.contact)
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    res = res.OrderByDescending(s => s.contact.name);
                    break;        
                case "date_asc":
                    res = res.OrderBy(s => s.reservation_date);
                    break;
                case "date_desc":
                    res = res.OrderByDescending(s => s.reservation_date);
                    break;
                case "name_asc":
                    res = res.OrderBy(s => s.contact.name);
                    break;
                default:
                    res = res.OrderBy(s => s.contact.name);
                    break;
            }
            return View(res.ToList());


            //var reservations = db.reservations.Include(r => r.contact);
            //return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservation reservation = db.reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.contact_id = new SelectList(db.contacts, "contact_id", "name");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reservation_id,reservation_date,contact_id")] reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.contact_id = new SelectList(db.contacts, "contact_id", "name", reservation.contact_id);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservation reservation = db.reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.contact_id = new SelectList(db.contacts, "contact_id", "name", reservation.contact_id);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "reservation_id,reservation_date,contact_id")] reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.contact_id = new SelectList(db.contacts, "contact_id", "name", reservation.contact_id);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservation reservation = db.reservations.Find(id);
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
            reservation reservation = db.reservations.Find(id);
            db.reservations.Remove(reservation);
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
