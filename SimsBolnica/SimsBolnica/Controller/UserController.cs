// File:    UserController.cs
// Author:  User
// Created: Saturday, June 12, 2021 11:40:09 AM
// Purpose: Definition of Class UserController

using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class UserController
   {

        private UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        public bool? login(string jmbg, string password)
      {
            return this.userService.login(jmbg, password);
      }

        public UserType getActiveUserType()
        {
            return this.userService.getActiveUserType();
        }

        public string getActiveUserJMBG()
        {
            return this.userService.getActiveUserJMBG();
        }

        public bool RegisterPatient(User patient)
      {
            return this.userService.RegisterPatient(patient);
      }
      
      public List<User> GetAllPatients()
      {
            return this.userService.GetAllPatients();
      }
   }
}