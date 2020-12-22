using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class HomeController : Controller
    {

        MobilePhoneDBEntities _db = new MobilePhoneDBEntities();
        public ActionResult Index()
        {
            return View(_db.BestSellers.Take(6).ToList());
        }
        [AllowAnonymous]
        [ActionName("ve-chung-toi")]
        public ActionResult About()
        {
            return View("About");
        }
        [HttpGet]
        [AllowAnonymous]
        [ActionName("gui-lien-he")]
        public ActionResult Contact()
        {
            return View("Contact");
        }
        [HttpPost]
        [AllowAnonymous]
        [ActionName("gui-lien-he")]
        public ActionResult Contact(Contact _contact)
        {
            int id = Convert.ToInt32(Session["UserID"]);
            if (ModelState.IsValid)
            {
                _db.Configuration.ValidateOnSaveEnabled = false;
                _contact.ContactDate = DateTime.Now;
                _contact.UserID = id;
                _contact.Status = false;
                _db.Contacts.Add(_contact);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View("Contact");
        }
        [AllowAnonymous]
        [ActionName("chi-tiet-san-pham")]
        public ActionResult Detail(string name)
        {
            var rate = new Rating();
            var pro = _db.Products.Where(s => s.ProductName == name).FirstOrDefault();
            return View("Detail", pro);
        }
        public ActionResult RelatedProducts(string name)
        {
            var pro = _db.Products.Where(s => s.ProductName == name).FirstOrDefault();

            var result = from r in _db.Products
                         where (r.BrandID == pro.BrandID
                         && r.ProductName != name)
                         select r;
            return PartialView(result.ToList().Take(4));
        }
        public ActionResult NewArrival()
        {
            return PartialView(_db.Products.OrderByDescending(m => m.ImportDate).Take(6));
        }
        public ActionResult LowestProducts()
        {
            return PartialView(_db.Products.OrderBy(m => m.Price).Take(6));
        }
        public ActionResult Apple()
        {
            return PartialView(_db.Products.Where(s => s.BrandID == 1).OrderByDescending(m => m.ImportDate).Take(6));
        }
        public ActionResult Samsung()
        {
            return PartialView(_db.Products.Where(s => s.BrandID == 2).OrderByDescending(m => m.ImportDate).Take(6));
        }
        [AllowAnonymous]
        [ActionName("tim-kiem")]
        public ActionResult SearchResult(string search, string Price)
        {
            if (search == null)
            {
                return View("SearchResult",_db.Products.ToList());
            }
            else
            {
                if (Price == "option2")
                    return View("SearchResult", _db.Products.Where(s => s.Price < 5000000 && (s.ProductName.Contains(search) || s.FullName.Contains(search) || s.Brand.BrandName == search)).ToList());
                if (Price == "option3")
                    return View("SearchResult", _db.Products.Where(s => s.Price >= 5000000 && s.Price < 10000000 && (s.ProductName.Contains(search) || s.FullName.Contains(search) || s.Brand.BrandName == search)).ToList());
                if (Price == "option4")
                    return View("SearchResult", _db.Products.Where(s => s.Price >= 10000000 && s.Price < 15000000 && (s.ProductName.Contains(search) || s.FullName.Contains(search) || s.Brand.BrandName == search)).ToList());
                if (Price == "option5")
                    return View("SearchResult", _db.Products.Where(s => s.Price >= 15000000 && (s.ProductName.Contains(search) || s.FullName.Contains(search) || s.Brand.BrandName == search)).ToList());
                return View("SearchResult", _db.Products.Where(s => s.ProductName.Contains(search) || s.FullName.Contains(search) || s.Brand.BrandName == search).ToList());
            }
        }
        [AllowAnonymous]
        [ActionName("dien-thoai")]
        public ActionResult AllPhone(string Price)
        {
            if (Price == "option2")
                return View("AllPhone", _db.Products.Where(s => s.Price < 5000000).ToList());
            if (Price == "option3")
                return View("AllPhone", _db.Products.Where(s => s.Price >= 5000000 && s.Price < 10000000).ToList());
            if (Price == "option4")
                return View("AllPhone", _db.Products.Where(s => s.Price >= 10000000 && s.Price < 15000000).ToList());
            if (Price == "option5")
                return View("AllPhone", _db.Products.Where(s => s.Price >= 15000000).ToList());
            else
                return View("AllPhone", _db.Products.ToList());
        }
        [AllowAnonymous]
        [ActionName("dien-thoai-apple")]
        public ActionResult ApplePhone(string Price)
        {
            if (Price == "option2")
                return View("ApplePhone", _db.Products.Where(s => s.Price < 5000000 && s.BrandID == 1).ToList());
            if (Price == "option3")
                return View("ApplePhone", _db.Products.Where(s => s.Price >= 5000000 && s.Price < 10000000 && s.BrandID == 1).ToList());
            if (Price == "option4")
                return View("ApplePhone", _db.Products.Where(s => s.Price >= 10000000 && s.Price < 15000000 && s.BrandID == 1).ToList());
            if (Price == "option5")
                return View("ApplePhone", _db.Products.Where(s => s.Price >= 15000000 && s.BrandID == 1).ToList());
            else
                return View("ApplePhone", _db.Products.Where(s => s.BrandID == 1).ToList());
        }
        [AllowAnonymous]
        [ActionName("dien-thoai-samsung")]
        public ActionResult SamsungPhone(string Price)
        {
            if (Price == "option2")
                return View("SamsungPhone", _db.Products.Where(s => s.Price < 5000000 && s.BrandID == 2).ToList());
            if (Price == "option3")
                return View("SamsungPhone", _db.Products.Where(s => s.Price >= 5000000 && s.Price < 10000000 && s.BrandID == 2).ToList());
            if (Price == "option4")
                return View("SamsungPhone", _db.Products.Where(s => s.Price >= 10000000 && s.Price < 15000000 && s.BrandID == 2).ToList());
            if (Price == "option5")
                return View("SamsungPhone", _db.Products.Where(s => s.Price >= 15000000 && s.BrandID == 2).ToList());
            else
                return View("SamsungPhone", _db.Products.Where(s => s.BrandID == 2).ToList());
        }
        [AllowAnonymous]
        [ActionName("dien-thoai-oppo")]
        public ActionResult OPPOPhone(string Price)
        {
            if (Price == "option2")
                return View("OPPOPhone", _db.Products.Where(s => s.Price < 5000000 && s.BrandID == 3).ToList());
            if (Price == "option3")
                return View("OPPOPhone", _db.Products.Where(s => s.Price >= 5000000 && s.Price < 10000000 && s.BrandID == 3).ToList());
            if (Price == "option4")
                return View("OPPOPhone", _db.Products.Where(s => s.Price >= 10000000 && s.Price < 15000000 && s.BrandID == 3).ToList());
            if (Price == "option5")
                return View("OPPOPhone", _db.Products.Where(s => s.Price >= 15000000 && s.BrandID == 3).ToList());
            else
                return View("OPPOPhone", _db.Products.Where(s => s.BrandID == 3).ToList());
        }
        [AllowAnonymous]
        [ActionName("dien-thoai-vivo")]
        public ActionResult VivoPhone(string Price)
        {
            if (Price == "option2")
                return View("VivoPhone", _db.Products.Where(s => s.Price < 5000000 && s.BrandID == 4).ToList());
            if (Price == "option3")
                return View("VivoPhone", _db.Products.Where(s => s.Price >= 5000000 && s.Price < 10000000 && s.BrandID == 4).ToList());
            if (Price == "option4")
                return View("VivoPhone", _db.Products.Where(s => s.Price >= 10000000 && s.Price < 15000000 && s.BrandID == 4).ToList());
            if (Price == "option5")
                return View("VivoPhone", _db.Products.Where(s => s.Price >= 15000000 && s.BrandID == 4).ToList());
            else
                return View("VivoPhone", _db.Products.Where(s => s.BrandID == 4).ToList());
        }
        [AllowAnonymous]
        [ActionName("dien-thoai-xiaomi")]
        public ActionResult XiaomiPhone(string Price)
        {
            if (Price == "option2")
                return View("XiaomiPhone", _db.Products.Where(s => s.Price < 5000000 && s.BrandID == 5).ToList());
            if (Price == "option3")
                return View("XiaomiPhone", _db.Products.Where(s => s.Price >= 5000000 && s.Price < 10000000 && s.BrandID == 5).ToList());
            if (Price == "option4")
                return View("XiaomiPhone", _db.Products.Where(s => s.Price >= 10000000 && s.Price < 15000000 && s.BrandID == 5).ToList());
            if (Price == "option5")
                return View("XiaomiPhone", _db.Products.Where(s => s.Price >= 15000000 && s.BrandID == 5).ToList());
            else
                return View("XiaomiPhone", _db.Products.Where(s => s.BrandID == 5).ToList());
        }
        [AllowAnonymous]
        [ActionName("dien-thoai-realme")]
        public ActionResult RealmePhone(string Price)
        {
            if (Price == "option2")
                return View("RealmePhone", _db.Products.Where(s => s.Price < 5000000 && s.BrandID == 6).ToList());
            if (Price == "option3")
                return View("RealmePhone", _db.Products.Where(s => s.Price >= 5000000 && s.Price < 10000000 && s.BrandID == 6).ToList());
            if (Price == "option4")
                return View("RealmePhone", _db.Products.Where(s => s.Price >= 10000000 && s.Price < 15000000 && s.BrandID == 6).ToList());
            if (Price == "option5")
                return View("RealmePhone", _db.Products.Where(s => s.Price >= 15000000 && s.BrandID == 6).ToList());
            else
                return View("RealmePhone", _db.Products.Where(s => s.BrandID == 6).ToList());
        }
    

        [HttpGet]
        public ActionResult Rating()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Rating(FormCollection form, string name)
        {
            string email = Session["UserEmail"].ToString();
            var pro = _db.Products.Where(s => s.ProductName == name).FirstOrDefault();
            var userID = _db.Customers.Single(m => m.Email.Equals(email)).UserID;
            var check = _db.Ratings.Where(s => s.UserID == userID && s.Product.ProductName == name).FirstOrDefault();
            if (check == null)
            {
                try
                {
                    Rating _rate = new Rating();
                    _rate.RatingDate = DateTime.Now;
                    _rate.ProductID = pro.ProductID;
                    _rate.UserID = _db.Customers.Single(m => m.Email.Equals(email)).UserID;
                    _rate.Content = form["content"];
                    _rate.RatingPoint = double.Parse(form["point"]);
                    _rate.RatingState = true;
                    _db.Ratings.Add(_rate);
                    _db.SaveChanges();
                    return RedirectToAction("chi-tiet-san-pham", "Home", new { name = pro.ProductName });
                }
                catch
                {
                    return RedirectToAction("chi-tiet-san-pham", "Home", new { name = pro.ProductName });
                }
            }
            return RedirectToAction("chi-tiet-san-pham", "Home", new { name = pro.ProductName });
        }
        public ActionResult CountRating(string name)
        {
            var pro = _db.Products.Where(s => s.ProductName == name).FirstOrDefault();
            int totalRating = _db.Ratings.Where(s => s.Product.ProductName == name && s.RatingState==true).Count();
            if (totalRating != 0)
            {
                double totalPoint = (double)_db.Ratings.Where(s => s.Product.ProductName == name && s.RatingState==true).Sum(s => s.RatingPoint);
                double point = totalPoint / totalRating;
                ViewData["TotalRating"] = totalRating;
                ViewData["Point"] = point;
                return View();
            }
            else
                return View();
        }
        public static class PaypalConfiguration
        {
            //Variables for storing the clientID and clientSecret key  
            public readonly static string ClientId;
            public readonly static string ClientSecret;
            //Constructor  
            static PaypalConfiguration()
            {
                var config = GetConfig();
                ClientId = config["clientId"];
                ClientSecret = config["clientSecret"];
            }
            // getting properties from the web.config  
            public static Dictionary<string, string> GetConfig()
            {
                return PayPal.Api.ConfigManager.Instance.GetProperties();
            }
            private static string GetAccessToken()
            {
                // getting accesstocken from paypal  
                string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
                return accessToken;
            }
            public static APIContext GetAPIContext()
            {
                // return apicontext object by invoking it with the accesstoken  
                APIContext apiContext = new APIContext(GetAccessToken());
                apiContext.Config = GetConfig();
                return apiContext;
            }
        }
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return RedirectToAction("OrderFailure", "ShoppingCart");
                    }
                }
            }
            //A resource representing a Payer that funds a payment Payment Method as paypal  
            //Payer Id will be returned when payment proceeds or click to pay  

            catch (Exception ex)
            {
                return RedirectToAction("OrderFailure", "ShoppingCart");
            }
            //on successful payment, show success page to user.           
            return RedirectToAction("OrderSuccessfully", "ShoppingCart");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            Cart cart = Session["Cart"] as Cart;

            float _subtotal = 0;
            float phiVanChuyen = 1;
            float thue = 0;
            float giamGia = 0;
            foreach (var item in cart.Items)
            {
                float tempPrice = 0;
                tempPrice = (float)(item._shopping_product.Price / 23000);
                
                itemList.items.Add(new Item()
                {
                    name = item._shopping_product.ProductName.ToString(),
                    currency = "USD",
                    price = tempPrice.ToString(),
                    quantity = item._shopping_quantity.ToString(),
                    sku = "Mã SP: #" + item._shopping_product.ProductID,
                }) ; ;
                _subtotal += tempPrice * item._shopping_quantity;
            }
            Models.Order _order = new Models.Order();


            _order.OrderDate = DateTime.Now;
            _order.RecieveDate = null;
            _order.StatusID = 1;
            _order.Note = Request.Form["Note"];
            _order.ShippingAddress = Request.Form["ShippingAddress"];
            _order.Price = 0;
            _order.TotalPrice = 0;
            _order.Discount = 0;
            _order.ShippingCost = 23000;
            _order.Payment = true;
            _order.CheckOut = false;
            if (Session["UserID"] == null)
            {
                
                _order.UserID = null;
                _order.FullName = Request.Form["FullName"];
                _order.PhoneNumber = Request.Form["PhoneNumber"];
                _order.Email = Request.Form["Email"];
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
                if (user.RankID == 1)
                {
                    _order.ShippingCost = 23000;
                    _order.Discount = 0.02 * _subtotal * 23000;
                    giamGia = _subtotal * 2 /100;
                    phiVanChuyen = 1;
                }
                if (user.RankID == 2)
                {
                    _order.ShippingCost = 23000;
                    _order.Discount = 0.05 * _subtotal * 23000;
                    giamGia = _subtotal * 5 / 100;
                    phiVanChuyen = 1;
                }
                if (user.RankID == 3)
                {                   
                    _order.ShippingCost = 0;
                    _order.Discount = 0.08 * _subtotal * 23000;
                    giamGia = _subtotal * 8 / 100;
                    phiVanChuyen = 0;
                }
                if (user.RankID == 4)
                {
                    _order.ShippingCost = 0;
                    _order.Discount = 0.1 * _subtotal * 23000;
                    giamGia = _subtotal * 10 / 100;
                    phiVanChuyen = 0;
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
            _order.TotalPrice = _order.Price + _order.VAT + _order.ShippingCost - _order.Discount;

            thue = _subtotal * 10 / 100;
            _db.SaveChanges();

            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = thue.ToString(),
                shipping = phiVanChuyen.ToString(), 
                shipping_discount = giamGia.ToString(),
                subtotal = _subtotal.ToString(),
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = (_subtotal + thue + phiVanChuyen -giamGia).ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details,

            };
            DateTime dt = DateTime.Now;
            String date;
            date = dt.ToString("yyyyddMMhhmmss", DateTimeFormatInfo.InvariantInfo);
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = date,
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            _order.CheckOut = true;
            // Create a payment using a APIContext  
            _db.SaveChanges();
            cart.ClearCart();
            return this.payment.Create(apiContext);
        }
    }

}