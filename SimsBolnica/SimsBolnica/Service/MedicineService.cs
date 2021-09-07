// File:    MedicineService.cs
// Author:  User
// Created: Thursday, June 17, 2021 4:56:50 PM
// Purpose: Definition of Class MedicineService

using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
   public class MedicineService
    { 

        private readonly MedicineRepository medicineRepository;
        private int MAX_MEDS_PER_PURCHASE = 5;
        private int MAX_MEDS_PER_WEEK = 50;

    public MedicineService(MedicineRepository medicineRepository)
    {
        this.medicineRepository = medicineRepository;
    }

        public int GetMaxMedsPerPurchase()
        {
            return MAX_MEDS_PER_PURCHASE;
        }

        public int GetMaxMedsPerWeek()
        {
            return MAX_MEDS_PER_WEEK;
        }

        public List<Medicine> GetAllMedicines()
      {
            return this.medicineRepository.GetAllMedicines();
      }
      
      public List<Medicine> GetMedicinesByStatus(bool status)
      {
            return this.medicineRepository.GetMedicinesByStatus(status);
      }

        public List<Medicine> GetUndeletedMedicines()
        {
            return this.medicineRepository.GetUndeletedMedicines();
        }

        public void BuyMedicine(string id, int qtyTobuy)
        {
            Medicine med = this.medicineRepository.GetAllMedicines().Find(obj => obj.Id == id);
            med.Amount -= qtyTobuy;
            this.medicineRepository.UpdateMedicine(med);

        }

        public void ApproveMedicine(string medId)
      {
            this.medicineRepository.ApproveMedicine(medId);
        }
      
      public void DeleteMedicine(string medId)
      {
            this.medicineRepository.DeleteMedicine(medId);
      }
      
      public void RejectMedicine(string medId)
      {
            this.medicineRepository.RejectMedicine(medId);
        }
      
      public void CreateMedicine(Medicine medicine)
      {
            this.medicineRepository.CreateMedicine(medicine);
      }
   }
}