// File:    UserService.cs
// Author:  User
// Created: Thursday, June 17, 2021 4:56:50 PM
// Purpose: Definition of Class UserService

using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
   public class UserService
   {
        private int loginAttempts;
        private User activeUser;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.activeUser = new User();

            loginAttempts = 0;
        }

        public UserType getActiveUserType()
        {
            return activeUser.UserType;
        }

        public string getActiveUserJMBG()
        {
            return activeUser.Jmbg;
        }

        public bool? login(string jmbg, string password)
        {
            activeUser = this.userRepository.GetAllUsers().Find(obj => obj.Jmbg == jmbg);

            if (activeUser == null | activeUser?.password != password) 
            {
                loginAttempts += 1;
                if (loginAttempts < 3) return null;
                return false;
            }
            else return true;

        }
     
      public bool RegisterPatient(User patient)
      {
            if (jmbgIsUnique(patient.Jmbg) && emailIsUnique(patient.Email))
            {
                 this.userRepository.RegisterPatient(patient);
                return true;
            }
            return false;
            
      }

        private bool jmbgIsUnique(string jmbg)
        {
            foreach(User user in GetAllPatients())
            {
                if (jmbg == user.Jmbg) return false;
            }
            return true;
        }

        private bool emailIsUnique(string email)
        {
            foreach (User user in GetAllPatients())
            {
                if (email == user.Email) return false;
            }
            return true;
        }


        public List<User> GetAllPatients()
      {
            return this.userRepository.GetAllPatients();
      }

        private UserRepository userRepository;
   
   }
}