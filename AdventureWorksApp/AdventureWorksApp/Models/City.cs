using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class City
    {
        public int IDCity { get; set; }
        public string CityName { get; set; }
        public Country Country { get; set; }

        public override string ToString() => $"{CityName}";
    }
}