using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GA.BDC.Web.MGP.Models.Views
{
   public class AmountRaisedIndicator
   {
      public int ThermometerPercentage { get; set; }
      public int ThermometerFullIndexCount
      {
         get
         {
            if (ThermometerPercentage <= 0)
               return 0;
            else
            {
               int level = ThermometerPercentage / 10;
               if (level > 10)
                  return 10;
               else if (level < 1)
                  return 0;
               else
                  return (int)level;
            }
         }
      }
      public decimal Goal { get; set; }
      public string JoinHyperLinkUrl { get; set; }
      public bool IsGroupPageView { get; set; }
      public bool IsParticipantView { get; set; }
      public bool IsCampaignManagerView { get; set; }
      public int ParticipantId { get; set; }
      public bool HideJOIN { get; set; }
      public int ThermometerColumn { get; set; }
      public int GoalColumn { get; set; }
   }
}