using System;
using System.Data.Entity;
using GA.BDC.Shared.Data;
using Ninject;
using Ninject.Parameters;
using Database = GA.BDC.Shared.Data.Database;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Data.EzFund.EZMain.Repositories;

namespace GA.BDC.Data.EzFund.Helpers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dataProvider;
        private readonly DbContextTransaction _transaction;

        public T CreateRepository<T>() where T : class
        {
            var kernel = new StandardKernel();
            kernel.Bind<ILeadRepository>().To<LeadsRepository>();
            kernel.Bind<ITestimonialsRepository>().To<TestimonialsRepository>();
            kernel.Bind<IProspectRepository>().To<ProspectRepository>();
            kernel.Bind<IReferralRepository>().To<ReferralsRepository>();
            kernel.Bind<IStartingDateRepository>().To<SalesStartingDateRepository>();
            kernel.Bind<ISellingKitLeadRepository>().To<SellingKitLeadsRepository>();
            kernel.Bind<IPrimaryProgramRepository>().To<PrimaryProgramRepository>();
            kernel.Bind<IOrganizationTypeRepository>().To<OrganizationTypeRepository>();
            kernel.Bind<IProductClassRepository>().To<ProductsClassRepository>();
            kernel.Bind<IProductRepository>().To<ProductsRepository>();
            kernel.Bind<ICategoriesRepository>().To<CategoriesRepository>();
            kernel.Bind<IHomePageRotatorRepository>().To<HomePageRotatorRepository>();
            kernel.Bind<INewsletterSubscriptionRepository>().To<NewsletterSubscriptionRepository>();
            kernel.Bind<IBlogCategoryRepository>().To<BlogCategoryRepository>();
            kernel.Bind<IBlogPostRepository>().To<BlogPostRepository>();
            kernel.Bind<IBlogTagRepository>().To<BlogTagRepository>();
            kernel.Bind<INotificationRepository>().To<NotificationRepository>();
            kernel.Bind<ISalesRepository>().To<SalesRepository>();
            kernel.Bind<IWorkflowRepository>().To<WorkflowRepository>();
            kernel.Bind<IOrganizationRepository>().To<OrganizationRepository>();
            kernel.Bind<IShoppingCartRepository>().To<ShoppingCartRepository>();
				kernel.Bind<IRouteMapperRepository>().To<RouteMapperRepository>();
            kernel.Bind<IArTrnsTblRepository>().To<ArTrnsTblRepository>();

            //TODO: Javi, make this generic
            //kernel.Bind(
            //      p =>
            //          p.FromAssemblyContaining<UnitOfWork>()
            //              .SelectAllClasses()
            //              .BindAllInterfaces());
            return kernel.Get<T>(new ConstructorArgument("dataProvider", _dataProvider));
        }

        public UnitOfWork(Database database)
        {
            switch (database)
            {
                case Database.Unknown:
                    throw new Exception("Unknown Database");
	            case Database.EZMain:
                    _dataProvider = new EZMain.Tables.DataProvider();
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
