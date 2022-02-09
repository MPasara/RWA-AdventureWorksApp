using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class User
    {
        public int IDUser { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User() { }

        public User(int iDUser, string firstname, string surname, string username, string password)
            : this(firstname, surname, username, password)
        {
            IDUser = iDUser;
        }

        public User(string firstname, string surname, string username, string password)
        {
            Firstname = firstname;
            Surname = surname;
            Username = username;
            Password = password;
        }

        public override string ToString() => $"ID: {IDUser}, Firstname: {Firstname}, Surname: {Surname}";
    }
}