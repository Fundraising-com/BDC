using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
    public static class BannerMapper
    {
        public static Banner Hydrate(banner banner)
        {
            var _banner = new Banner
            {
                Id = banner.id,
                AlternativeText = banner.alternative_text,
                Created = banner.created_on,
                Url = banner.url,
                Image = banner.image,
                IsActive = banner.is_active,
                SortOrder = banner.sort_order
            };
            return _banner;
        }
    }
}
