// File:    Ingredient.cs
// Author:  User
// Created: Saturday, June 12, 2021 11:38:03 AM
// Purpose: Definition of Class Ingredient

using System;

namespace Model
{
   public class Ingredient
   {
      private string name;
      private string description;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
    }
}