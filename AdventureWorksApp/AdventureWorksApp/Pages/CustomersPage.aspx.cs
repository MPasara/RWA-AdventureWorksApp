using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.DAO;

namespace AdventureWorksApp.Pages
{
    public partial class CustomersPage : Page
    {
        bool isAscending = true; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["loginData"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack)
            {
                ShowCustomers();
                ShowCountries();
                ShowCitiesOfCountries(ddlCountry.SelectedValue);
                ShowCitiesOfCountries(ShowCountries());
            }
        }

        private void ShowCustomers()
        {
            try
            {
                gvCustomers.DataSource = SqlHandler.GetCustomers();
                gvCustomers.DataBind();
            }
            catch (Exception e)
            {
                lbInfo.Text = e.Message;
            }
        }

        private string ShowCountries()
        {
            ddlCountry.DataSource = SqlHandler.GetCountries();
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "IDCountry";
            ddlCountry.DataBind();
            return ddlCountry.SelectedValue;
        }

        private void ShowCitiesOfCountries(string countryId)
        {
            ddlCity.DataSource = SqlHandler.GetCities().FindAll(c => c.Country.IDCountry == int.Parse(countryId));
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "IDCity";
            ddlCity.DataBind();
        }

        protected void btnViewHistory_Click(object sender, EventArgs e){}

        protected void btn10_Click(object sender, EventArgs e)
        {
            gvCustomers.PageSize = 10;
            ShowCustomers();
        }

        protected void btn20_Click(object sender, EventArgs e)
        {
            gvCustomers.PageSize = 20;
            ShowCustomers();
        }

        protected void btn30_Click(object sender, EventArgs e)
        {
            gvCustomers.PageSize = 30;
            ShowCustomers();
        }

        protected void btn40_Click(object sender, EventArgs e)
        {
            gvCustomers.PageSize = 40;
            ShowCustomers();
        }

        protected void btn50_Click(object sender, EventArgs e)
        {
            gvCustomers.PageSize = 50;
            ShowCustomers();
        }

        protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomers.PageIndex = e.NewPageIndex;
            //gvCustomers.DataSource = SqlHandler.GetCustomers().Where(c => c.Grad.IDCity == int.Parse(ddlCity.SelectedValue)).ToList(); 
            gvCustomers.DataSource = SqlHandler.GetCustomers();
            gvCustomers.DataBind();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e) => ShowCitiesOfCountries(ddlCountry.SelectedValue);

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvCustomers.DataSource = SqlHandler.GetCustomers().Where(c => c.Grad.IDCity == int.Parse(ddlCity.SelectedValue)).ToList();
            gvCustomers.DataBind();
        }

        
        protected void gvCustomers_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortExpression == "Ime" && isAscending == true)
            {
                try
                {
                    gvCustomers.DataSource=SqlHandler.SortCustomersByFirstnameAsc(isAscending);
                    gvCustomers.DataBind();
                    isAscending = false;
                }
                catch (Exception ex)
                {
                    lbInfo.Text = ex.StackTrace;
                }
            }
            else if(e.SortExpression == "Ime" && isAscending==false)
            {
                try
                {
                    gvCustomers.DataSource = SqlHandler.SortCustomersByFirstnameDesc(isAscending);
                    gvCustomers.DataBind();
                    isAscending = true;
                }
                catch (Exception ex)
                {
                    lbInfo.Text = ex.Message;
                }
            }
            else if (e.SortExpression == "Prezime" && isAscending==true)
            {
                try
                {
                    gvCustomers.DataSource = SqlHandler.SortCustomersBySurnameAsc(isAscending);
                    gvCustomers.DataBind();
                    isAscending = false;
                }
                catch (Exception ex)
                {
                    lbInfo.Text = ex.Message;
                }
            }
            else if (e.SortExpression == "Prezime" && isAscending==false)
            {
                try
                {
                    gvCustomers.DataSource = SqlHandler.SortCustomersBySurnameDesc(isAscending);
                    gvCustomers.DataBind();
                    isAscending = true;
                }
                catch (Exception ex)
                {
                    lbInfo.Text = ex.Message;
                }
            }
        }

        protected void btnShowAllCustomers_Click(object sender, EventArgs e)
        {
            try
            {
                gvCustomers.DataSource = SqlHandler.GetCustomers();
                gvCustomers.DataBind();
            }
            catch (Exception ex)
            {
                lbInfo.Text = ex.Message;
            }
        }

        //View history link button
        protected void Unnamed_Command(object sender, CommandEventArgs e)
        {
            var page = HttpContext.Current.Handler as Page;
            Response.Redirect(page.GetRouteUrl("History", new { customerId = e.CommandArgument }));
        }
    }
}