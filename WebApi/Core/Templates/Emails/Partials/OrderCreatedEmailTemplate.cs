using System.Collections.Generic;
using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.Fundraising.Core.Templates.Emails
{
   public partial class OrderCreatedEmailTemplate
   {
      public IList<Sale> Sales { get; set; }
      public string Subject { get; set; }
      public OrderCreatedEmailTemplate(string subject, IList<Sale> sales)
      {
         Subject = subject;
         Sales = sales;
      }
   }
}