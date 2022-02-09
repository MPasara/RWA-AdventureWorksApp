using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class CreditCard
    {
        public int IDCreditCard { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
    }
}