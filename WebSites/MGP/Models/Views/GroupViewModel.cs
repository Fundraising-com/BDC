using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.MGP.Models.Views
{
   public class GroupViewModel
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public double AmountRaised { get; set; }
      public double Goal { get; set; }
      public double NumberOfMembers { get; set; }
      public DateTime? EventDate { get; set; }
      public string Image { get; set; }

      public int TotalEvents { get; set; }
      public int EventId { get; set; }

      public int PercentageAdvanced
      {
         get
         {
            if (Goal <= 0 && AmountRaised > 0)
            {
               return 100;
            }
            if (Goal > 0 && AmountRaised > 0)
            {
               return (int) (AmountRaised/Goal*100.0);
            }
            return 5;
         }
      }

      public string BootstrapClass
      {
         get { return PercentageAdvanced >= 90 ? "success" : PercentageAdvanced >= 10 ? "info" : "warning"; }
      }
   }
}