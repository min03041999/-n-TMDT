using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class UserController : Controller
    {
        MobilePhoneDBEntities _db = new MobilePhoneDBEntities();
        // GET: User
        [HttpGet]
        [AllowAnonymous]
        [ActionName("dang-nhap")]
        public ActionResult SignIn()
        {
            return View("SignIn");
        }
        [HttpPost]
        [AllowAnonymous]
        [ActionName("dang-nhap")]
        public ActionResult SignIn(Customer _user)
        {
            var check = _db.Customers.Where(m => m.Email.Equals(_user.Email) &&
              m.Password.Equals(_user.Password)).FirstOrDefault();
            if (check == null)
            {
                _user.LogInErrorMessage = "Sai thông tin đăng nhập!";
                return View("SignIn", _user);
            }
            else
            {
                Session["UserID"] = check.UserID;
                Session["RankID"] = check.RankID;
                Session["UserEmail"] = check.Email;
                Session["UserName"] = check.FullName;
                Session["PhoneNumber"] = check.PhoneNumber;
                Session["Address"] = check.Address;
                return RedirectToAction("Index", "Home");
            }
        }
        [AllowAnonymous]
        [ActionName("dang-xuat")]
        public ActionResult LogOut()
        {
            Session.Clear();
            return View("SignIn");
        }
        [HttpGet]
        [AllowAnonymous]
        [ActionName("dang-ky")]
        public ActionResult SignUp()
        {
            return View("SignUp");
        }
        [HttpPost]
        [AllowAnonymous]
        [ActionName("dang-ky")]
        public ActionResult SignUp(Customer _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Customers.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _user.Point = 0;
                    _user.RankID = 1;
                    _db.Customers.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("dang-nhap", "User");
                }
                else
                {
                    ViewBag.error = "Email này đã được sử dụng!";
                    return View("SignUp");
                }
            }
            return View();
        }
        [AllowAnonymous]
        [ActionName("tai-khoan-cua-toi")]
        public ActionResult MyProfile()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            var user = _db.Customers.Where(s => s.UserID == id).FirstOrDefault();
            return View("MyProfile", user);
        }
        public ActionResult ChoXacNhan()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            return PartialView(_db.Orders.Where(s => s.UserID == id && s.OrderStatu.StatusID == 1).ToList());
        }
        public ActionResult ChoLayHang()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            return PartialView(_db.Orders.Where(s => s.UserID == id && s.OrderStatu.StatusID == 2).ToList());
        }
        public ActionResult DangGiaoHang()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            return PartialView(_db.Orders.Where(s => s.UserID == id && s.OrderStatu.StatusID == 3).ToList());
        }
        public ActionResult DaGiaoHang()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            return PartialView(_db.Orders.Where(s => s.UserID == id && s.OrderStatu.StatusID == 4).ToList());
        }
        public ActionResult DaHuy()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            return PartialView(_db.Orders.Where(s => s.UserID == id && s.OrderStatu.StatusID == 5).ToList());
        }
        [AllowAnonymous]
        [ActionName("chi-tiet-don-hang")]
        public ActionResult OrderDetail(int id)
        {
            var order = _db.OrderDetails.Where(s => s.OrderID == id).ToList();
            return View("OrderDetail", order);
        }
        [AllowAnonymous]
        [ActionName("cap-nhat-thong-tin")]
        public ActionResult UpdateProfile()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            var user = _db.Customers.Where(m => m.UserID == id).FirstOrDefault();
            return View("UpdateProfile", user);
        }
        [HttpPost]
        [AllowAnonymous]
        [ActionName("cap-nhat-thong-tin")]
        public ActionResult UpdateProfile(Customer _user)
        {
            try
            {
                // TODO: Add update logic here
                _db.Entry(_user).State = EntityState.Modified;
                _db.SaveChanges();
                Session["UserEmail"] = _user.Email;
                Session["UserName"] = _user.FullName;
                Session["PhoneNumber"] = _user.PhoneNumber;

                return RedirectToAction("tai-khoan-cua-toi", "User");
            }
            catch
            {
                return View("UpdateProfile");
            }
        }
        [AllowAnonymous]
        [ActionName("huy-don-hang")]
        public ActionResult CancelOrder(int id)
        {
            var order = _db.Orders.Where(s => s.OrderID == id).FirstOrDefault();
            if (order.StatusID == 1 || order.StatusID == 2 || order.StatusID == 3)
            {
                order.StatusID = 5;
                _db.Entry(order).State = EntityState.Modified;
            }
            _db.SaveChanges();
            return RedirectToAction("tai-khoan-cua-toi", "User");
        }
        [AllowAnonymous]
        [ActionName("hoan-tac")]
        public ActionResult Reconfirm(int id)
        {
            var order = _db.Orders.Where(s => s.OrderID == id).FirstOrDefault();
            if (order.StatusID == 5)
            {
                order.StatusID = 1;
                _db.Entry(order).State = EntityState.Modified;
            }
            _db.SaveChanges();
            return RedirectToAction("tai-khoan-cua-toi", "User");
        }
        public ActionResult SoDonChoXacNhan()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            ViewData["1"] = _db.Orders.Where(s =>s.UserID == id && s.OrderStatu.StatusID == 1).Count();
            return View();
        }
        public ActionResult SoDonChoLayHang()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            ViewData["2"] = _db.Orders.Where(s => s.UserID == id && s.OrderStatu.StatusID == 2).Count();
            return View();
        }
        public ActionResult SoDonDangGiaoHang()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            ViewData["3"] = _db.Orders.Where(s => s.UserID == id && s.OrderStatu.StatusID == 3).Count();
            return View();
        }
        public ActionResult SoDonDaGiaoHang()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            ViewData["4"] = _db.Orders.Where(s => s.UserID == id && s.OrderStatu.StatusID == 4).Count();
            return View();
        }
        public ActionResult SoDonDaHuy()
        {
            int id = Convert.ToInt32(Session["UserID"]);
            ViewData["5"] = _db.Orders.Where(s => s.UserID == id && s.OrderStatu.StatusID == 5).Count();
            return View();
        }
    }
}