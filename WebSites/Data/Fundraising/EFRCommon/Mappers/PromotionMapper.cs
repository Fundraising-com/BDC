using GA.BDC.Data.Fundraising.EFRCommon.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFRCommon.Mappers
{

    public static class PromotionMapper
    {
        public static Promotion Hydrate(promotion promotion, int partnerId)
        {
            var result = new Promotion
            {
                Id = promotion.promotion_id,
                Type = promotion.promotion_type_code,
                DestinationId = promotion.promotion_destination_id,
                Name = promotion.promotion_name,
                ScriptName = promotion.script_name,
                IsActive = promotion.active,
                IsDsplayable = promotion.is_displayable,
                CookieContent = promotion.cookie_content,
                Keyword = promotion.keyword,
                CreateDate = promotion.create_date,
                PartnerId = partnerId
            };
            return result;
        }

        public static promotion Dehydrate(Promotion promotion)
        {
            var result = new promotion
            {
                active = promotion.IsActive,
                create_date = promotion.CreateDate,
                promotion_id = promotion.Id,
                script_name = promotion.ScriptName,
                is_displayable = promotion.IsDsplayable,
                cookie_content = promotion.CookieContent,
                promotion_name = promotion.Name,
                promotion_destination_id = promotion.DestinationId,
                keyword = promotion.Keyword,
                promotion_type_code = promotion.Type
            };
            return result;
        }
    }
 }
