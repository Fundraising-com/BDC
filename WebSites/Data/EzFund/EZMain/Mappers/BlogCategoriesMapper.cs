using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class BlogCategoriesMapper
    {
        public static BlogCategory Hydrate(blog_categories categories)
        {
            var _categories = new BlogCategory
            {
                Id = categories.id,
                Name = categories.name,
                Url = categories.url,
            };
            return _categories;
        }

        public static blog_categories DeHydrate(BlogCategory categories)
        {
            var _categories = new blog_categories
            {
                name = categories.Name,
                id = categories.Id,
                url = categories.Url,

            };
            return _categories;
        }

    }
}
