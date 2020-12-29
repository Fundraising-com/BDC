using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IBlogPostRepository : IRepository<BlogPost>
   {
      BlogPost GetByUrl(string url, bool isPreview);
      BlogPost GetByCategory(int id);
      IList<BlogPost> GetAllByTagId(int tagId);
      IList<BlogPost> GetAllByCategoryId(int categoryId);
   }
}
