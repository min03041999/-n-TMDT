using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class ReportController : Controller
    {
        MobilePhoneDBEntities _db = new MobilePhoneDBEntities();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TotalOrder()
        {
            ViewData["totalOrder"] = _db.Receipts.Count();
            return View();
        }
        public ActionResult TotalProductSold()
        {
            ViewData["TotalProductSold"] = _db.Receipts.Sum(s => s.Order.OrderDetails.Sum(m=>m.Quantity));
            return View();
        }
        public ActionResult DoanhThu()
        {
            ViewData["DoanhThu"] = _db.Receipts.Sum(s => s.TotalPrice).Value.ToString("N0");
            return View();
        }
        public ActionResult TopProduct()
        {
            return View(_db.BestSellers.OrderByDescending(m => m.DaBan).ToList());
        }
        public ActionResult BestSeller()
        {
            return PartialView(_db.BestSellers.OrderByDescending(m => m.DaBan).Take(1).ToList());
        }
        public ActionResult HighestRevenue()
        {
            return PartialView(_db.BestSellers.OrderByDescending(m => m.TongTien).Take(1).ToList());
        }
        public ActionResult KH_Chart()
        {
            var query = _db.OrderDetails.Include("User").Where(s =>( s.Order.StatusID == 4 || s.Order.CheckOut==true) && s.Order.UserID!=null)
                .GroupBy(m => m.Order.Customer.FullName)
                .Select(s => new { name = s.Key, count = s.Sum(w => w.Quantity) }).ToList().Take(5);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DoanhThuTheoThang()
        {
            var query = _db.Receipts.Include("Order")
               .GroupBy(m => m.Order.OrderDate.Value.Month)
               .Select(s => new { name = s.Key, count = s.Sum(m => m.TotalPrice) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DonHangTheoThang()
        {
            var query = _db.Orders.Include("Order").Where(s => s.StatusID == 4 || s.CheckOut==true)
               .GroupBy(m => m.OrderDate.Value.Month)
               .Select(s => new { name = s.Key, donhang = s.Count(), sanpham = s.Sum(w => w.OrderDetails.Sum(m => m.Quantity)) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetData()
        {
            int iphone = _db.OrderDetails.Where(x => x.Product.BrandID == 1 && x.Order.StatusID==4).Count();
            int samsung = _db.OrderDetails.Where(x => x.Product.BrandID == 2 && x.Order.StatusID == 4).Count();
            int oppo = _db.OrderDetails.Where(x => x.Product.BrandID == 3 && x.Order.StatusID == 4).Count();
            int vivo = _db.OrderDetails.Where(x => x.Product.BrandID == 4 && x.Order.StatusID == 4).Count();
            int xiaomi = _db.OrderDetails.Where(x => x.Product.BrandID == 5 && x.Order.StatusID == 4).Count();
            int realme = _db.OrderDetails.Where(x => x.Product.BrandID == 6 && x.Order.StatusID == 4).Count();

            Ratio obj = new Ratio();
            obj.iPhone = iphone;
            obj.SAMSUNG = samsung;
            obj.OPPO = oppo;
            obj.VIVO = vivo;
            obj.XIAOMI = xiaomi;
            obj.REALME = realme;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Ratio
        {
            public int iPhone { get; set; }
            public int SAMSUNG { get; set; }
            public int OPPO { get; set; }
            public int VIVO { get; set; }
            public int XIAOMI { get; set; }
            public int REALME { get; set; }
        }
        public ActionResult GetDataPayment()
        {
            int shipCOD = _db.Orders.Where(x => x.Payment == false && x.CheckOut== true).Count();
            int paypal = _db.Orders.Where(x => x.Payment == true && x.CheckOut == true).Count();

            RatioPayment obj = new RatioPayment();
            obj.COD = shipCOD;
            obj.PAYPAL = paypal;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class RatioPayment
        {
            public int COD { get; set; }
            public int PAYPAL { get; set; }
        }
    }
}