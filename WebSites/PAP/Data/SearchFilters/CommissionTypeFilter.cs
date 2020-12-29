using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.PAP.Data.SearchFilters
{
   public class CommissionTypeFilter:SearchFilter
   {
      public CommissionTypeFilter()
      {
         FilterResult = "commtypeid".ToString();
      }
   }
}
