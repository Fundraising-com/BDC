using System.Collections.Generic;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
    public static class CategoryMapper
    {
        /// <summary>
        /// Transforms the package into a Category
        /// </summary>
        /// <param name="package"></param>
        /// <param name="shippingFeeDetails"></param>
        /// <returns></returns>
        public static Category Hydrate(package package, IList<shipping_fee_detail> shippingFeeDetails)
        {
            var category = new Category
            {
                Id = package.package_id,
                Name = package.name,
                Order = package.order,
                Url = !string.IsNullOrEmpty(package.url) ? package.url.ToLowerInvariant() : string.Empty,
                ParentId = package.parent_package_id ?? 0,
                METADescription = package.meta_description,
                METAKeywords = package.meta_keywords,
                METATitle = package.meta_title,
                Description = package.description,
                Description2 = package.description2,
                LinkGroupKey = package.link_group_key
            };
            foreach (var shippingFeeDetail in shippingFeeDetails)
            {
                category.ShippingFees.Add(new ShippingFee{MinimumQuantity = shippingFeeDetail.minimum_quantity, MaximumQuantity = shippingFeeDetail.maximum_quantity, Fee = shippingFeeDetail.fee});
            }
            return category;
        }
    }
}
