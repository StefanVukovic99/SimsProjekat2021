// File:    ReceiptService.cs
// Author:  User
// Created: Thursday, June 17, 2021 4:56:50 PM
// Purpose: Definition of Class ReceiptService

using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
   public class ReceiptService
   {

        private readonly ReceiptRepository receiptRepository;

        public ReceiptService(ReceiptRepository receiptRepository)
        {
            this.receiptRepository = receiptRepository;
        }

        public void CreateReceipt(Receipt receipt)
        {
            this.receiptRepository.CreateReceipt(receipt);
        }

        public float getRecentSpending(string jmbg)
        {
            float result = 0;
            foreach (Receipt receipt in this.receiptRepository.GetPatientReceipts(jmbg))
            {
                if(receipt.PatientJMBG == jmbg) result += receipt.Total;
            }
            return result;
        }

        public List<Receipt> GetPatientReceipts(string jmbg)
      {
            return this.receiptRepository.GetPatientReceipts(jmbg);
      }

   }
}