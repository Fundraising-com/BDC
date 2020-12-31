using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Data;
using BusinessServices;
using Microsoft.Practices.ServiceLocation;

namespace ActivityLibrary
{

    public sealed class GetMagazineClosestMatchingOfferActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> ProductCode { get; set; }
        public InArgument<Int32> ProductQty { get; set; }
        public InArgument<Double> ProductPrice { get; set; }
        public InArgument<string> ProductName { get; set; }
        public InArgument<Int32> CampaignID { get; set; }
        public InArgument<Int32> PriceOverride { get; set; }
        public OutArgument<PricingData> PricingDataResult { get; set; }


        protected override void Execute(CodeActivityContext context)
        {
            string _pcode = ProductCode.Get(context);
            int _qty = ProductQty.Get(context);
            double _price = ProductPrice.Get(context);
            int _campaignid = CampaignID.Get(context);
            int _poverride = PriceOverride.Get(context);


            ProductAndPricingService _pss = ServiceLocator.Current.GetInstance<ProductAndPricingService>();

            /* start with exact match */
            PricingData _pData = _pss.GetPricingData(_pcode, _qty, (decimal)_price, _campaignid, 46001);

            if (_pData == null)
            {
                // Try for quantity
                _pData = _pss.GetPricingData(_pcode, _qty, _campaignid, 46001);
                if (_pData == null)
                {
                    // try for price
                    _pData = _pss.GetPricingData(_pcode, (decimal)_price, _campaignid, 46001);
                    if (_pData == null)
                    {

                    }
                }
                else
                {
                    //Make sure it falls within the tolerance
                }

            }

            PricingDataResult.Set(context, _pData);
        }
    }
}
