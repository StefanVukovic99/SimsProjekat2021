// File:    MedicineRepository.cs
// Author:  User
// Created: Thursday, June 17, 2021 4:58:15 PM
// Purpose: Definition of Class MedicineRepository

using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class MedicineRepository
   {

        public MedicineRepository()
        {
            fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\SimsBolnica\\data\\medicines.json";
            ReadJson();
        }
        public void UpdateMedicine(Medicine medicine)
        {
            objects[objects.FindIndex(obj => obj.Id == medicine.Id)] = medicine;
            WriteJson();
        }
        public List<Medicine> GetAllMedicines()
        {
            return objects;
        }
      
        public List<Medicine> GetMedicinesByStatus(bool status)
        {
            ReadJson();

            return objects.FindAll(obj => obj.Accepted == status && !obj.Deleted);
        }

        public List<Medicine> GetUndeletedMedicines()
        {
            ReadJson();

            return objects.FindAll(obj => !obj.Deleted);
        }

        public void CreateMedicine(Medicine medicine)
        {
            objects.Add(medicine);
            WriteJson();
        }

        public void ApproveMedicine(string medId)
        {
            objects[objects.FindIndex(obj => obj.Id == medId)].Accepted = true;
            WriteJson();
        }

        public void RejectMedicine(string medId)
        {
            objects[objects.FindIndex(obj => obj.Id == medId)].Accepted = false;
            WriteJson();
        }

        public void DeleteMedicine(string medId)
        {
            objects[objects.FindIndex(obj => obj.Id == medId)].Deleted = true;
            WriteJson();
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
                objects = JsonConvert.DeserializeObject<List<Medicine>>(json);
            }
        }
      
        public void WriteJson()
        {
            string json = JsonConvert.SerializeObject(objects, Formatting.Indented);
            File.WriteAllText(fileLocation, json);
        }
   
        private string fileLocation;
        private List<Medicine> objects;
      
    }
}