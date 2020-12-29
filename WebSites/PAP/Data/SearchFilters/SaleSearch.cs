using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.PAP.Data.SearchFilters
{
   public class SaleSearch:SearchFilter
   {
      public SaleSearch()
      {
         FilterValue = "Id".ToString();
         FilterResult = "value".ToString();
      }
   }
}
