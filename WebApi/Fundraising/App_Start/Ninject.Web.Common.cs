using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using FluentValidation;
using GA.BDC.Data.Fundraising.EFundStore.Repositories;
using GA.BDC.WebApi.Fundraising;
using log4net;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Extensions.Logging.Log4net;
using Ninject.Web.Common;
using WebActivatorEx;
using WebApiContrib.IoC.Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Planning.Bindings;
using GA.BDC.Shared;
using Ninject.Web.WebApi.Validation;
using Ninject.Web.Common.WebHost;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace GA.BDC.WebApi.Fundraising
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            log4net.Config.XmlConfigurator.Configure();
            var log = LogManager.GetLogger(typeof(NinjectWebCommon));
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                kernel.Bind(typeof(Log4NetModule));
                RegisterServices(kernel);
                FluentValidationSetup(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);

                return kernel;
            }
            catch (Exception exception)
            {
                kernel.Dispose();
                log.Fatal(exception);
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {            
            kernel.Bind(
                p =>
                    p.FromAssemblyContaining<ProductRepository>()
                        .SelectAllClasses()
                        .BindAllInterfaces()
                        .Configure(c => c.InRequestScope()));            
        }

        /// Set up Fluent Validation for WebApi.
        /// </summary>
        private static void FluentValidationSetup(IKernel kernel)
        {
            var ninjectValidatorFactory = new NinjectValidatorFactory(kernel);
            // Configure WebApi
            FluentValidation.WebApi.FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration, provider => provider.ValidatorFactory = ninjectValidatorFactory);

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }
    }

    public class NinjectValidatorFactory : ValidatorFactoryBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectValidatorFactory"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectValidatorFactory(IKernel kernel)
        {
            Kernel = kernel;
        }

        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        /// <value>The kernel.</value>
        public IKernel Kernel
        {
            get;
            set;
        }

        /// <summary>
        /// Creates an instance of a validator with the given type using ninject.
        /// </summary>
        /// <param name="validatorType">Type of the validator.</param>
        /// <returns>The newly created validator</returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            if (((IList<IBinding>)Kernel.GetBindings(validatorType)).Count == 0)
            {
                return null;
            }

            return Kernel.Get(validatorType) as IValidator;
        }
    }
}