using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class Bill
    {
        public int IDBill { get; set; }
        public string BillNumber { get; set; }
        public DateTime Date { get; set; }
        public Comercialist Comercialist { get; set; }
        public int CustomerID { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}