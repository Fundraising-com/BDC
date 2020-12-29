namespace GA.BDC.PAP.Data.SearchFilters
{
   public class AffiliateFilter : SearchFilter
   {
      public AffiliateFilter()
      {
         FilterResult = "userid";
      }
      public AffiliateFilter(string input)
      {
         FilterValue = input;
         FilterResult = "userid";
      }
   }
}
