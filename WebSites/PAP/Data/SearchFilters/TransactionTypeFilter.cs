using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.PAP.Data.SearchFilters
{
   public class TransactionTypeFilter : SearchFilter
   {
      public TransactionTypeFilter(string input)
      {
         FilterValue = input;
         FilterType = "S".ToString();
      }
   }
}
