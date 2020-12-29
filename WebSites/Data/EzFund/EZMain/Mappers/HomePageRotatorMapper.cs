using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class HomePageRotatorMapper
    {
        public static HomePageRotator Hydrate(home_page_rotator homepagerotator)
        {
            var _homepagerotator = new HomePageRotator
            {
                Id = homepagerotator.id,
                Image = homepagerotator.image,
                Title = homepagerotator.title,
                SubTitle = homepagerotator.subtitle,
                Url = homepagerotator.url,
                CategoryUrl = homepagerotator.category_url,
                PartnerId = homepagerotator.partner_id,
                AlternativeText = homepagerotator.alternative_text,
                IsActive = homepagerotator.is_active,
                IsProduct = homepagerotator.is_product,
                SortOrder = homepagerotator.sort_order,
                Created = homepagerotator.created_on,
            };
            return _homepagerotator;
        }
    }
}