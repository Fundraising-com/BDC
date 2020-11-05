using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.EzFund.Templates.Emails
{
   public partial class KitRequestedEmailTemplate
   {
      public string Subject { get; set; }
      public Lead Lead { get; set; }
      public string MGPWizardStep2 { get; set; }
      public KitRequestedEmailTemplate(string subject, Lead lead)
      {
         Subject = subject;
         Lead = lead;
         MGPWizardStep2 = string.Concat(ConfigurationManager.AppSettings["KitConfirmAutocreate"], "AutoCreate.aspx?gID=",
                     Lead.Partner.Guid, "&lid=", Lead.Id, "&oName=", Lead.FirstName + Lead.LastName, "&gName=", Lead.Group,
                     "&oEmail=", Lead.Email, "&om=", -1, "&oy=", -1,
                     "&cc=44&rURL=~/CMWizard2.aspx&utm_source=fundraisingcom&utm_medium=banner&utm_campaign=KitRequest");
      }
   }
}