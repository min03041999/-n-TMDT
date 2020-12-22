using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class AdminController : Controller
    {
        MobilePhoneDBEntities _db = new MobilePhoneDBEntities();
        // GET: Admin
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(Admin _ad)
        {
            var check = _db.Admins.Where(m => m.Email.Equals(_ad.Email) &&
              m.Password.Equals(_ad.Password)).FirstOrDefault();
            if (check == null)
            {
                _ad.LogInErrorMessage = "Sai thông tin đăng nhập!";
                return View("SignIn", _ad);
            }
            else
            {
                Session["AdminID"] = check.AdminID;
                Session["AdminEmail"] = check.Email;
                Session["AdminName"] = check.FullName;
                Session["AdminRole"] = check.RoleID;
                Session["AdminRoleName"] = check.Role.RoleName;
                return RedirectToAction("Index", "Order");
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("SignIn", "Admin");
        }
        public ActionResult UserList(string search)
        {
            if (search == null)
            {
                return View(_db.Customers.ToList());
            }
            else
            {
                return View(_db.Customers.Where(s => s.FullName.Contains(search) || s.Email.Contains(search) || s.PhoneNumber.Contains(search)).ToList());
            }
        }
        public ActionResult StaffList(string search)
        {
            if (search == null)
            {
                return View(_db.Admins.ToList());
            }
            else
            {
                return View(_db.Admins.Where(s => s.FullName.Contains(search) || s.Email.Contains(search) || s.PhoneNumber.Contains(search)).ToList());
            }
        }
        public ActionResult UserDetail(int id)
        {
            var user = _db.Customers.Where(s => s.UserID == id).FirstOrDefault();
            return View(user);
        }
        public ActionResult StaffDetail(int id)
        {
            if (Session["AdminID"] != null && Session["AdminRole"] != null && Session["AdminRole"].ToString() == "1")
            {
                var user = _db.Admins.Where(s => s.AdminID == id).FirstOrDefault();
                return View(user);
            }
            else
                return Content("Bạn không được quyền truy cập trang web này");

        }
        public ActionResult ChooseRole()
        {
            Role role = new Role();
            role.RoleCollection = _db.Roles.ToList<Role>();
            return PartialView(role);
        }
        [HttpGet]
        public ActionResult AddNewStaff()
        {
            if (Session["AdminID"] != null && Session["AdminRole"] != null && Session["AdminRole"].ToString() == "1")
                return View();
            else
                return Content("Bạn không được quyền truy cập trang web này");
        }
        [HttpPost]
        public ActionResult AddNewStaff(Admin _ad)
        {
            var check = _db.Admins.FirstOrDefault(s => s.Email == _ad.Email);
            if (check == null)
            {
                _db.Configuration.ValidateOnSaveEnabled = false;
                _db.Admins.Add(_ad);
                _db.SaveChanges();
                return RedirectToAction("StaffList");
            }
            else
            {
                ViewBag.error = "Email này đã được sử dụng!";
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            if (Session["AdminID"] != null && Session["AdminRole"] != null && Session["AdminRole"].ToString() == "1")
                if (id == 1)
                    return Content("Không thể edit tài khoản này");
                else
                    return View(_db.Admins.Where(s => s.AdminID == id).FirstOrDefault());
            else
                return Content("Bạn không được quyền truy cập trang web này");
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Admin _ad)
        {
            try
            {
                // TODO: Add update logic here
                _db.Entry(_ad).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("StaffList");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteStaff(int id)
        {
            if (Session["AdminID"] != null && Session["AdminRole"] != null && Session["AdminRole"].ToString() == "1")
                return View(_db.Admins.Where(s => s.AdminID == id).FirstOrDefault());
            else
                return Content("Bạn không được quyền truy cập trang web này");
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult DeleteStaff(int id, Admin _ad)
        {
            var rolename = _db.Admins.Where(s => s.AdminID == id).FirstOrDefault();
            if (rolename.RoleID != 1)
            {
                try
                {
                    // TODO: Add delete logic here
                    _ad = _db.Admins.Where(x => x.AdminID == id).FirstOrDefault();
                    _db.Admins.Remove(_ad);
                    _db.SaveChanges();
                    return RedirectToAction("StaffList");
                }
                catch
                {
                    return Content("Lỗi ràng buộc khóa ngoại");
                }
            }
            else
            {
                return Content("Không thể xóa tài khoản này");
            }
        }
        public ActionResult DeleteUser(int id)
        {
            if (Session["AdminID"] != null && Session["AdminRole"] != null && Session["AdminRole"].ToString() == "1")
                return View(_db.Customers.Where(s => s.UserID == id).FirstOrDefault());
            else
                return Content("Bạn không được quyền truy cập trang web này");
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult DeleteUser(int id, Customer _user)
        {
            try
            {
                // TODO: Add delete logic here
                _user = _db.Customers.Where(x => x.UserID == id).FirstOrDefault();
                _db.Customers.Remove(_user);
                _db.SaveChanges();
                return RedirectToAction("UserList");
            }
            catch
            {
                return Content("Lỗi ràng buộc khóa ngoại");
            }
        }
    }
}