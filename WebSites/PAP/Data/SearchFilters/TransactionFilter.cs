using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.PAP.Data.SearchFilters
{
   public class TransactionFilter : SearchFilter
   {
      public TransactionFilter()
      {
         FilterValue = "Id".ToString();
         FilterResult = "value".ToString();
      }
   }
}
