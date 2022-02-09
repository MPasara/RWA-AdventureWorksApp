using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class Bill_Item
    {
        public int IDItem { get; set; }
        public int BillId { get; set; }
        public int Amount { get; set; }
        public Product Product { get; set; }
        public double PricePerPiece { get; set; }
        public double Discount { get; set; }
        public int TotalPrice { get; set; }
    }
}