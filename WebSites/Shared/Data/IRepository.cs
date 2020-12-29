using System.Collections.Generic;

namespace GA.BDC.Shared.Data
{
   public interface IRepository<TEntity> where TEntity : class
   {      
      TEntity GetById(int id);
      IList<TEntity> GetAll();
      int Save(TEntity model);
      void Update(TEntity model);
      void Delete(TEntity model);
     


    }
}
