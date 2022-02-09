using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class Product
    {
        public int IDProduct { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public int MinimalAmountInStorage { get; set; }
        public double Price { get; set; }
        public Subcategory Subcategory { get; set; }
        public int SubcategoryID { get; set; }
    }
}