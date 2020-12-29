using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
    public static class AdvertisingMapper
    {
        public static AdvertiserProducts Hydrate(advertiser_products row)
        {
            return new AdvertiserProducts
            {
                Id = row.Id,
                product_id = row.product_id,
                partner_id = row.partner_id,
                phone_number = row.phone_number,
                kit_button_custom_url = row.kit_button_custom_url,
                add_to_cart_custom_url = row.add_to_cart_custom_url,
                IsEnabled = row.enabled,
                create_date = row.create_date
            };
        }


    }
}
