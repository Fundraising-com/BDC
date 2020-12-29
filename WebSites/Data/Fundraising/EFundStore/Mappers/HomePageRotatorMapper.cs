using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
    public static class HomePageRotatorMapper
    {
        public static HomePageRotator Hydrate(homepagerotator homepagerotator)
        {
            var _homepagerotator = new HomePageRotator
            {
                Id = homepagerotator.id,
                Image = homepagerotator.image,
                Url = homepagerotator.url,
                PartnerId = homepagerotator.partner_id,
                AlternativeText = homepagerotator.alternative_text,
                IsActive = homepagerotator.is_active,
                SortOrder = homepagerotator.sort_order,
                Created = homepagerotator.created_on,
               
                
            };
            return _homepagerotator;
        }
    }
}