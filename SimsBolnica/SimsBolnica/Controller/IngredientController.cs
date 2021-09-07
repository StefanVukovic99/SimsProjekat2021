// File:    IngredientController.cs
// Author:  User
// Created: Saturday, June 12, 2021 12:00:17 PM
// Purpose: Definition of Class IngredientController

using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class IngredientController
   {


        private readonly IngredientService ingredientService;

        public IngredientController(IngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        public List<Ingredient> GetAllIngredients()
        {
            return this.ingredientService.GetAllIngredients();
        }
   }
}