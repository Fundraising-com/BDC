using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.Fundraising.Core.Templates.Emails
{
   public partial class LeadEmailTemplate
   {
      public string Subject { get; private set; }
      public string Notes { get; private set; }
      public string ExtraMessage { get; set; }
      public Lead Lead { get; private set; }
      public Promotion Promotion { get; private set; }
      public Partner Partner { get; private set; }
      public Consultant Consultant { get; private set; }
      public LeadEmailTemplate(string subject, Lead lead, string notes = "", string extraMessage = "") // Force checkin
      {
         Subject = subject;
         Lead = lead;
         Promotion = Lead.Promotion;
         Partner = Lead.Partner;
         Consultant = Lead.Consultant;
         Notes = notes;
         ExtraMessage = extraMessage;
         #region Review Empty Strings
         Lead.Address.Address1 = Lead.Address.Address1 ?? string.Empty;
         Lead.Address.Address2 = Lead.Address.Address1 ?? string.Empty;
         Lead.Address.City = Lead.Address.City ?? string.Empty;
         Lead.Address.PostCode = Lead.Address.PostCode ?? string.Empty;
         Lead.Phone = Lead.Phone ?? string.Empty;
         Lead.FirstName = Lead.FirstName ?? string.Empty;
         Lead.LastName = Lead.LastName ?? string.Empty;
         Lead.Email = Lead.Email ?? string.Empty;
         Promotion.Name = Promotion.Name ?? string.Empty;
         Partner.Name = Partner.Name ?? string.Empty;
            Lead.Group = Lead.Group ?? string.Empty;
         #endregion
      }
   }
   
}