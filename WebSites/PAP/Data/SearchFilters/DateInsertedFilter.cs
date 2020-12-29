using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.PAP.Data.SearchFilters
{
  public  class DateInsertedFilter: SearchFilter
   {

      public DateInsertedFilter()
      {
         FilterResult = "dateinserted".ToString();
      }

      public DateInsertedFilter(string input)
      {
         FilterValue = input;
         FilterResult = "dateinserted".ToString();
      }
   }
}
