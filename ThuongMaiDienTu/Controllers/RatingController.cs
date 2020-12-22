using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class RatingController : Controller
    {
        MobilePhoneDBEntities _db = new MobilePhoneDBEntities();
        // GET: Rating
        public ActionResult Index(string search, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            if (search == null)
            {

                return View(_db.Ratings.OrderByDescending(s => s.RatingDate).ToList().ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View(_db.Ratings.Where(s => s.Product.FullName.Contains(search) || s.Product.Brand.BrandName.Contains(search) || s.Content.Contains(search) || s.Customer.FullName.Contains(search)).OrderByDescending(s => s.RatingDate).ToList().ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult Delete(int id)
        {
            if (Session["AdminID"] != null && Session["AdminRole"] != null && Session["AdminRole"].ToString() == "1")
                return View(_db.Ratings.Where(s => s.RatingID == id).FirstOrDefault());
            else
                return Content("Bạn không được quyền truy cập trang web này");
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Rating rate)
        {
            try
            {
                // TODO: Add delete logic here
                rate = _db.Ratings.Where(x => x.RatingID == id).FirstOrDefault();
                _db.Ratings.Remove(rate);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi ràng buộc khóa ngoại");
            }
        }
        public ActionResult Details(int id)
        {
            return View(_db.Ratings.Where(s => s.RatingID == id).FirstOrDefault());
        }
        public ActionResult HideOrShow(int id)
        {
            var rate = _db.Ratings.Where(s => s.RatingID == id).FirstOrDefault();
            if (rate.RatingState == true)
                rate.RatingState = false;
            else
                rate.RatingState = true;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}