using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class Country
    {
        public int IDCountry { get; set; }
        public string CountryName { get; set; }

        public override string ToString() => $"{CountryName}";
    }
}