using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class ShoppingCartController : Controller
    {
        MobilePhoneDBEntities _db = new MobilePhoneDBEntities();
        // GET: ShoppingCart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var pro = _db.Products.SingleOrDefault(s => s.ProductID == id);
            if (pro != null)
            {
                if (pro.Quantity <= 0)
                {
                    return Content("Sản phẩm đã bán hết");
                }
                GetCart().Add(pro);
            }
            string url = this.Request.UrlReferrer.AbsolutePath;
            //return RedirectToAction("Index", "Home");
            return Redirect(url);
        }
        [AllowAnonymous]
        [ActionName("gio-hang-cua-toi")]
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Cart cart = Session["Cart"] as Cart;
            return View("ShowCart", cart);
        }
        [AllowAnonymous]
        [ActionName("cap-nhat-so-luong")]
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            var pro = _db.Products.Where(s => s.ProductID == id_pro).FirstOrDefault();
            if (quantity > pro.Quantity)
                return Content("Xin lỗi! Chúng tôi không còn đủ sản phẩm.");
            else
                cart.Update_Quantity_Shopping(id_pro, quantity);
                return RedirectToAction("gio-hang-cua-toi", "ShoppingCart");
        }
        [AllowAnonymous]
        [ActionName("xoa-san-pham")]
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("gio-hang-cua-toi", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int _t_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                _t_item = cart.Total_Quantity();
            }
            ViewBag.infoCart = _t_item;
            return PartialView("BagCart");
        }
        public ActionResult OrderSuccessfully()
        {
            return View();
        }

        public ActionResult CheckOut(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            Order _order = new Order();
            _order.OrderDate = DateTime.Now;
            _order.RecieveDate = null;
            _order.StatusID = 1;
            _order.Note = form["Note"];
            _order.ShippingAddress = form["ShippingAddress"];
            _order.Price = 0;
            _order.TotalPrice = 0;
            _order.Discount = 0;
            _order.ShippingCost = 23000;
            _order.Payment = false;
            _order.CheckOut = false;

            if (Session["UserID"] == null)
            {
                _order.UserID = null;
                _order.FullName = form["FullName"];
                _order.PhoneNumber = form["PhoneNumber"];
                _order.Email = form["Email"];
            }
            else
            {
                int id = Convert.ToInt32(Session["UserID"]);
                string email = Session["UserEmail"].ToString();
                string phone = Session["PhoneNumber"].ToString();
                string name = Session["UserName"].ToString();

                _order.UserID = id;
                _order.FullName = name;
                _order.PhoneNumber = phone;
                _order.Email = email;
                var user = _db.Customers.Where(s => s.UserID == id).FirstOrDefault();
                if(user.RankID==1)
                {
                    _order.Discount = 0.02;
                    _order.ShippingCost = 23000;
                }
                if (user.RankID == 2)
                {
                    _order.Discount = 0.05;
                    _order.ShippingCost = 23000;
                }
                if (user.RankID == 3)
                {
                    _order.Discount = 0.08;
                    _order.ShippingCost = 0;
                }
                if (user.RankID == 4)
                {
                    _order.Discount = 0.1;
                    _order.ShippingCost = 0;
                }
            }
            _db.Orders.Add(_order);
            foreach (var item in cart.Items)
            {
                OrderDetail _orderDetail = new OrderDetail();
                _orderDetail.OrderID = _order.OrderID;
                _orderDetail.ProductID = item._shopping_product.ProductID;
                _orderDetail.Quantity = item._shopping_quantity;
                _orderDetail.TotalPrice = item._shopping_product.Price * item._shopping_quantity;

                _order.Price += _orderDetail.TotalPrice;
                _db.OrderDetails.Add(_orderDetail);
            }

            _order.VAT = 0.1 * _order.Price;
            _order.Discount = _order.Discount * _order.Price;
            _order.TotalPrice = _order.Price + _order.VAT + _order.ShippingCost - _order.Discount;
            _db.SaveChanges();
            cart.ClearCart();

            string customerContent =
            "<h3>Cảm ơn bạn đã đặt hàng tại <b style='color: red; font-weight: bold'>3Rings</b></h3>" +
            "Mã đơn hàng: " + _order.OrderID + "<br>" +
            "Ngày đặt: " + _order.OrderDate + "<br>" +
            "Khách hàng: " + _order.FullName + "<br>" +
            "Số điện thoại: " + _order.PhoneNumber + "<br>" +
            "Địa chỉ giao hàng: " + _order.ShippingAddress + "<br>" +
            "Tổng tiền: " + _order.TotalPrice.Value.ToString("N0") + " VNĐ" + "<br>" + "<hr>" +
            "Click vào <a href='https://localhost:44343/gio-hang/theo-doi-don-hang'>đây</a> để theo dõi đơn hàng của bạn.<br>" +
            "<b>3Rings Xin cảm ơn!</b>";

            string managerContent =
            "Mã đơn hàng: " + _order.OrderID + "<br>" +
            "Ngày đặt: " + _order.OrderDate + "<br>" +
            "Khách hàng: " + _order.FullName + "<br>" +
            "Số điện thoại: " + _order.PhoneNumber + "<br>" +
            "Địa chỉ giao hàng: " + _order.ShippingAddress + "<br>" +
            "Tổng tiền: " + _order.TotalPrice.Value.ToString("N0") + " VNĐ" + "<br>" + "<hr>" +
            "Click vào <a href='https://localhost:44343/Order/OrderDetail/" + _order.OrderID + "'>đây</a> để xem chi tiết và xác nhận đơn hàng này.";


            var toCustomerEmail = _order.Email.ToString();
            new Mail().SendMail(toCustomerEmail, "Bạn vừa đặt một đơn hàng từ 3Rings", customerContent);
            var toManagerEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
            new Mail().SendMail(toManagerEmail, "3Rings vừa nhận được một đơn hàng mới", managerContent);


            return RedirectToAction("OrderSuccessfully", "ShoppingCart");
        }
        [AllowAnonymous]
        [ActionName("theo-doi-don-hang")]
        public ActionResult TrackingOrder(string phoneNumber)
        {
            var order = _db.Orders.Where(s => s.PhoneNumber == phoneNumber).ToList();
            return View("TrackingOrder", order);
        }
    }
}