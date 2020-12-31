using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;

namespace BusinessServices
{
    public interface IProductAndPricingService  : IBusinessService
    {
        PricingData GetPricingData(string code, int term, int campaignId, int productType);
        PricingData GetPricingData(string code, int term, decimal dPrice, int campaignId, int productType);
        PricingData GetPricingData(string code, decimal dPrice, int campaignId, int productType);
        PricingData GetMagazineClosestMatchingOffer(string code, int term, decimal dPrice, int campaignId, int productType, double dTolerance, int originalpriceoverride, out int priceOverride);
        ProductCodeFromRemitCode GetProductCodeFromRemitCode(string remitcode, int term, int campaignId );
    }
}
