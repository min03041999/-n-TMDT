using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class OrderController : Controller
    {
        MobilePhoneDBEntities _db = new MobilePhoneDBEntities();
        // GET: Order
        public ActionResult Index()
        {
            return View(_db.Orders.ToList());
        }
        public ActionResult SearchOrder(string search)
        {
            if (search == null)
            {
                return View(_db.Orders.ToList());
            }
            else
            {
                return View(_db.Orders.Where(s => s.FullName.Contains(search) || s.Email.Contains(search) || s.PhoneNumber.Contains(search) || s.OrderStatu.StatusName.Contains(search)).ToList());
            }
        }
        public ActionResult OrderDetail(int id)
        {
            var order = _db.OrderDetails.Where(s => s.OrderID == id).FirstOrDefault();
            var result = from r in _db.OrderDetails
                         where r.OrderID == order.OrderID
                         select r;
            return View(result.ToList());
        }
        public ActionResult ChoXacNhan()
        {
            return PartialView(_db.Orders.Where(s => s.OrderStatu.StatusID == 1).ToList());
        }
        public ActionResult ChoLayHang()
        {
            return PartialView(_db.Orders.Where(s => s.OrderStatu.StatusID == 2).ToList());
        }
        public ActionResult DangGiaoHang()
        {
            return PartialView(_db.Orders.Where(s => s.OrderStatu.StatusID == 3).ToList());
        }
        public ActionResult DaGiaoHang()
        {
            return PartialView(_db.Orders.Where(s => s.OrderStatu.StatusID == 4).ToList());
        }
        public ActionResult DaHuy()
        {
            return PartialView(_db.Orders.Where(s => s.OrderStatu.StatusID == 5).ToList());
        }
        public ActionResult Confirm(int id)
        {
            var order = _db.Orders.Where(s => s.OrderID == id).FirstOrDefault();
            if (order.StatusID == 1)
            {
                order.StatusID = 2;
            }
            _db.SaveChanges();


            //string customerContent = "<h3>Đơn hàng <b style='color: red; font-weight: bold'>" +order.OrderID+"</b> của bạn đã được xác nhận và đang chờ lấy hàng.</h3>" +
            //    "Mã đơn hàng: " + order.OrderID + "<br>" +
            //    "Ngày đặt: " + order.OrderDate + "<br>" +
            //    "Trạng thái: " + order.OrderStatu.StatusName + "<br>" +
            //    "Khách hàng: " + order.FullName + "<br>" +
            //    "Số điện thoại: " + order.PhoneNumber + "<br>" +
            //    "Địa chỉ giao hàng: " + order.ShippingAddress + "<br>" +
            //    "Tổng tiền: " + order.TotalPrice.Value.ToString("N0") + " VNĐ" + "<br>" + "<hr>" +
            //    "Click vào <a href='https://localhost:44343/ShoppingCart/TrackingOrder'>đây</a> để theo dõi đơn hàng của bạn.<br>" +
            //    "<b>3Rings Xin cảm ơn!</b>";

            //var toCustomerEmail = order.Email.ToString();
            //new Mail().SendMail(toCustomerEmail, "Đơn hàng của bạn đã được xác nhận", customerContent);

            return RedirectToAction("Index");
        }
        public ActionResult LayHang(int id)
        {
            var order = _db.Orders.Where(s => s.OrderID == id).FirstOrDefault();
            if (order.StatusID == 2)
            {
                order.StatusID = 3;
            }
            var orderDetail = _db.OrderDetails.Where(s => s.OrderID == id).ToList();
            foreach (var pro in orderDetail)
            {
                var product = _db.Products.Where(s => s.ProductID == pro.ProductID).FirstOrDefault();
                if (product.Quantity - pro.Quantity < 0)
                {
                    return Content("Trong kho không đủ sản phẩm");
                }
                else
                {
                    product.Quantity -= pro.Quantity;
                }
            }
            _db.SaveChanges();

            //string customerContent = "<h3>Đơn hàng <b style='color: red; font-weight: bold'>" + order.OrderID + "</b> của bạn đã được lấy hàng và đang được giao đến bạn.</h3>" +
            //    "Mã đơn hàng: " + order.OrderID + "<br>" +
            //    "Ngày đặt: " + order.OrderDate + "<br>" +
            //    "Trạng thái: " + order.OrderStatu.StatusName + "<br>" +
            //    "Khách hàng: " + order.FullName + "<br>" +
            //    "Số điện thoại: " + order.PhoneNumber + "<br>" +
            //    "Địa chỉ giao hàng: " + order.ShippingAddress + "<br>" +
            //    "Tổng tiền: " + order.TotalPrice.Value.ToString("N0") + " VNĐ" + "<br>" + "<hr>" +
            //    "Click vào <a href='https://localhost:44343/ShoppingCart/TrackingOrder'>đây</a> để theo dõi đơn hàng của bạn.<br>" +
            //    "<b>3Rings Xin cảm ơn!</b>";

            //var toCustomerEmail = order.Email.ToString();
            //new Mail().SendMail(toCustomerEmail, "Đơn hàng của bạn đang được giao", customerContent);


            return RedirectToAction("Index");
        }
        public void AddPoint(int id, double point)
        {
            var user = _db.Customers.Where(s => s.UserID == id).FirstOrDefault();
            if (user != null)
            {
                var query = "UPDATE Customer SET Point = @diem WHERE UserID = @ma";
                _db.Database.ExecuteSqlCommand(query, new SqlParameter("@diem", point + user.Point), new SqlParameter("@ma", user.UserID));
                _db.SaveChanges();
                if (user.Point + point >= 50000)
                {
                    var query2 = "UPDATE Customer SET RankID = 2 WHERE UserID = @ma";
                    _db.Database.ExecuteSqlCommand(query2, new SqlParameter("@ma", user.UserID));
                }
                if (user.Point + point >= 100000)
                {
                    var query3 = "UPDATE Customer SET RankID = 3 WHERE UserID = @ma";
                    _db.Database.ExecuteSqlCommand(query3, new SqlParameter("@ma", user.UserID));
                }
                if (user.Point + point >= 200000)
                {
                    var query4 = "UPDATE Customer SET RankID = 4 WHERE UserID = @ma";
                    _db.Database.ExecuteSqlCommand(query4, new SqlParameter("@ma", user.UserID));
                }
            }
        }
        public ActionResult GiaoHang(int id)
        {
            var order = _db.Orders.Where(s => s.OrderID == id).FirstOrDefault();
            if (order.StatusID == 3)
            {
                order.RecieveDate = DateTime.Now;
                order.StatusID = 4;
                order.CheckOut = true;
            }
            if(order.UserID!=null)
            {
                double point = Convert.ToDouble(order.TotalPrice * 0.01);
                AddPoint(order.UserID.Value, point);
            }    

            Receipt receipt = new Receipt();
            receipt.OrderID = order.OrderID;
            receipt.ReceiptDate = DateTime.Now;
            receipt.TotalPrice = order.TotalPrice;
            receipt.Payment = order.Payment;
            _db.Receipts.Add(receipt);
            _db.SaveChanges();

            //string customerContent = "<h3>Đơn hàng <b style='color: red; font-weight: bold'>" + order.OrderID + "</b> của bạn đã được giao thành công." +
            //    "Cảm ơn bạn đã tin tưởng và mua sắm tại 3Rings</h3>" +
            //    "Mã đơn hàng: " + order.OrderID + "<br>" +
            //    "Ngày đặt: " + order.OrderDate + "<br>" +
            //    "Ngày giao: " + order.RecieveDate + "<br>" +
            //    "Trạng thái: " + order.OrderStatu.StatusName + "<br>" +
            //    "Khách hàng: " + order.FullName + "<br>" +
            //    "Số điện thoại: " + order.PhoneNumber + "<br>" +
            //    "Địa chỉ giao hàng: " + order.ShippingAddress + "<br>" +
            //    "Tổng tiền: " + order.TotalPrice.Value.ToString("N0") + " VNĐ" + "<br>" +
            //    "<b>3Rings Xin cảm ơn!</b>";

            //var toCustomerEmail = order.Email.ToString();
            //new Mail().SendMail(toCustomerEmail, "Giao hàng thành công", customerContent);  
            return RedirectToAction("Index");
        }
        public ActionResult CancelOrder(int id)
        {
            var order = _db.Orders.Where(s => s.OrderID == id).FirstOrDefault();
            if (order.StatusID == 3)
            {
                var orderDetail = _db.OrderDetails.Where(s => s.OrderID == id).ToList();
                foreach (var pro in orderDetail)
                {
                    var product = _db.Products.Where(s => s.ProductID == pro.ProductID).FirstOrDefault();
                    product.Quantity += pro.Quantity;
                }
            }
            if (order.StatusID == 1 || order.StatusID == 2 || order.StatusID == 3)
            {
                order.StatusID = 5;
                _db.Entry(order).State = EntityState.Modified;
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Reconfirm(int id)
        {
            var order = _db.Orders.Where(s => s.OrderID == id).FirstOrDefault();
            if (order.StatusID == 5)
            {
                order.StatusID = 2;
                _db.Entry(order).State = EntityState.Modified;
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SoDonChoXacNhan()
        {
            ViewData["ChoXacNhan"] = _db.Orders.Where(s => s.OrderStatu.StatusID == 1).Count();
            return View();
        }
        public ActionResult SoDonChoLayHang()
        {
            ViewData["ChoLayHang"] = _db.Orders.Where(s => s.OrderStatu.StatusID == 2).Count();
            return View();
        }
        public ActionResult SoDonDangGiaoHang()
        {
            ViewData["DangGiaoHang"] = _db.Orders.Where(s => s.OrderStatu.StatusID == 3).Count();
            return View();
        }
        public ActionResult SoDonDaGiaoHang()
        {
            ViewData["DaGiaoHang"] = _db.Orders.Where(s => s.OrderStatu.StatusID == 4).Count();
            return View();
        }
        public ActionResult SoDonDaHuy()
        {
            ViewData["Dahuy"] = _db.Orders.Where(s => s.OrderStatu.StatusID == 5).Count();
            return View();
        }
        public ActionResult CountOrder()
        {
            int count = 0;
            int soDonChoXacNhan = Convert.ToInt32(ViewData["ChoXacNhan"] = _db.Orders.Where(s => s.OrderStatu.StatusID == 1).Count());
            int soDonChoLayHang = Convert.ToInt32(ViewData["ChoLayHang"] = _db.Orders.Where(s => s.OrderStatu.StatusID == 2).Count());
            int soDonDangGiaoHang = Convert.ToInt32(ViewData["DangGiaoHang"] = _db.Orders.Where(s => s.OrderStatu.StatusID == 3).Count());
            count = soDonChoLayHang + soDonChoXacNhan + soDonDangGiaoHang;
            ViewData["CountOrder"] = count.ToString();
            return View();
        }
        public ActionResult AllOrder()
        {
            ViewData["AllOrder"] = _db.Orders.Count();
            return View();
        }
    }
}