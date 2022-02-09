using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdventureWorksApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Products",
                url: "Pages/ProductsPage/View",
                defaults: new { controller = "Pages", action = "ProductsPage"}
            );

            routes.MapRoute(
                name: "Edit",
                url: "Pages/EditProductsPage/View",
                defaults: new { controller = "Pages", action = "EditProductsPage" }
            );

            routes.MapRoute(
                name: "Customers",
                url: "Pages/CustomersPage.aspx",
                defaults: new { controller = "Pages", action = "CustomersPage" }
            );

            routes.MapRoute(
                name: "History",
                url: "Pages/CustomerHistory/{buyerId}",
                defaults: new { controller = "Pages", action = "CustomerHistory" ,buyerId = UrlParameter.Optional }
           );
            
            routes.MapRoute(
                name: "Categories",
                url: "Pages/CategoriesPage",
                defaults: new { controller = "Pages", action = "CategoriesPage" }
           );
            
            routes.MapRoute(
                name: "Subcategories",
                url: "Pages/SubcategoriesPage",
                defaults: new { controller = "Pages", action = "SubcategoriesPage" }
           );

        }
    }
}
