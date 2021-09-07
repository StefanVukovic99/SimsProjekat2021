// File:    UserRepository.cs
// Author:  User
// Created: Thursday, June 17, 2021 4:58:15 PM
// Purpose: Definition of Class UserRepository

using Model;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace Repository
{
   public class UserRepository
   {

        public UserRepository()
        {
            fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\SimsBolnica\\data\\users.json";
            ReadJson();
        }

        public void RegisterPatient(User patient)
        {
            objects.Add(patient);
            WriteJson();
        }
      
        public List<User> GetAllPatients()
        {
            return objects.FindAll(obj => obj.UserType == UserType.patient);
        }

        private void ReadJson()
        {
            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Close();
            }

            StreamReader r = new StreamReader(fileLocation);

            string json = r.ReadToEnd();
            if (json != "")
            {
                objects = JsonConvert.DeserializeObject<List<User>>(json);
            }
        }

        public void WriteJson()
        {
            string json = JsonConvert.SerializeObject(objects, Formatting.Indented);
            System.Diagnostics.Debug.WriteLine(json);
            File.WriteAllText(fileLocation, json);
        }
      
        public List<User> GetAllUsers()
        {
            ReadJson();
            return objects;
        }

        private string fileLocation;
        private List<User> objects;
   
   }
}