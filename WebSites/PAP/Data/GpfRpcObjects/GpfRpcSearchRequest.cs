using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GA.BDC.PAP.Data.SearchFilters;

namespace GA.BDC.PAP.Data.GpfRpcObjects
{
   [Serializable]
   internal class GpfRpcSearchRequest
   {
       public GpfRpcSearchRequest(CommissionFilter filter)
      {
         C = "Pap_Merchants_Campaign_CampaignForm";
         M = "load";
         sort_col = "dateinserted";
         sort_asc = false;
         offset = 0;
         limit = 1;
         filters = new List<String[]> {  new String[] {"name", "IN", filter.FilterValue } };
         columns = new List<String[]> {  new String[] {"id"},   
                                         new String[] {"rstatus"}, 
                                         new String[] {"rstatus"}, 
                                         new String[] {"dateinserted"}
                                      };

      }
      public GpfRpcSearchRequest(CampaignFilter filter)
      {
         C = "Pap_Merchants_Campaign_CampaignsGrid";
         M = "getRows";
         sort_col = "dateinserted";
         sort_asc = false;
         offset = 0;
         limit = 1;
         filters = new List<String[]> {  new String[] {"name", "IN", filter.FilterValue } };
         columns = new List<String[]> {  new String[] {"id"},   
                                         new String[] {"rstatus"}, 
                                         new String[] {"rstatus"}, 
                                         new String[] {"dateinserted"}
                                      };
    
      }

      public GpfRpcSearchRequest(AffiliateFilter filter)
      {
         C = "Pap_Merchants_User_AffiliatesGridSimple";
         M = "getRows";
         sort_col = "dateinserted";
         sort_asc = false;
         offset = 0;
         limit = 1;
         filters = new List<String[]> {  new String[] {"refid", "IN", filter.FilterValue } };
         columns = new List<String[]> {  new String[] {"id"}, 
                                         new String[] {"refid"}, 
                                         new String[] {"userid"},  
                                         new String[] {"rstatus"}, 
                                         new String[] {"dateinserted"}
                                      };

      }


        public GpfRpcSearchRequest(TransactionTypeFilter filter)
        {
            C = "Pap_Merchants_Transaction_TransactionsGrid";
            M = "getRows";
            sort_col = "dateinserted";
            sort_asc = false;
            offset = 150;
            filters = new List<String[]> { new String[] { "rstatus", "IN", filter.FilterValue },
                                          new String[] { "rtype", "IN", filter.FilterType }
                                         };

            columns = new List<String[]> {  new String[] {"id"},
                                            new String[] {"userid"},
                                            new String[] {"t_orderid"},
                                            new String[] {"name"},
                                         
                                            new String[] {"rstatus"},
                                            new String[] {"dateinserted"}
                                        };

        }


        public GpfRpcSearchRequest(BannerFilter filter)
      {
         C = "Pap_Merchants_Banner_BannersGrid";
         M = "getRows";
         sort_col = "name";
         sort_asc = true;
         offset = 0;
         limit = 1;
         filters = new List<String[]> { new String[] { "search", "L", filter.FilterValue } };
         columns = new List<String[]> {  new String[] {"id"}, 
                                         new String[] {"banner"}, 
                                         new String[] {"rtype"},  
                                         new String[] {"destinationurl"}, 
                                         new String[] {"rorder"}, 
                                         new String[] {"actions"}
                                      };

      }


      public string C {get; set;}
      public string M {get; set;}
      public string sort_col {get; set;}
      public bool sort_asc {get; set;}
      public int offset {get; set;}
      public int limit {get; set;}
      public List<String[]> filters { get; set; }
      public List<String[]> columns { get; set; }

   }

   
}
