using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;
using System;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
    public static class NewsletterMapper
    {

        public static Newsletter Hydrate(newsletter newsletter)
        {
            var _newsletter = new Newsletter
            {
                Id = newsletter.id,
                Title = newsletter.title,
                Url = newsletter.url,
                Body = newsletter.body,
                Created_on = newsletter.created_on,
                Enabled = newsletter.enabled,
                Author = "HTMLNews",
                UpdatedOn = newsletter.updated_on == null ? DateTime.Now : (DateTime)newsletter.updated_on,
                Partner = newsletter.partner == null ? 686 : (int)newsletter.partner,
                DisplayOrder = newsletter.display_order

            };
            return _newsletter;
        }


    }
}
