using AdventureWorksApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdventureWorksApp.Pages
{
    public partial class LoginPage : Page
    {
        private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private static User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["loginData"];
            if (Request.Cookies["loginData"] != null)
            {
                //Response.Redirect("CustomersPage.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text==string.Empty || tbPassword.Text==string.Empty)
            {
                ClearForm();
                lbInfo.ForeColor = Color.DarkRed;
                lbInfo.Text = "Username and Password are a must.";
            }
            if (ValidatePass())
            {
                HttpCookie cookie = new HttpCookie("loginData");
                cookie["username"] = tbUsername.Text;
                cookie["password"] = tbPassword.Text;
                cookie.Expires = DateTime.Now.AddMinutes(10);
                Response.Cookies.Add(cookie);//!
                Response.Redirect("CustomersPage.aspx");
            }
        }

        private bool ValidatePass()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string procedureName = "validateUser";
                    user = new User();
                    SqlCommand cmd = new SqlCommand(procedureName, con);
                    SqlParameter username = new SqlParameter();
                    SqlParameter pwrd = new SqlParameter();
                    username.ParameterName = "@Username";
                    pwrd.ParameterName = "@Pwrd";
                    username.SqlDbType = pwrd.SqlDbType = SqlDbType.NVarChar;
                    username.Value = tbUsername.Text;
                    pwrd.Value = tbPassword.Text;

                    cmd.Parameters.Add(username);
                    cmd.Parameters.Add(pwrd);

                    con.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user.IDUser = dr.GetInt32(0);
                            user.Firstname = dr.GetString(1);
                            user.Surname = dr.GetString(2);
                            user.Username = dr.GetString(3);
                            user.Password = dr.GetString(4);
                            return true;
                        }
                    }
                    else
                    {
                        ClearForm();
                        lbInfo.ForeColor = Color.DarkRed;
                        lbInfo.Text = "No such user, please register.";
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                ClearForm();
                lbInfo.Text = e.Message;
            }
            return false;
        }

        private void ClearForm()
        {
            tbUsername.Text = string.Empty;
            tbPassword.Text = string.Empty;
            lbInfo.Text = string.Empty;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterPage.aspx");
        }
    }
}