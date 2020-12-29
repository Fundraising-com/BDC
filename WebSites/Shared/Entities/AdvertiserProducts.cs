using System;

namespace GA.BDC.Shared.Entities
{
    public class AdvertiserProducts
    {
        public int Id { get; set; }

        public int product_id { get; set; }

        public int partner_id { get; set; }

        public string phone_number { get; set; }

        public string kit_button_custom_url { get; set; }

        public string add_to_cart_custom_url { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime create_date { get; set; }

    }
}
