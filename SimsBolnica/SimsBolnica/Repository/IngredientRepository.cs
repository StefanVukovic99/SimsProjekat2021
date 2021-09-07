// File:    IngredientRepository.cs
// Author:  User
// Created: Thursday, June 17, 2021 4:58:15 PM
// Purpose: Definition of Class IngredientRepository

using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class IngredientRepository
   {

        public IngredientRepository()
        {
            fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\SimsBolnica\\data\\ingredients.json";
            ReadJson();
        }

        public List<Ingredient> GetAllIngredients()
      {
            return objects;
      }
      
      public void ReadJson()
      {
            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Close();
            }

            StreamReader r = new StreamReader(fileLocation);

            string json = r.ReadToEnd();
            if (json != "")
            {
                objects = JsonConvert.DeserializeObject<List<Ingredient>>(json);
            }
        }

      private string fileLocation;
      private List<Ingredient> objects;
   
   }
}