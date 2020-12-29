using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
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
