using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class Customer
    {
        public int IDKupac { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public City Grad { get; set; }

        public Customer(int iDKupac, string ime, string prezime, string email, string telefon, City grad)
            : this(ime, prezime, email, telefon, grad)
        {
            IDKupac = iDKupac;
        }

        public Customer()
        {
        }

        public Customer(string ime, string prezime, string email, string telefon, City grad)
        {
            Ime = ime;
            Prezime = prezime;
            Email = email;
            Telefon = telefon;
            Grad = grad;
        }
    }
}