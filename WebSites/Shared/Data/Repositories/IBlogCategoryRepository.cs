using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IBlogCategoryRepository : IRepository<BlogCategory>
   {
      BlogCategory GetByUrl(string url);
   }
}
