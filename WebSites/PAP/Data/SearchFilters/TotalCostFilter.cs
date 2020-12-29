using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.PAP.Data.SearchFilters
{
   public class TotalCostFilter:SearchFilter
   {
      public TotalCostFilter()
      {
         FilterResult = "totalcost".ToString();
      }
   }
}
