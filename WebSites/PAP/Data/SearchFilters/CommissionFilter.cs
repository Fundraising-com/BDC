namespace GA.BDC.PAP.Data.SearchFilters
{
   public class CommissionFilter:SearchFilter
   {
      public CommissionFilter(string input)
      {
         FilterValue = input;
         FilterResult = "rtype";
         FilterType = "S";
      }
   }
}
