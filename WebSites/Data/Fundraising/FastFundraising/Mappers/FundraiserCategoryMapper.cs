using GA.BDC.Data.Fundraising.FastFundraising.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.FastFundraising.Mappers
{
    public static class FundraiserCategoryMapper
    {
        public static FundraiserCategory Hydrate(fundraiser_category fc)
        {
            var result = new FundraiserCategory
            {
                Id = fc.id,
                Name = fc.name,
                Image = fc.image,
                Order = fc.display_order,
                Url = fc.url
            };
            return result;
        }
    }
}
