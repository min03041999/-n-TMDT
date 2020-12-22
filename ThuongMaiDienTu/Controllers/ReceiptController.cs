using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;
using PagedList.Mvc;
using PagedList;

namespace ThuongMaiDienTu.Controllers
{
    public class ReceiptController : Controller
    {
        MobilePhoneDBEntities _db = new MobilePhoneDBEntities();
        // GET: Order
        public ActionResult Index(string search, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            if (search == null)
            {
                return View(_db.Receipts.ToList().ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View(_db.Receipts.Where(s => s.Order.FullName.Contains(search) || s.Order.Email.Contains(search) || s.Order.PhoneNumber.Contains(search) || s.Order.OrderStatu.StatusName.Contains(search)).ToList().ToPagedList(pageNumber, pageSize));
            }
        }
    }
}