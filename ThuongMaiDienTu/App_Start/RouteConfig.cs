using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThuongMaiDienTu
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //create new route  
            routes.MapRoute(
              name: "User",
              url: "tai-khoan/{action}/{id}",
              defaults: new { controller = "User", action = "SignIn", id = UrlParameter.Optional }
            );

            //create new route  
            routes.MapRoute(
              name: "ShoppingCart",
              url: "gio-hang/{action}/{id}",
              defaults: new { controller = "ShoppingCart", action = "ShowCart", id = UrlParameter.Optional }
            );

            //create new route  
            routes.MapRoute(
              name: "Home",
              url: "trang-chu/{action}/{name}",
              defaults: new { controller = "Home", action = "AllPhone", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
           
        }
    }
}
