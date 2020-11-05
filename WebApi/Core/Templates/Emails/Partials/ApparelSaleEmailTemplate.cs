using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.Fundraising.Core.Templates.Emails
{
   public partial class ApparelSaleEmailTemplate
    {
      public string Subject { get; set; }
        private Sale sales;
        public Consultant Consultant { get; set; }
      public Client Client { get; set; }
      public Lead Lead { get; set; }
      public string ExtraMessage { get; set; }
      public ApparelSaleEmailTemplate(string subject, Sale sales, string extraMessage = "")
      {
        
         Subject = subject;
            this.sales = sales;
            Consultant = sales.Consultant;
         Client = sales.Client;
         Lead = Client.Lead;
         ExtraMessage = extraMessage;
      }
   }
}