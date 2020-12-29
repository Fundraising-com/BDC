using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.PAP.Data.SearchFilters
{
   public class ActivationFilter : SearchFilter
   {
      public ActivationFilter(string input)
      {
         FilterValue = input;
         FilterResult = "rtype".ToString();
         FilterType = "A".ToString();
      }
   }
}
