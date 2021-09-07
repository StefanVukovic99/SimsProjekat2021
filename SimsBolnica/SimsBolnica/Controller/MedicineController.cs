// File:    MedicineController.cs
// Author:  User
// Created: Saturday, June 12, 2021 11:51:07 AM
// Purpose: Definition of Class MedicineController

using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class MedicineController
   {
        private MedicineService medicineService;

        public MedicineController(MedicineService medicineService)
        {
            this.medicineService = medicineService;
        }

        public int GetMaxMedsPerPurchase()
        {
            return this.medicineService.GetMaxMedsPerPurchase();
        }

        public int GetMaxMedsPerWeek()
        {
            return this.medicineService.GetMaxMedsPerWeek();
        }

        public List<Medicine> GetAllMedicines()
      {
            return this.medicineService.GetAllMedicines();
      }
      
      public List<Medicine> GetMedicinesByStatus(bool status)
      {
            return this.medicineService.GetMedicinesByStatus(status);
      }

        public List<Medicine> GetUndeletedMedicines()
        {
            return this.medicineService.GetUndeletedMedicines();
        }


        public void ApproveMedicine(string medId)
      {
            this.medicineService.ApproveMedicine(medId);
        }
      
      public void DeleteMedicine(string medId)
      {
            this.medicineService.DeleteMedicine(medId);
      }
      
      public void RejectMedicine(string medId)
      {
            this.medicineService.RejectMedicine(medId);
        }
      
      public void BuyMedicine(string id, int qtyTobuy)
        {
            this.medicineService.BuyMedicine( id, qtyTobuy);
      }
      
        public bool searchMedByIngredients(Medicine med, string search_string, int i)
        {
            int i2;
            int i3;
            switch (search_string.Split(' ')[i])
            {
                case "&":
                    i2 = 2;
                    i3 = 1;

                    while(i3<=i2)
                    {
                        if (i + i3 >= search_string.Split(' ').Length) return true;
                        if(search_string.Split(' ')[i+i3] == "&" | search_string.Split(' ')[i + i3] == "|")
                        {
                            i2 += 2;
                        }
                        i3 += 1;
                    }

                    if (i + i2 >= search_string.Split(' ').Length) return true;
                    return searchMedByIngredients(med, search_string, i+1) & searchMedByIngredients(med, search_string, i+i2);
                case "|":
                    i2 = 2;
                    i3 = 1;
                    while (i3 <= i2)
                    {
                        if (i + i3 >= search_string.Split(' ').Length) return true;
                        if (search_string.Split(' ')[i + i3] == "&" | search_string.Split(' ')[i + i3] == "|")
                        {
                            i2 += 2;
                        }
                        i3 += 1;
                    }

                    if (i + i2 >= search_string.Split(' ').Length) return true;
                    return searchMedByIngredients(med, search_string, i + 1) | searchMedByIngredients(med, search_string, i + i2);
                default:
                    bool flag = false;
                    foreach(Ingredient ing in med.Ingredients)
                    {
                        if (ing.Name.ToLower().Contains(search_string.Split(' ')[i])) flag = true;
                    }
                    return flag;
            }
        }

      public void CreateMedicine(Medicine medicine)
      {
            this.medicineService.CreateMedicine(medicine);
      }
   }
}