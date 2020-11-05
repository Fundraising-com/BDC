using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.Fundraising.Core.Templates.Emails
{
   public partial class OrderConfirmationEmailTemplate
   {
      public string Subject { get; set; }
      public IList<Sale> Sales { get; set; }
     

        public OrderConfirmationEmailTemplate(string subject, IList<Sale> sales)
      {
            Sales = sales;
            Subject = subject;
           
        }
    }
}