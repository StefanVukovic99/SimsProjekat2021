// File:    ReceiptController.cs
// Author:  User
// Created: Saturday, June 12, 2021 12:00:20 PM
// Purpose: Definition of Class ReceiptController

using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class ReceiptController
   {

        private readonly ReceiptService receiptService;

        public ReceiptController(ReceiptService receiptService)
        {
            this.receiptService = receiptService;
        }

        public void CreateReceipt(Receipt receipt)
        {
            this.receiptService.CreateReceipt(receipt);
        }

        public List<Receipt> GetPatientReceipts(string jmbg)
      {
            return this.receiptService.GetPatientReceipts(jmbg);
      }

        public float getRecentSpending(string jmbg)
        {
            return this.receiptService.getRecentSpending(jmbg);
        }

    }
}