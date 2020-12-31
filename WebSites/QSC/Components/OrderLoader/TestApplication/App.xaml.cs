using System.Windows;
using GalaSoft.MvvmLight.Threading;
using Data;
using System.Diagnostics;
using System.Linq;
using System.Configuration;
using System;
using System.Activities;
using System.Activities.Statements;

using Common;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.Unity;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Xml.Linq;
using System.Data.Objects;
using BusinessServices;
using ActivityLibrary;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Collections.Specialized;
using Business;
using System.Data.Metadata.Edm;

namespace TestApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
           
            DispatcherHelper.Initialize();

            
            FileConfigurationSource fcs =
                new FileConfigurationSource(
                    "C:\\Projects\\OrderLoader\\BusinessServices\\App.config"
                );

            var ffffff = fcs.GetSection("unity");

            var builder = new ConfigurationSourceBuilder();
            builder.UpdateConfigurationWithReplace(fcs);

        
            var coreExtension = new EnterpriseLibraryCoreExtension(fcs);

            var container = new UnityContainer();
            container.AddExtension(coreExtension);


            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = "C:\\Projects\\OrderLoader\\Data\\xxxx.config";
            System.Configuration.Configuration config
              = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            UnityConfigurationSection section
              = (UnityConfigurationSection)config.GetSection("unity");

            UnityServiceLocator locator = new UnityServiceLocator(container);
            section.Containers["OrderLoading"].Configure(container);
            //section.Configure(ccc);


            //container.LoadConfiguration();
            //IUnityContainer foo = container.RegisterType<ObjectContext, QSPCanadaOrderManagementEntities>(new InjectionConstructor());//container.RegisterType<IProductAndPricingService, ProductAndPricingService>();

 
            //UnityConfigurationSection section
             //           = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            //section. .ConfigureContainer(foo);


            ServiceLocator.SetLocatorProvider(() => locator);

            container.RegisterType<ObjectContext, QSPCanadaOrderManagementEntities>(new InjectionConstructor());
            container.RegisterType(typeof(IRepository<>), typeof(EFRepository<>));
            container.RegisterType<OrderBatchService>();
            container.RegisterType<IProductAndPricingService>();
            //container.RegisterType<TaxService>();
            container.RegisterType<Teacher>();
            container.RegisterType<Batch>();
            container.RegisterType<Student>();            
            container.RegisterType<Customer>();
            container.RegisterType<CustomerOrderHeader>();
            container.RegisterType<CustomerOrderDetail>();
            container.RegisterType<CreditCardBatch>();
            container.RegisterType<ProductAndPricingService>();
            container.RegisterType<ProductPricings>();
            container.RegisterType<InternetOrderID>();

            IUnityContainer c = container.RegisterType<Data.BatchNode>();

            QSPCanadaOrderManagementEntities ooo = (QSPCanadaOrderManagementEntities)ServiceLocator.Current.GetInstance<ObjectContext>();

            string entitySetName = ooo.MetadataWorkspace
                        .GetEntityContainer(ooo.DefaultContainerName, DataSpace.CSpace)
                        .BaseEntitySets.Where(bes => bes.ElementType.Name == typeof(Batch).Name).First().Name;

            string name = String.Format("{0}.{1}", ooo.DefaultContainerName, entitySetName);


            IRepository<Batch> mmm = container.Resolve<IRepository<Batch>>();

           

            // PricingData foo2 = myServiceInstance.GetPricingData("0514",12, 84405, 46001);

            /*
            GetMagazineClosestMatchingOffer wf = new GetMagazineClosestMatchingOffer();
               
            wf.zProductCode="0514";
            wf.nProductQty = 12;
            wf.nCampaignID = 84405;
                    
                
            IDictionary<String, Object>wfresults = WorkflowInvoker.Invoke(wf);
            */
            EnterpriseLibraryContainer.Current =  new UnityServiceLocator(container);
           
            System.DateTime d = new System.DateTime(2012, 10, 24);

            /*
            //Entity framework context
            var _c = new QSPCanadaOrderManagementEntities();
            var b = from c in _c.Batches where c.Date == d && c.ID == 3671 select c;

            foreach (var x in b)
            {
                Debug.WriteLine("{0}", x.KE3FileName);
            }
            int acctid = 1000;
            int cid = 1;
            var act = _c.Accounts
                    .Where(a => a.Id == acctid)
                          .Select(a => a.Name);

            Account acct = _c.Accounts.SingleOrDefault(a => a.Id == acctid);

            var camp = _c.Campaigns
                            .Where(c => c.ID == cid && c.BillToAccountID == acctid)
                            .Select(c=>c).ToList();
            */
            IRepository<Campaign> _b = ServiceLocator.Current.GetInstance<IRepository<Campaign>>();

            var test =  ooo.Batchs
                       .Where(b => b.OrderID == 111) 
                        .Select(b=>b);

            Batch bbbbbb = test.FirstOrDefault();
            var spec = new  Specification<Campaign>(b => b.ID == 111);

            var query = _b.DoQuery(spec);
            Campaign _c = query.First<Campaign>();
            foreach (var argg in query)
            {
                int iiii = 0;
            }
            var _batch = query.First();

            System.DateTime dd = new System.DateTime(2012, 10, 25);

            Batch _aBatch = new Batch();
            _aBatch.DateCreated = dd;
           
            _aBatch.OrderQualifierID = 39009;//(int)BatchStatus.New;
            ooo.Batchs.AddObject(_aBatch);

        //    int cccci = ooo.SaveChanges();

            var factory = container.Resolve<ValidatorFactory>();

            var aVal = factory.CreateValidator<Batch>("Batch Header Validations");        
            var results = aVal.Validate(_aBatch);

            if (results.IsValid == false)
            {
                foreach(var vresult in results)
                {
                    Console.WriteLine(vresult.Message);
                }
            }
         
            /*
            ProcessInternetFileActivity aFileActivity = new ProcessInternetFileActivity();
            IDictionary<String, Object> wf = WorkflowInvoker.Invoke(aFileActivity,
                      new Dictionary<String, Object>
                      {
                          {"fileName", @"C:\Projects\OrderLoader\QSPCanada2012_10_28_07_25_00.xml"}
                        
                      }
                   );
            */

            ProcessNonInternetFileActivity aNonFileActivity = new ProcessNonInternetFileActivity();
            IDictionary<String, Object> nonwf = WorkflowInvoker.Invoke(aNonFileActivity,
                      new Dictionary<String, Object>
                      {
                          {"fileName", @"C:\Projects\OrderLoader\2012_10_15_14_57_17.MAGEXP.xml.pgp.xml"}
                        
                      }
                   );
            

            XDocument xDoc = XDocument.Load(@"C:\Projects\OrderLoader\SmallWCC.xml");
            var batch = from r in xDoc.Descendants("BATCH")
                        select r;
            foreach( var f in batch)
            {
               
                  BatchNodeParser aBN = new BatchNodeParser();
                  EnvelopeNodeParser aEN = new EnvelopeNodeParser();
                  MagOrderDetailParser aMO = new MagOrderDetailParser();
                  PaymentNodeParser  aPN = new PaymentNodeParser();
                  ParticipantNodeParser aPartP = new ParticipantNodeParser();
                  AddressNodeParser aAP = new AddressNodeParser();
                  InternetNodeParser aIIDP = new InternetNodeParser();

                  BatchNode a = aBN.ParseNonInternetNode(f);

                var yDoc = from x in f.Descendants("ENVELOPE")
                               select x;


                foreach (var env in yDoc)
                {
                    EnvelopeNode eNode = aEN.ParseNonInternetNode(env);
                   
                    var sDoc = from student in env.Descendants("PARTICIPANT")
                               select student;


                    foreach (var sData in sDoc)
                    {
                        ParticipantNode apartn = aPartP.ParseNonInternetNode(sData);
                      
                       var magorders = from m in sData.Descendants("MAGAZINEORDERS").Descendants("MAGORDER")
                                       select m;

                       foreach (var mNode in magorders)
                       {
                           XElement aBillToAddress = mNode.Descendants("MAILINGADDRESS").First();
                           XElement aPayment = mNode.Descendants("PAYMENT").First();

                           AddressNode add = aAP.ParseInternetNode(aBillToAddress);
                           PaymentNode pnode = aPN.ParseNonInternetNode(aPayment);

                           
                           var detail = from mDetail in mNode.Descendants("MAGORDERDETAIL")
                                        select mDetail;
                           foreach (var morestuff in detail)
                           {
                               MagOrderDetailNode mon = aMO.ParseNonInternetNode(morestuff);

                           }
                       }
                        
                    }
                     
                }

            }
                            
          




        //    _c.Batches.AddObject(_aBatch);

            Teacher t = new Teacher();
            t.LastName = "Teacher";

            Student s = new Student();
     //       s.Teacher = t;
            s.FirstName = "foo";
            s.LastName = "bar";

            Customer _cust = new Customer();
            _cust.FirstName = "Dizzy";
            _cust.LastName = "The Dog";
            _cust.ChangeDate = dd;

            CustomerOrderHeader coh = new CustomerOrderHeader();
            coh.Batch = _aBatch;
            coh.Student = s;
            coh.Customer = _cust;
     
            CustomerOrderDetail cod = new CustomerOrderDetail();
            cod.CustomerOrderHeader = coh;

            
            int i = 0;
            try
            {
              //  i = _c.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());
                
            }
            catch (System.ArgumentException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            catch (System.Exception ex_)
            {
                Console.WriteLine("Error: {0}", ex_.ToString());
            }


            var Stufff = ServiceLocator.Current.GetInstance<OrderBatchService>();

           


        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }

}
