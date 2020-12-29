
namespace GA.BDC.PAP.Data.SearchFilters
{
   public class CampaignFilter:SearchFilter
   {
      public CampaignFilter()
      {
          FilterResult = "id";
      }
      public CampaignFilter(string input)
      {
         FilterValue = input;
         FilterResult = "id";
      }
   }
}
