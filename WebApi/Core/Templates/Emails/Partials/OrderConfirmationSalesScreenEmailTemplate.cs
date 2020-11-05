using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.Fundraising.Core.Templates.Emails
{
   public partial class OrderConfirmationSalesScreenEmailTemplate
    {
        private Sale sales;
       

        public string Subject { get; set; }
      public IList<Sale> Sales { get; set; }
     

       
        public OrderConfirmationSalesScreenEmailTemplate(string subject, Sale sales)
        {

            Subject = subject;
            this.sales = sales;
            
        }
    }
}