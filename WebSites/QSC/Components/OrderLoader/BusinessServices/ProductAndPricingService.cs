using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using System.Data.Objects;
using Common;

namespace BusinessServices
{
    public class ProductAndPricingService : IProductAndPricingService
    {
        public PricingData GetPricingData(string code, int term, int campaignId, int productType)
        {
            PricingData _pData=null;
            using (QSPCanadaOrderManagementEntities _context = new QSPCanadaOrderManagementEntities())
            {

                ObjectResult<PricingData> results = _context.spGetCodeTermAndCampaign(code, term, campaignId, productType);

                _pData = results.FirstOrDefault();
            }
            return _pData;
        }


        public PricingData GetPricingData(string code, int term, decimal dPrice, int campaignId, int productType)
        {
            PricingData _pData = null;
            using (QSPCanadaOrderManagementEntities _context = new QSPCanadaOrderManagementEntities())
            {

                ObjectResult<PricingData> results = _context.GetCodeTermPriceAndCampaign(code, term, dPrice, campaignId, productType);

                _pData = results.FirstOrDefault();
            }
            return _pData;
        }


        public ProductCodeFromRemitCode GetProductCodeFromRemitCode(string remitcode, int term, int campaignId)
        {
            ProductCodeFromRemitCode pData = null;
            using(QSPCanadaOrderManagementEntities _context = new QSPCanadaOrderManagementEntities())
            {
                ObjectResult<ProductCodeFromRemitCode> results = _context.GetProductCodeFromRemitCodeAndTerm(remitcode, term, campaignId);

                pData = results.FirstOrDefault();
            }
            return pData;
        }


        public PricingData GetPricingData(string code, decimal dPrice, int campaignId, int productType)
        {
            PricingData _pData = null;
            using (QSPCanadaOrderManagementEntities _context = new QSPCanadaOrderManagementEntities())
            {

                ObjectResult<PricingData> results = _context.GetCodePriceAndCampaign(code, dPrice, campaignId, productType);

                _pData = results.FirstOrDefault();
            }
            return _pData;
        }


        public PricingData GetMagazineClosestMatchingOffer(string code, int term, decimal dPrice, int campaignId, int productType, double dTolerance, int originalpriceoverride, out int priceOverride)
        {
            /* start with exact match */
            PricingData _pData = null;
            priceOverride = originalpriceoverride;

            //If coupon just look for matching term otherwise try for exact match
            if (originalpriceoverride == (int)PriceOverride.Coupon)
                _pData = GetPricingData(code, term, campaignId, 46001);
            else
                _pData=GetPricingData(code, term, (decimal)dPrice, campaignId, 46001);

            priceOverride = (int)PriceOverride.None;

            if (_pData == null)
            {
                // First, assume price is correct and try to find a matching term
                _pData = GetPricingData(code, (decimal)dPrice, campaignId, 46001);

                if (_pData == null)
                {
                    // Still not valid -- Now assume the term is correct, and the price is a bit off
                    _pData = GetPricingData(code, term, campaignId, 46001);

                    if (_pData == null)
                    {
                        priceOverride = (int)PriceOverride.InvalidPrice;                    
                    }
                    else
                    {
                        //Make sure it falls within the tolerance
                        if ( Math.Abs( (double)dPrice - (double)(_pData.Price )) <= dTolerance )
                        {
                            priceOverride = (int)PriceOverride.ClosestMatchingOffer;
                        }
                        else
                            priceOverride = (int)PriceOverride.InvalidPrice;   

                    }
                }
                else
                    priceOverride = (int)PriceOverride.ClosestMatchingOffer;
            }

           
            return _pData;
        }
    }
}
