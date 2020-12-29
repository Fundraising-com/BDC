using System;

namespace GA.BDC.Shared.Data
{
   public interface IUnitOfWork : IDisposable
   {
      /// <summary>
      /// Persists the changes
      /// </summary>
      void Commit();
      /// <summary>
      /// Creates a Repository for the Data Provider given in the Constructor
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <returns></returns>
      T CreateRepository<T>() where  T : class;
   }
}
