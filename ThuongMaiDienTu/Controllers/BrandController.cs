using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class BrandController : Controller
    {
        MobilePhoneDBEntities _db = new MobilePhoneDBEntities();
        // GET: Brand
        public ActionResult Index(string search)
        {
            if (search == null)
            {
                return View(_db.Brands.ToList());
            }
            else
            {
                return View(_db.Brands.Where(s => s.BrandName.Contains(search)).ToList());
            }
        }
        public ActionResult Create()
        {
            Brand brand = new Brand();
            return View(brand);
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            try
            {
                if (brand.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(brand.ImageUpload.FileName);
                    string extension = Path.GetExtension(brand.ImageUpload.FileName);
                    fileName = fileName + extension;
                    brand.Image = "~/Content/Image/" + fileName;
                    brand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName));
                }
                // TODO: Add insert logic here
                _db.Brands.Add(brand);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(_db.Brands.Where(s => s.BrandID == id).FirstOrDefault());
        }

        // POST: Category/edit
        [HttpPost]
        public ActionResult Edit(Brand brand)
        {
            try
            {
                if (brand.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(brand.ImageUpload.FileName);
                    string extension = Path.GetExtension(brand.ImageUpload.FileName);
                    fileName = fileName + extension;
                    brand.Image = "~/Content/Image/" + fileName;
                    brand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName));
                }
                _db.Entry(brand).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            if (Session["AdminID"] != null && Session["AdminRole"] != null && Session["AdminRole"].ToString() == "1")
            {
                return View((_db.Brands.Where(s => s.BrandID == id).FirstOrDefault()));
            }
            else
            {
                return Content("Bạn không có quyền truy cập vào trang web này");
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Brand brand)
        {
            try
            {
                // TODO: Add delete logic here
                brand = _db.Brands.Where(x => x.BrandID == id).FirstOrDefault();
                _db.Brands.Remove(brand);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi ràng buộc khóa ngoại");
            }
        }
    }
}