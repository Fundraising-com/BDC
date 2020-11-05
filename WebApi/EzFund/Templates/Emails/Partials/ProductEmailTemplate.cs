using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.EzFund.Templates.Emails
{
   public partial class ProductEmailTemplate
   {
      public string Subject { get; set; }
      public Product Product { get; set; }
      public string ExtraMessage { get; set; }
      public bool ShowApprovedReviews { get; set; }
      public ProductEmailTemplate(string subject, Product product, bool showApprovedReviews, string extraMessage = "")
      {
         Subject = subject;
         Product = product;
         ExtraMessage = extraMessage;
         ShowApprovedReviews = showApprovedReviews;
      }
   }
}