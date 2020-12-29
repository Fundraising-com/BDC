using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GA.BDC.PAP.Data.SearchFilters;

namespace GA.BDC.PAP.Data.GpfRpcObjects
{
   [Serializable]
   internal class GpfRpcCommissionCalculationRequest
   {
      public GpfRpcCommissionCalculationRequest(GpfRpcFormRespond respond)
      {
         C = "Pap_Merchants_Campaign_CampaignForm";
         M = "computeAutomaticCommissionRpc";
         campaignid = respond.GetRowValue((new CampaignRowFilter()).FilterResult);
         userid = respond.GetRowValue((new AffiliateFilter()).FilterResult);
         type = respond.GetRowValue((new CommissionTypeFilter()).FilterResult);
         totalcost = respond.GetRowValue((new TotalCostFilter()).FilterResult);
         fixedcost = "".ToString();
         tier = "1".ToString();
         fields = new List<String[]> { 
                                       new String[]{ "name","value"},
                                       new String[]{ "commission",""},
                                    };
      }

      public GpfRpcCommissionCalculationRequest(string campaignId, string userId, string type, double totalCost)
      {
         this.C = "Pap_Merchants_Campaign_CampaignForm";
         this.M = "computeAutomaticCommissionRpc";
         this.campaignid = campaignId;
         this.userid = userId;
         this.type = type;
         this.totalcost = totalCost.ToString();
         this.fixedcost = "".ToString();
         this.tier = "1".ToString();
         this.fields = new List<String[]> { 
                                       new String[]{ "name","value"},
                                       new String[]{ "commission",""},
                                    };
      }
      public string C { get; set; }
      public string M { get; set; }
      public string campaignid { get; set; }
      public string userid { get; set; }
      public string type { get; set; }
      public string totalcost { get; set; }
      public string fixedcost { get; set; }
      public string tier { get; set; } 
      public List<String[]> fields { get; set; }
   }
}
