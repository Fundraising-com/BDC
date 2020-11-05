using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.EzFund.Templates.Emails
{
   public partial class LeadEmailTemplate
   {
      public string Subject { get; private set; }
      public string Notes { get; private set; }
      public string ExtraMessage { get; set; }
      public ProsNmadTbl Lead { get; private set; }
      public Promotion Promotion { get; private set; }
      public Partner Partner { get; private set; }
      public Consultant Consultant { get; private set; }
      public LeadEmailTemplate(string subject, ProsNmadTbl lead, string notes = "", string extraMessage = "") // Force checkin
      {
         Subject = subject;
         Lead = lead;
        
         Notes = notes;
         ExtraMessage = extraMessage;
         //#region Review Empty Strings
         Lead.Addr1Txt = Lead.Addr1Txt ?? "N/A";
         Lead.CityNme = Lead.CityNme ?? "N/A";
         Lead.ZipCde = Lead.ZipCde ?? "N/A";
         Lead.SlsStrtTxt = Lead.SlsStrtTxt ?? "N/A";
         Lead.Ph1Nbr = Lead.Ph1Nbr ?? "N/A";
         Lead.OrgNme = Lead.OrgNme ?? "N/A";
         Lead.OrgMembQtyTxt = Lead.OrgMembQtyTxt ?? "N/A";
         Lead.SlsStrtTxt = Lead.SlsStrtTxt ?? "N/A";
         Lead.TargPrftAmtTxt = Lead.TargPrftAmtTxt ?? "N/A";
         Lead.CmntTxt = Lead.CmntTxt ?? "N/A";
         Lead.ReferralUrl = Lead.ReferralUrl ?? "N/A";

            //#endregion
        }
    }
   
}