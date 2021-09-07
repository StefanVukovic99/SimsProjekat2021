// File:    Medicine.cs
// Author:  User
// Created: Saturday, June 12, 2021 11:38:03 AM
// Purpose: Definition of Class Medicine

using System;
using System.Collections.Generic;

namespace Model
{
   public class Medicine
   {
      private string id;
        private string name;
        private string manufacturer;
        private float price;
        private int amount;
      private List<Ingredient> ingredients;
        private bool accepted;
        private bool deleted;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        public float Price { get => price; set => price = value; }
        public int Amount { get => amount; set => amount = value; }
        public List<Ingredient> Ingredients { get => ingredients; set => ingredients = value; }
        public bool Accepted { get => accepted; set => accepted = value; }
        public bool Deleted { get => deleted; set => deleted = value; }

        public Medicine(string id, string name, string manufacturer, float price, int amount, List<Ingredient> ingredients, bool accepted, bool deleted)
        {
            this.id = id;
            this.name = name;
            this.manufacturer = manufacturer;
            this.price = price;
            this.amount = amount;
            this.ingredients = ingredients;
            this.accepted = accepted;
            this.deleted = deleted;
        }

        public Medicine()
        {
        }
    }
}