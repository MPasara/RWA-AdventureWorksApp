using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdventureWorksApp.Pages
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbProducts_Command(object sender, CommandEventArgs e)
        {
            var page = HttpContext.Current.Handler as Page;

            Response.Redirect(page.GetRouteUrl("Products", null));
        }

        protected void lbCategories_Command(object sender, CommandEventArgs e)
        {
            var page = HttpContext.Current.Handler as Page;

            Response.Redirect(page.GetRouteUrl("Categories", null));
        }

        protected void lbSubcategories_Command(object sender, CommandEventArgs e)
        {
            var page = HttpContext.Current.Handler as Page;

            Response.Redirect(page.GetRouteUrl("Subcategories", null));
        }
    }
}