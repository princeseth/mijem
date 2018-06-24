using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using Mijem.Models;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mijem;
using System.Web.Script.Serialization;

namespace Mijem.Controllers
{
    public class ContactsController : Controller
    {
        private mijemEntities db = new mijemEntities();

        // GET: Contacts
        public ActionResult Index()
        {
             var contacts = db.contacts.Include(c => c.contact_type);

            List<contact_type> list = db.contact_type.ToList();
            ViewBag.contactTypeList = new SelectList(list, "contact_type_id", "type_of_contact");
            return View(contacts.ToList());
        }
        [HttpPost]
        public ActionResult Index(ContactViewModel model)
        {
            List<contact_type> list = db.contact_type.ToList();
            ViewBag.contactTypeList = new SelectList(list, "contact_type_id", "type_of_contact");

            contact contact = new contact();
            contact.name = model.name;
            contact.phone = model.phone;
            contact.birth_date = model.birth_date;
            contact.contact_type_id = model.contact_type_id;

            db.contacts.Add(contact);
            db.SaveChanges();

            int contactId = contact.contact_id;

            reservation reserv = new reservation();
            reserv.description = model.description;
            reserv.contact_id = contactId;
            reserv.reservation_date = new DateTime();
            db.reservations.Add(reserv);
            db.SaveChanges();

            return View(model);
        }

        
        public JsonResult ContactList()
        {
        
                List<contact> list = db.contacts.ToList();

            var data = (from p in db.contacts
                        join b in db.contact_type on p.contact_type_id equals b.contact_type_id
                        select new 
                        {
                            contactId = p.contact_id,
                            contactName = p.name,
                            contactPhone = p.phone,
                            contactBirthdate = p.birth_date,
                            contactType = b.type_of_contact
                        }).ToList();
            
                return Json(data, JsonRequestBehavior.AllowGet);
            
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact contact = db.contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            ViewBag.contact_type_id = new SelectList(db.contact_type, "contact_type_id", "type_of_contact");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "contact_id,name,phone,birth_date,contact_type_id")] contact contact)
        {
            if (ModelState.IsValid)
            {
                db.contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.contact_type_id = new SelectList(db.contact_type, "contact_type_id", "type_of_contact", contact.contact_type_id);
            return View(contact);
        }

     
     



        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact contact = db.contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.contact_type_id = new SelectList(db.contact_type, "contact_type_id", "type_of_contact", contact.contact_type_id);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "contact_id,name,phone,birth_date,contact_type_id")] contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.contact_type_id = new SelectList(db.contact_type, "contact_type_id", "type_of_contact", contact.contact_type_id);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact contact = db.contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contact contact = db.contacts.Find(id);
            db.contacts.Remove(contact);
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
