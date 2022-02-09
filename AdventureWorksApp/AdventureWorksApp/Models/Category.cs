using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class Category
    {
        public int IDCategory { get; set; }
        public string CategoryName { get; set; }

        public override string ToString() => $"{CategoryName}";
    }
}