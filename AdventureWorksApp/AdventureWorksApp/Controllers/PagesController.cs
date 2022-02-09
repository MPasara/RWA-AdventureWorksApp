using AdventureWorksApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebForms.DAO;

namespace AdventureWorksApp.Controllers
{
    public class PagesController : Controller
    {
        // GET: Pages
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProductsPage()
        {
            if (Request.Cookies["loginData"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            var products = SqlHandler.GetProducts();
            
            return View(products);
        }

        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            SqlHandler.AddProduct(p);
            return RedirectToRoute("Edit");
        }

        [HttpPost]
        public ActionResult DeleteProduct(int idProduct)
        {
            SqlHandler.DeleteProduct(idProduct);
            return RedirectToRoute("Products");
        }

        public ActionResult CustomersPage() => RedirectToRoute("Customers");

        public ActionResult CustomerHistory(int customerId)
        {
            if (Request.Cookies["loginData"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            List<Bill> bills = SqlHandler.GetBills(customerId);
            ViewBag.customer = SqlHandler.GetCustomer(customerId);
            return View(bills);
        }


        public ActionResult EditProductsPage()
        {
            if (Request.Cookies["loginData"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            var products = SqlHandler.GetProducts();
            var subs = SqlHandler.GetSubcategories();
            List<SelectListItem> s = new List<SelectListItem>();
            foreach (var sub in subs)
            {
                s.Add(new SelectListItem() { Text = sub.SubcategoryName, Value = sub.IDSubcategory.ToString() });
            }
            ViewBag.subcategories = s;
            return View(products);
        }

        public ActionResult CategoriesPage()
        {
            if (Request.Cookies["loginData"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            var categories = SqlHandler.GetCategories();
            return View(categories);
        }

        [HttpPost]
        public ActionResult AddCategory(Category c)
        {
            SqlHandler.AddCategory(c);
            return RedirectToRoute("Categories");
        }
        public ActionResult DeleteCategory(int idCategory)
        {
            SqlHandler.DeleteCategory(idCategory);
            return RedirectToRoute("Categories");
        }

        public ActionResult EditCategory(int idCategory, string naziv)
        {
            SqlHandler.EditCategory(idCategory,naziv);
            return RedirectToRoute("Categories");
        }

        public ActionResult SubcategoriesPage()
        {
            if (Request.Cookies["loginData"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            var categories = SqlHandler.GetCategories();
            var subcategories = SqlHandler.GetSubcategories();
            List<SelectListItem> cats = new List<SelectListItem>();
            foreach (var c in categories)
            {
                cats.Add(new SelectListItem() { Text = c.CategoryName, Value = c.IDCategory.ToString()});
            }
            ViewBag.categories = cats;
            return View(subcategories);
        }

        [HttpPost]
        public ActionResult AddSubcategory(Subcategory s)
        {
            SqlHandler.AddSubcategory(s);
            return RedirectToRoute("Subcategories");
        }

        public ActionResult DeleteSubcategory(int idSubcategory)
        {
            SqlHandler.DeleteSubcategory(idSubcategory);
            return RedirectToRoute("Subcategories");
        }
    }
}