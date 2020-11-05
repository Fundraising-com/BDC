using System;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Reports.ViewModels
{
   public class SpiderViewModel
   {
      public int? SaleId { get; set; }
      public int? LeadId { get; set; }
      public int? PromotionId { get; set; }
      public int? PartnerId { get; set; }
      public int? ScratchbookId { get; set; }
      public string Email { get; set; }
      public string ZipCode { get; set; }
      public string DayPhone { get; set; }
      public string EveningPhone { get; set; }
      public decimal? TotalAmount { get; set; }
      public DateTime? ShipDateStart { get; set; }
      public DateTime? ShipDateEnd { get; set; }
      public DateTime? FundraiserStart { get; set; }
      public DateTime? FundraiserEnd { get; set; }
      public DateTime? SalesConfirmStart { get; set; }
      public DateTime? SalesConfirmEnd { get; set; }
      public DateTime? LeadsEntryStart { get; set; }
      public DateTime? LeadsEntryEnd { get; set; }
      public Country Country { get; set; }
      public Region Region { get; set; }
      public Consultant Consultant { get; set; }
      public OrganizationType OrganizationType { get; set; }
      public ProductClass ProductClass { get; set; }
      public GroupType GroupType { get; set; }
    }
}