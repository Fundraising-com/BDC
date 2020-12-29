using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.PAP.Data.SearchFilters
{
   abstract public class SearchFilter
   {
     
      public string FilterValue { get; protected set; }
      public string FilterResult  { get; protected set; }
      public string FilterType { get; protected set; }

   }
}
