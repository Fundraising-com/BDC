using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.Fundraising.Core.Templates.Emails
{
   public partial class SaleEmailTemplate
   {
      public string Subject { get; set; }
      public IList<Sale> Sales { get; set; }
      public Consultant Consultant { get; set; }
      public Client Client { get; set; }
      public Lead Lead { get; set; }
      public string ExtraMessage { get; set; }
      public SaleEmailTemplate(string subject, IList<Sale> sales, string extraMessage = "")
      {
         if (!sales.Any())
         {
            throw new Exception("You must provide at least one Sale");
         }
         Subject = subject;
         Sales = sales;
         Consultant = sales[0].Consultant;
         Client = sales[0].Client;
         Lead = Client.Lead;
         ExtraMessage = extraMessage;
      }
   }
}