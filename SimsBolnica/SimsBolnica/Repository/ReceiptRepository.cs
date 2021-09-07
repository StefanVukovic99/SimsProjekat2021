// File:    ReceiptRepository.cs
// Author:  User
// Created: Thursday, June 17, 2021 4:58:15 PM
// Purpose: Definition of Class ReceiptRepository

using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class ReceiptRepository
   {

        public ReceiptRepository()
        {
            fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\SimsBolnica\\data\\receipts.json";
            ReadJson();
        }

        public List<Receipt> GetPatientReceipts(string jmbg)
      {
            ReadJson();
            return objects.FindAll(obj => obj.PatientJMBG == jmbg);
      }

        public void ReadJson()
        {
            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Close();
            }

            string json;

            using (StreamReader r = new StreamReader(fileLocation))
            {
                json = r.ReadToEnd();
            }

            
            if (json != "")
            {
                objects = JsonConvert.DeserializeObject<List<Receipt>>(json);
            }
        }
      
      public void WriteJson()
      {
            string json = JsonConvert.SerializeObject(objects, Formatting.Indented);
            System.Diagnostics.Debug.WriteLine(json);
            File.WriteAllText(fileLocation, json);
        }
      
      public void CreateReceipt(Receipt receipt)
      {
            objects.Add(receipt);
            WriteJson();
        }
   
      private string fileLocation;
      private List<Receipt> objects;
   
   }
}