using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class BlogTagsMapper
    {
        public static BlogTag Hydrate(blog_tags row)
        {
            return row == null ? null :
                new BlogTag
                {
                    Id = row.id,
                    Name = row.name,
                    Url = row.url,
                };
        }

        public static blog_tags DeHydrate(BlogTag entity)
        {
            var row = new blog_tags
            {
                name = entity.Name,
                id = entity.Id,
                url = entity.Url,

            };
            return row;
        }

    }
}
