using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class ContactController : Controller
    {
        MobilePhoneDBEntities _db = new MobilePhoneDBEntities();
        // GET: Contact
        public ActionResult Index( string search)
        {
            if (search == null)
            {
                return View(_db.Contacts.OrderByDescending(s => s.ContactDate).ToList());
            }
            else
            {
                return View(_db.Contacts.Where(s => s.Customer.FullName.Contains(search) || s.Customer.Email.Contains(search) || s.Customer.PhoneNumber == search || s.Subject.Contains(search) || s.Content.Contains(search)).OrderByDescending(s => s.ContactDate).ToList());
            }
        }
        public ActionResult CountContact()
        {
            ViewData["CountContact"] = _db.Contacts.Where(s => s.Status == false).Count();
            return View();
        }
        public ActionResult Detail(int id)
        {
            return View(_db.Contacts.Where(s => s.ContactID == id).FirstOrDefault());
        }
        public ActionResult Replied(int id)
        {
            var contact = _db.Contacts.Where(s => s.ContactID == id).FirstOrDefault();
            contact.Status = true;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}