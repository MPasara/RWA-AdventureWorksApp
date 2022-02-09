using AdventureWorksApp.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;

namespace WebForms.DAO
{
    public class SqlHandler
    {
        private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private static List<City> cities = new List<City>();


        internal static int AddProduct(Product p)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "createProizvod";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Naziv", p.ProductName);
                    cmd.Parameters.AddWithValue("BrojProizvoda", p.ProductNumber);
                    cmd.Parameters.AddWithValue("Boja", p.Color);
                    cmd.Parameters.AddWithValue("MinimalnaKolicinaNaSkladistu", p.MinimalAmountInStorage);
                    cmd.Parameters.AddWithValue("CijenaBezPDV", p.Price);
                    cmd.Parameters.AddWithValue("PotkategorijaID", p.SubcategoryID);
                    SqlParameter idProduct = new SqlParameter("@IDProizvod", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idProduct);
                    cmd.ExecuteNonQuery();
                    return (int)(idProduct.Value);
                }
            }
        }


        internal static int AddCategory(Category c)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "createKategorija";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Naziv", c.CategoryName);
                    SqlParameter idCategory = new SqlParameter("@IDKategorija", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idCategory);
                    cmd.ExecuteNonQuery();
                    return (int)(idCategory.Value);
                }
            }
        }

        internal static int AddSubcategory(Subcategory s)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "createPotkategorija";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("KategorijaID", s.CategoryID);
                    cmd.Parameters.AddWithValue("Naziv", s.SubcategoryName);
                    SqlParameter idSubcategory = new SqlParameter("@IDPotkategorija", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idSubcategory);
                    cmd.ExecuteNonQuery();
                    return (int)(idSubcategory.Value);
                }
            }
        }

        internal static void EditCategory(int idCategory,string naziv)
        {
            SqlHelper.ExecuteDataset(cs, "updateKategorija", idCategory, naziv);
        }

        internal static Customer GetCustomer(int customerId)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "selectKupac", customerId);
            var row = ds.Tables[0].Rows[0];

            return new Customer()
            {
                IDKupac = customerId,
                Ime = row["Ime"].ToString(),
                Prezime = row["Prezime"].ToString(),
                Email = row["Email"].ToString(),
                Telefon = row["Telefon"].ToString(),
                Grad = cities.Find(c => c.IDCity == (int)row["GradID"])
            };
        }

        internal static List<Bill> GetBills(int customerId)
        {
            List<Bill> bills = new List<Bill>();
            DataSet ds = SqlHelper.ExecuteDataset(cs, "getRacuniOdKupac", customerId);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Bill bill = new Bill
                {
                    IDBill = (int)row["IDRacun"],
                    Date = DateTime.Parse(row["DatumIzdavanja"].ToString()),
                    BillNumber = row["BrojRacuna"].ToString(),
                    CustomerID = (int)row["KupacID"]
                };
                bills.Add(bill);
            }

            return bills;
        }

        internal static int DeleteProduct(int idProduct)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "deleteProizvod";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IDProizvod", idProduct);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        internal static int DeleteCategory(int idCategory)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "deleteKategorija";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IDKategorija", idCategory);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        internal static int DeleteSubcategory(int idSubcategory)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "deletePotkategorija";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IDPotkategorija", idSubcategory);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        internal static IEnumerable<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            DataSet ds = SqlHelper.ExecuteDataset(cs, "selectKategorije");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                categories.Add(new Category
                {
                    IDCategory = (int)row["IDKategorija"],
                    CategoryName = row["Naziv"].ToString()
                });
            }

            return categories;
        }

        internal static IEnumerable<Subcategory> GetSubcategories()
        {
            List<Subcategory> subcategories = new List<Subcategory>();

            DataSet ds = SqlHelper.ExecuteDataset(cs, "selectPotkategorije");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                subcategories.Add(new Subcategory
                {
                    IDSubcategory = (int)row["IDPotkategorija"],
                    SubcategoryName = row["Naziv"].ToString(),
                    Category = GetCategory((int)row["KategorijaID"])
                });
            }

            return subcategories;
        }

        internal static Product GetProductById(int? productId)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "selectProizvod", productId);
            DataRow row = ds.Tables[0].Rows[0];

            return new Product()
            {
                IDProduct = (int)row["IDProizvod"],
                Color = row["Boja"].ToString(),
                ProductName = row["Naziv"].ToString(),
                MinimalAmountInStorage = Convert.ToInt32(row["MinimalnaKolicinaNaSkladistu"]),
                Price = (double)row["CijenaBezPDV"],
                ProductNumber = row["BrojProizvoda"].ToString(),
                Subcategory = GetSubcategory((int)row["PotkategorijaID"])
            };
        }

        private static Subcategory GetSubcategory(int id)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "selectPotkategorija", id);
            DataRow row = ds.Tables[0].Rows[0];

            return new Subcategory() { IDSubcategory = (int)row["IDPotkategorija"], SubcategoryName = row["Naziv"].ToString(), Category = GetCategory((int)row["KategorijaID"]) };
        }

        private static Category GetCategory(int id)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "selectKategorija", id);
            DataRow row = ds.Tables[0].Rows[0];

            return new Category() { IDCategory = (int)row["IDKategorija"], CategoryName = row["Naziv"].ToString() };
        }

        private static List<Country> countries = new List<Country>();
        public static DataSet ds { get; set; }

        internal static int CreateUser(User user)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = nameof(CreateUser);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Firstname", user.Firstname);
                    command.Parameters.AddWithValue("Surname", user.Surname);
                    command.Parameters.AddWithValue("Username", user.Username);
                    command.Parameters.AddWithValue("Pwrd", user.Password);
                    SqlParameter idUser = new SqlParameter("@IDUser", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(idUser);
                    command.ExecuteNonQuery();
                    return (int)(idUser.Value);
                }
            }
        }

        internal static IEnumerable<City> GetCities(int countryId)
        {
            var tblCities = SqlHelper.ExecuteDataset(cs, "getCities", countryId).Tables[0];
            foreach (DataRow row in tblCities.Rows)
            {
                yield return new City
                {
                    IDCity = (int)row["IDGrad"],
                    CityName = row["Naziv"].ToString()
                };
            }
        }

        internal static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            DataSet ds = SqlHelper.ExecuteDataset(cs, "selectKupci");
            if (cities.Count == 0)
            {
                cities = GetCities();
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                customers.Add(
                        new Customer
                        {
                            IDKupac = (int)row["IDKupac"],
                            Ime = row["Ime"].ToString(),
                            Prezime = row["Prezime"].ToString(),
                            Email = row["Email"].ToString(),
                            Telefon = row["Telefon"].ToString(),
                            Grad = cities.Find(c => c.IDCity == (int)row["GradID"])
                        }
                    );
            }

            return customers;
        }

        internal static List<Customer> SortCustomersByFirstnameAsc(bool isAscending)
        {
            List<Customer> customers = new List<Customer>();

            DataSet ds = SqlHelper.ExecuteDataset(cs, nameof(SortCustomersByFirstnameAsc));

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                customers.Add(
                        new Customer
                        {
                            IDKupac = (int)row["IDKupac"],
                            Ime = row["Ime"].ToString(),
                            Prezime = row["Prezime"].ToString(),
                            Email = row["Email"].ToString(),
                            Telefon = row["Telefon"].ToString(),
                            Grad = cities.Find(c => c.IDCity == (int)row["GradID"])
                        }
                    );
            }

            return customers;
        }

        internal static List<Customer> SortCustomersByFirstnameDesc(bool isAscending)
        {
            List<Customer> customers = new List<Customer>();

            DataSet ds = SqlHelper.ExecuteDataset(cs, nameof(SortCustomersByFirstnameDesc));

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                customers.Add(
                        new Customer
                        {
                            IDKupac = (int)row["IDKupac"],
                            Ime = row["Ime"].ToString(),
                            Prezime = row["Prezime"].ToString(),
                            Email = row["Email"].ToString(),
                            Telefon = row["Telefon"].ToString(),
                            Grad = cities.Find(c => c.IDCity == (int)row["GradID"])
                        }
                    );
            }

            return customers;
        }

        internal static List<City> GetCities()
        {
            List<City> cities = new List<City>();

            DataSet ds = SqlHelper.ExecuteDataset(cs, "selectGradovi");

            if (countries.Count == 0)
            {
                countries = GetCountries();
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                cities.Add(new City
                {
                    IDCity = (int)row["IDGrad"],
                    CityName = row["Naziv"].ToString(),
                    Country = countries.Find(c => c.IDCountry == (int)row["DrzavaID"])
                });
            }

            return cities;
        }

        internal static List<Customer> SortCustomersBySurnameDesc(bool isAscending)
        {
            List<Customer> customers = new List<Customer>();

            DataSet ds = SqlHelper.ExecuteDataset(cs, nameof(SortCustomersBySurnameDesc));

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                customers.Add(
                        new Customer
                        {
                            IDKupac = (int)row["IDKupac"],
                            Ime = row["Ime"].ToString(),
                            Prezime = row["Prezime"].ToString(),
                            Email = row["Email"].ToString(),
                            Telefon = row["Telefon"].ToString(),
                            Grad = cities.Find(c => c.IDCity == (int)row["GradID"])
                        }
                    );
            }

            return customers;
        }

        internal static List<Customer> SortCustomersBySurnameAsc(bool isAscending)
        {
            List<Customer> customers = new List<Customer>();

            DataSet ds = SqlHelper.ExecuteDataset(cs, nameof(SortCustomersBySurnameAsc));

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                customers.Add(
                        new Customer
                        {
                            IDKupac = (int)row["IDKupac"],
                            Ime = row["Ime"].ToString(),
                            Prezime = row["Prezime"].ToString(),
                            Email = row["Email"].ToString(),
                            Telefon = row["Telefon"].ToString(),
                            Grad = cities.Find(c => c.IDCity == (int)row["GradID"])
                        }
                    );
            }

            return customers;
        }

        internal static List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();

            DataSet ds = SqlHelper.ExecuteDataset(cs, "selectDrzavas");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                countries.Add(new Country
                {
                    IDCountry = (int)row["IDDrzava"],
                    CountryName = row["Naziv"].ToString()
                });
            }

            return countries;
        }

        internal static void UpdateCustomer(Customer customer)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "updateKupac";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IDKupac", customer.IDKupac);
                    cmd.Parameters.AddWithValue("Ime", customer.Ime);
                    cmd.Parameters.AddWithValue("Prezime", customer.Prezime);
                    cmd.Parameters.AddWithValue("Email", customer.Email);
                    cmd.Parameters.AddWithValue("Telefon", customer.Telefon);
                    cmd.Parameters.AddWithValue("GradID", customer.Grad.IDCity);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static int DeleteCustomer(Customer customer)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "deleteKupac";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IDKupac", customer.IDKupac);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        internal static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            DataSet ds = SqlHelper.ExecuteDataset(cs, "selectProizvodi");
            Subcategory noSubcategory = new Subcategory { IDSubcategory = 0, SubcategoryName = "-" };

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Product product = new Product();

                product.IDProduct = (int)row["IDProizvod"];
                product.ProductName = row["Naziv"].ToString();
                product.ProductNumber = row["BrojProizvoda"].ToString();
                product.Color = row["Boja"].ToString();
                product.MinimalAmountInStorage = Convert.ToInt16(row["MinimalnaKolicinaNaSkladistu"]);
                product.Price = (double)Convert.ToDecimal(row["CijenaBezPDV"]);
                product.SubcategoryID = (int)row["PotkategorijaID"];

                string subcategoryId = row["PotkategorijaID"].ToString();

                product.Subcategory = subcategoryId == "" ? noSubcategory : GetSubcategory(int.Parse(subcategoryId));
                products.Add(product);
            }

            return products;
        }
    }
}