using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;
using PagedList.Mvc;
using PagedList;

namespace ThuongMaiDienTu.Controllers
{
    public class ProductController : Controller
    {
        MobilePhoneDBEntities _db = new MobilePhoneDBEntities();
        // GET: Product
        public ActionResult Index(string search, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            if (search == null)
            {

                return View(_db.Products.ToList().ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View(_db.Products.Where(s => s.FullName.Contains(search) || s.Brand.BrandName.Contains(search)).ToList().ToPagedList(pageNumber, pageSize));
            }
        }
        // GET: Product/Create
        public ActionResult Create()
        {
            Product pro = new Product();
            return View(pro);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product pro)
        {
            try
            {
                if (pro.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);
                    fileName = fileName + extension;
                    pro.Image = "~/Content/Image/" + fileName;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName));
                }
                if (pro.ImageUpload2 != null)
                {
                    string fileName2 = Path.GetFileNameWithoutExtension(pro.ImageUpload2.FileName);
                    string extension2 = Path.GetExtension(pro.ImageUpload2.FileName);
                    fileName2 = fileName2 + extension2;
                    pro.Image2 = "~/Content/Image/" + fileName2;
                    pro.ImageUpload2.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName2));
                }
                if (pro.ImageUpload3 != null)
                {
                    string fileName3 = Path.GetFileNameWithoutExtension(pro.ImageUpload3.FileName);
                    string extension3 = Path.GetExtension(pro.ImageUpload3.FileName);
                    fileName3 = fileName3 + extension3;
                    pro.Image3 = "~/Content/Image/" + fileName3;
                    pro.ImageUpload3.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName3));
                }
                if (pro.ImageUpload4 != null)
                {
                    string fileName4 = Path.GetFileNameWithoutExtension(pro.ImageUpload4.FileName);
                    string extension4 = Path.GetExtension(pro.ImageUpload4.FileName);
                    fileName4 = fileName4 + extension4;
                    pro.Image4 = "~/Content/Image/" + fileName4;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName4));
                }
                if (pro.ImageUpload5 != null)
                {
                    string fileName5 = Path.GetFileNameWithoutExtension(pro.ImageUpload5.FileName);
                    string extension5 = Path.GetExtension(pro.ImageUpload5.FileName);
                    fileName5 = fileName5 + extension5;
                    pro.Image5 = "~/Content/Image/" + fileName5;
                    pro.ImageUpload5.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName5));
                }
                if (pro.ImageUpload6 != null)
                {
                    string fileName6 = Path.GetFileNameWithoutExtension(pro.ImageUpload6.FileName);
                    string extension6 = Path.GetExtension(pro.ImageUpload6.FileName);
                    fileName6 = fileName6 + extension6;
                    pro.Image6 = "~/Content/Image/" + fileName6;
                    pro.ImageUpload6.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName6));
                }
                // TODO: Add insert logic here
                _db.Products.Add(pro);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ChooseBrand()
        {
            Brand brand = new Brand();
            brand.BrandCollection = _db.Brands.ToList<Brand>();
            return PartialView(brand);
        }
        public ActionResult Edit(int id)
        {
            return View(_db.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Product pro)
        {
            try
            {
                if (pro.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);
                    fileName = fileName + extension;
                    pro.Image = "~/Content/Image/" + fileName;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName));
                }
                if (pro.ImageUpload2 != null)
                {
                    string fileName2 = Path.GetFileNameWithoutExtension(pro.ImageUpload2.FileName);
                    string extension2 = Path.GetExtension(pro.ImageUpload2.FileName);
                    fileName2 = fileName2 + extension2;
                    pro.Image2 = "~/Content/Image/" + fileName2;
                    pro.ImageUpload2.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName2));
                }
                if (pro.ImageUpload3 != null)
                {
                    string fileName3 = Path.GetFileNameWithoutExtension(pro.ImageUpload3.FileName);
                    string extension3 = Path.GetExtension(pro.ImageUpload3.FileName);
                    fileName3 = fileName3 + extension3;
                    pro.Image3 = "~/Content/Image/" + fileName3;
                    pro.ImageUpload3.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName3));
                }
                if (pro.ImageUpload4 != null)
                {
                    string fileName4 = Path.GetFileNameWithoutExtension(pro.ImageUpload4.FileName);
                    string extension4 = Path.GetExtension(pro.ImageUpload4.FileName);
                    fileName4 = fileName4 + extension4;
                    pro.Image4 = "~/Content/Image/" + fileName4;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName4));
                }
                if (pro.ImageUpload5 != null)
                {
                    string fileName5 = Path.GetFileNameWithoutExtension(pro.ImageUpload5.FileName);
                    string extension5 = Path.GetExtension(pro.ImageUpload5.FileName);
                    fileName5 = fileName5 + extension5;
                    pro.Image5 = "~/Content/Image/" + fileName5;
                    pro.ImageUpload5.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName5));
                }
                if (pro.ImageUpload6 != null)
                {
                    string fileName6 = Path.GetFileNameWithoutExtension(pro.ImageUpload6.FileName);
                    string extension6 = Path.GetExtension(pro.ImageUpload6.FileName);
                    fileName6 = fileName6 + extension6;
                    pro.Image6 = "~/Content/Image/" + fileName6;
                    pro.ImageUpload6.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/"), fileName6));
                }
                // TODO: Add insert logic here
                _db.Entry(pro).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Detail(int id)
        {
            var rate = new Rating();
            return View(_db.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }

        public ActionResult Delete(int id)
        {
            if (Session["AdminID"] != null && Session["AdminRole"] != null && Session["AdminRole"].ToString() == "1")
            {
                return View((_db.Products.Where(s => s.ProductID == id).FirstOrDefault()));
            }
            else
            {
                return Content("Bạn không có quyền truy cập vào trang web này");
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            try
            {
                // TODO: Add delete logic here
                pro = _db.Products.Where(x => x.ProductID == id).FirstOrDefault();
                _db.Products.Remove(pro);
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