// File:    User.cs
// Author:  User
// Created: Saturday, June 12, 2021 11:38:03 AM
// Purpose: Definition of Class User

using System;

namespace Model
{
   public class User
   {
        private string jmbg;
        private string email;
      public string password;
      private string firstName;
      private string lastName;
      private string phone;
      private UserType userType;

        public UserType UserType { get => userType; set => userType = value; }
        public string Jmbg { get => jmbg; set => jmbg = value; }
        public string Email { get => email; set => email = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Phone { get => phone; set => phone = value; }

        public User(string jmbg, string email, string firstName, string lastName, string phone, UserType userType)
        {
            this.jmbg = jmbg;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.userType = userType;
        }

        public User()
        {
        }
    }
}