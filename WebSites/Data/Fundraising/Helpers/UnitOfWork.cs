using System;
using System.Data.Entity;
using GA.BDC.Shared.Data;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Parameters;
using Database = GA.BDC.Shared.Data.Database;

namespace GA.BDC.Data.Fundraising.Helpers
{
   public class UnitOfWork : IUnitOfWork
   {
      private readonly DbContext _dataProvider;
      private readonly DbContextTransaction _transaction;

      public T CreateRepository<T>() where T : class
      {
         var kernel = new StandardKernel();
         kernel.Bind(
               p =>
                   p.FromAssemblyContaining<UnitOfWork>()
                       .SelectAllClasses()
                       .BindAllInterfaces());
         return kernel.Get<T>(new ConstructorArgument("dataProvider", _dataProvider));
      }

      public UnitOfWork(Database database) {
         switch (database)
         {
            case Database.Unknown:
               throw new Exception("Unknown Database");
            case Database.EFundraisingProd:
               _dataProvider = new EFundraisingProd.Tables.DataProvider();
               break;
            case Database.FastFundraising:
               _dataProvider = new FastFundraising.Tables.DataProvider();
               break;
            case Database.EFRCommon:
               _dataProvider = new EFRCommon.Tables.DataProvider();
               break;
            case Database.EFundStore:
               _dataProvider = new EFundStore.Tables.DataProvider();
               break;
            case Database.ESubs:
               _dataProvider = new ESubs.Tables.DataProvider();
               break;            
                default:
               throw new ArgumentOutOfRangeException(nameof(database));
         }
         _transaction = _dataProvider.Database.BeginTransaction();
      }

      public void Commit()
      {
         try
         {
            _transaction.Commit();
         }
         catch (Exception)
         {
            _transaction.Rollback();
            throw;
         }
      }

      public void Dispose()
      {
         _dataProvider.Dispose();
      }
   }
}
