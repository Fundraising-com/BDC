using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IBlogTagRepository : IRepository<BlogTag>
   {
      BlogTag GetByUrl(string url);
      IList<BlogTag> GetAllByPostId(int postId);
   }
}
