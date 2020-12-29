using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
    public static class BlogPostsMapper
    {
       public static BlogPost Hydrate(blog_post row)
       {
          return row == null
             ? null
             : new BlogPost
             {
                Id = row.id,
                Title = row.title,
                Url = row.url,
                Author = row.author,
                Text = row.text,
                Created = row.created,
                Published = row.published,
                IsDraft = row.is_draft,
                CategoryId = row.category_id,
                ImageUrl = row.image_url,
                ThumbnailUrl = row.thumbnail_url,
                MetaDescription = row.meta_description,
                MetaTitle = row.meta_title,
                Summary = row.summary
             };
       }

       public static blog_post DeHydrate(BlogPost entity)
        {
            var row = new blog_post
            {
                id = entity.Id,
                title = entity.Title,
                url = entity.Url,
                author = entity.Author,
                text = entity.Text,
                created = entity.Created,
                published = entity.Published,
                is_draft = entity.IsDraft,
                category_id = entity.CategoryId,
                image_url = entity.ImageUrl,
                thumbnail_url = entity.ThumbnailUrl,
                meta_description = entity.MetaDescription,
                meta_title = entity.MetaTitle,
                summary = entity.Summary
            };
            return row;
        }
    }
}
