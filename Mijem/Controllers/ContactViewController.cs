using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Mijem.Models;

namespace Mijem.Controllers
{
    public class ContactViewController : Controller
    {
        private mijemEntities db = new mijemEntities();
        // GET: ContactView
       
        public ActionResult Index()
        {
            List<contact_type> list = db.contact_type.ToList();
            ViewBag.contactTypeList = new SelectList(list, "contact_type_id", "type_of_contact");
            return View();
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
            reserv.reservation_date = System.DateTime.Now;

            db.reservations.Add(reserv);
            db.SaveChanges();

            return View(model);
        }

      
        public JsonResult AutoComplete(string prefix)
        {

            var nam = db.sp_searchContact(prefix).ToList();

            var cntcts = (from con in nam
                          where con.name.StartsWith(prefix)
                          select new
                          {
                              cname = con.name,
                              cphone = con.phone,
                              cbirth_date = con.birth_date,
                              ccontact_type_id = con.contact_type_id,
                              ctype_of_contact = con.type_of_contact

                                 }).ToList();
               return Json(cntcts, JsonRequestBehavior.AllowGet);
            
        }
    }
}