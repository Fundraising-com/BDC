using System;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Reports.ViewModels
{
   public class FcCommissionViewModel
    {
      
      public DateTime? start_date { get; set; }
      public DateTime? end_date { get; set; }
      public Consultant Consultant { get; set; }

    }
}