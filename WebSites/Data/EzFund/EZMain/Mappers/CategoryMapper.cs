using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public class CategoryMapper
    {
        public static Category Hydrate(category category)
        {
            return new Category
            {
                Id = category.category_id,
                ParentId = category.parent_category_id!=null? (int)category.parent_category_id:-1,
                Name = category.name,
                Order = category.order,
                Image = new Image
                {
                    Url = category.image_name,
                    AlternativeText = category.image_alt_text
                },
                Description = category.description,
                Description2 = category.description2,
                Url = category.url,
                METATitle = category.meta_title,
                METADescription = category.meta_description,
                METAKeywords = category.meta_keywords
            };
        }

    }
}
