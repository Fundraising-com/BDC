namespace GA.BDC.PAP.Data.SearchFilters
{
   public class BannerFilter : SearchFilter
   {
      public BannerFilter(string input)
      {
         FilterValue = input;
         FilterResult = "bannerid";
      }
   }
}
