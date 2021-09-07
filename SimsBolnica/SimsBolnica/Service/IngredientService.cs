// File:    IngredientService.cs
// Author:  User
// Created: Thursday, June 17, 2021 4:56:50 PM
// Purpose: Definition of Class IngredientService

using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
   public class IngredientService
   {
        public IngredientService(IngredientRepository ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public List<Ingredient> GetAllIngredients()
        {
            return this.ingredientRepository.GetAllIngredients();
        }
   
        private IngredientRepository ingredientRepository;
      
   }
}