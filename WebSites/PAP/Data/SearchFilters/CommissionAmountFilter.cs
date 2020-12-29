using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.PAP.Data.SearchFilters
{
   public class CommissionAmountFilter:SearchFilter
   {
      public CommissionAmountFilter()
      {
         FilterResult = "commission".ToString();
      }
   }
}
