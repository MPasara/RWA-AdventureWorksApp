using AdventureWorksApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.DAO;

namespace AdventureWorksApp.Pages
{
    public partial class RegisterPage : Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["loginData"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            User newUser = new User();

            newUser.Firstname = tbFirstname.Text;
            newUser.Surname = tbSurname.Text;
            newUser.Username = tbUsername.Text;
            newUser.Password = tbPassword.Text;

            try
            {
                SqlHandler.CreateUser(newUser);
            }
            catch (Exception ex)
            {
                lbInfo.Text = ex.StackTrace;
            }

            Response.Redirect("CustomersPage.aspx");
        }
    }
}