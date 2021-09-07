// File:    Receipt.cs
// Author:  User
// Created: Saturday, June 12, 2021 11:38:03 AM
// Purpose: Definition of Class Receipt

using System;
using System.Collections.Generic;

namespace Model
{
   public class Receipt
   {
      private int id;
      private string apothecary;
      private string dateAndTime;
      private Dictionary<string, int> medicines;
      private float total;
        private string patientJMBG;

        public int Id { get => id; set => id = value; }
        public string Apothecary { get => apothecary; set => apothecary = value; }
        public string DateAndTime { get => dateAndTime; set => dateAndTime = value; }
        public Dictionary<string, int> Medicines { get => medicines; set => medicines = value; }
        public float Total { get => total; set => total = value; }
        public string PatientJMBG { get => patientJMBG; set => patientJMBG = value; }

        public Receipt()
        {
        }

        public Receipt(int id, string apothecary, string dateAndTime, Dictionary<string, int> medicines, float total, string patientJMBG)
        {
            this.id = id;
            this.apothecary = apothecary;
            this.dateAndTime = dateAndTime;
            this.medicines = medicines;
            this.total = total;
            this.patientJMBG= patientJMBG;
        }
    }
}