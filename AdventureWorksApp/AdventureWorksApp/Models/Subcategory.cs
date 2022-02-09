using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class Subcategory
    {
        public int IDSubcategory { get; set; }
        public string SubcategoryName { get; set; }
        public Category Category { get; set; }
        public int CategoryID { get; set; }

        public override string ToString() => $"{SubcategoryName}";
    }
}