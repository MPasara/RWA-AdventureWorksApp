using AdventureWorksApp.Models;
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
    public partial class EditPage : Page
    {
        private static readonly string cs = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private static Customer customer;
        private static City city;
        private static Country country;
        private GridViewRow currentRow;
        private List<City> cities;


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
                ShowCitiesOfCountries();
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
                lbInfo.Text = e.StackTrace;
            }
        }

        private void ShowCountries()
        {
            ddlCountry.DataSource = SqlHandler.GetCountries();
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "IDCountry";
            ddlCountry.DataBind();
        }

        private void ShowCitiesOfCountries()
        {
            ddlCity.Items.Clear();
            cities = SqlHandler.GetCities().FindAll(c => c.Country.IDCountry == int.Parse(ddlCountry.SelectedValue));
            ddlCity.DataSource = cities;
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "IDCity";
            ddlCity.DataBind();
        }

        private void ShowSelectedCity(int countryId)
        {
            ddlCity.Items.Clear();
            cities = SqlHandler.GetCities().FindAll(c => c.Country.IDCountry == countryId);
            ddlCity.DataSource = cities;
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "IDCity";
            ddlCity.DataBind();
        }

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

        protected void btnEditCustomer_Click(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
            currentRow = gvCustomers.Rows[rowIndex];
            GetCustomer(currentRow.Cells[0].Text);
            ShowSelectedCity(country.IDCountry);
            FillFields();
        }

        private void GetCustomer(string idCustomer)
        {
            try
            {
                customer = new Customer();
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string procedureName = "getKupacDetails";

                    SqlCommand command = new SqlCommand(procedureName, con);
                    SqlParameter ID = new SqlParameter();
                    ID.ParameterName = "@IDKupac";
                    ID.SqlDbType = SqlDbType.Int;
                    ID.Value = int.Parse(idCustomer);

                    command.Parameters.Add(ID);

                    con.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        city = new City();
                        country = new Country();
                        while (dr.Read())
                        {
                            customer.IDKupac = dr.GetInt32(0);
                            customer.Ime = dr.GetString(1);
                            customer.Prezime = dr.GetString(2);
                            customer.Email = dr.GetString(3);
                            customer.Telefon = dr.GetString(4);
                            city.IDCity = dr.GetInt32(5);
                            city.CityName = dr.GetString(6);
                            country.IDCountry = dr.GetInt32(7);
                            country.CountryName = dr.GetString(8);
                            city.Country = country;
                            customer.Grad = city;
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                lbInfo.Text = e.StackTrace;
            }
        }

        private void FillFields()
        {
            try
            {
                tbFirstname.Text = customer.Ime;
                tbSurname.Text = customer.Prezime;
                tbEmail.Text = customer.Email;
                tbPhoneNumber.Text = customer.Telefon;
                ddlCountry.SelectedIndex = country.IDCountry - 1;
                ddlCity.SelectedIndex = cities.FindIndex(c => c.IDCity == city.IDCity);
            }
            catch (Exception e)
            {
                lbInfo.Text = e.Message;
            }
        }

        protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomers.PageIndex = e.NewPageIndex;
            gvCustomers.DataSource = SqlHandler.GetCustomers();
            gvCustomers.DataBind();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e) 
        {
            ShowCitiesOfCountries();
        } 

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e){}

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlHandler.UpdateCustomer(new Customer
                {
                    IDKupac = customer.IDKupac,
                    Ime = tbFirstname.Text,
                    Prezime = tbSurname.Text,
                    Email = tbEmail.Text,
                    Telefon = tbPhoneNumber.Text,
                    Grad = new City { IDCity = int.Parse(ddlCity.SelectedValue.ToString()) }
                });
                ShowCustomers();
                ClearForm();
            }
            catch (Exception ex)
            {
                lbInfo.Text = ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlHandler.DeleteCustomer(customer);
                ShowCustomers();
                lbInfo.Text = "Customer deleted";
            }
            catch (Exception ex)
            {
                lbInfo.Text = ex.Message;
            }
        }

        protected void btnClearForm_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            tbFirstname.Text = string.Empty;
            tbSurname.Text = string.Empty;
            tbEmail.Text = string.Empty;
            tbPhoneNumber.Text = string.Empty;
            lbInfo.Text = string.Empty;
        }
    }
}