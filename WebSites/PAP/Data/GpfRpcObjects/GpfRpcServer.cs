using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GA.BDC.Data;
using GA.BDC.PAP.Data.SearchFilters;

namespace GA.BDC.PAP.Data.GpfRpcObjects
{
   [Serializable]
   internal class GpfRpcServer
   {

      public GpfRpcServer(SearchFilter sf)
      {
         C = "Pap_Merchants_Campaign_Commissions";
         M = "loadCommissionTypes";
         campaignid = sf.FilterValue;

      }
          
     /* public GpfRpcServer(CommissionFilter cf)
      {
         C = "Pap_Merchants_Campaign_Commissions";
         M = "loadCommissionTypes";
         campaignid = cf.FilterValue;
        
      }*/
    
      public string C { get; set; }
      public string M { get; set; }
      public string campaignid { get; set; }
   }
}
