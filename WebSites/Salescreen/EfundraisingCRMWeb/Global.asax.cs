using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using efundraising.EFundraisingCRM;
using efundraising.Configuration;
using log4net;


namespace EFundraisingCRMWeb
{

    using System.Web.Security;
    using efundraising.Intranet.BusinessEntities.Security.Interface;
    using efundraising.Intranet.BusinessEntities.Security.Principal;
    using efundraising.Utilities.CookieHandler;
    using efundraising.Intranet.BusinessEntities.Security.Identity;
    /// <summary>
    /// Summary description for Global.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ILog Logger { get; set; }
        public Global()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            Logger = LogManager.GetLogger(typeof(Global));
        }

        public struct SessionVariables
        {

            public const string CLIENT_INFO = "clientInfo";
            public const string LEAD_INFO = "leadInfo";
            public const string SALE_INFO = "saleInfo";
            public const string USER_INFO = "userInfo";
            public const string AUTHENTIFICATED = "authentificated";
            public const string CURRENTTAB = "currentTab";
            public const string DVUNASSIGNEDLEADS = "dvUnassignedLeads";
            public const string CURRENTDATETEXTBOX = "currentDateTextBox";
            public const string USER_ID = "userID";
            public const string CURRENT_LEAD_ID = "currentLeadID";
            public const string PROJECT_LOCATION = "projectLocation";
            public const string CONFIRMATION_EMAIL_SUBJECT = "confirmationEmailSubject";
            public const string ACCESS_LEAD_ACCOUNTS_ONLY = "accessLeadAccountsOnly";
            public const string LEAD_ACCOUNTS_ID = "leadAccountsID";
            public const string CONTROL_TO_DISPLAY = "controlToDisplay";
            public const string CURRENT_LEAD_REPORT = "currentLeadReport";
            public const string CURRENT_LEAD_REPORT_SCORE = "currentLeadReportScore";

            public const string LEAD_ID = "leadID";
            public const string CLIENT_ID = "clientID";
            public const string CLIENT_SEQUENCE_CODE = "clientsequencecode";
            public const string CONSULTANT_ID = "consultantID";
            public const string CLASS_TOTALS = "classTotals";
            public const string PACKAGE_TOTALS = "packageTotals";
            public const string SALE_COLLECTION = "saleCollection";
            public const string ROLES = "roles";
            public const string TEXTONCARD = "textOnCard";
            public const string ITEMSDATAGRID = "itemsDataGrid";
            public const string WFC_SALES = "wfcSales";
            public const string SALES_IN_CART = "salesInCart";
            public const string USER_NAME = "userName";
            public const string SALES_TAX_AND_SHIP = "salesTaxAndShip";
            public const string SALES_SHIPPING_FEE = "salesShippingFee";
            public const string SALES_ID = "salesID";
            public const string SALES_ITEM_QTY = "salesItemQty";










            public const string INTRANETWEBUSER = "INTRANETWEBUSER";
        }

        static private readonly string ListReportFileDumps = "ListReportFileDumps";



        private CountryCollections ShippingCountryCollections(string[] countryCodes)
        {
            CountryCollections countryList = new CountryCollections();
            Hashtable hashTBCountries = Application[allCountriesKey] as Hashtable;

            if (hashTBCountries != null)
            {
                for (int i = 0; i < countryCodes.Length; i++)
                {

                    Countries ctries = hashTBCountries[countryCodes[i]] as Countries;
                    if (ctries != null)
                    {
                        Country ct = new Country(ctries.CountryCode, ctries.CountryName, ctries.CurrencyCode);
                        if (ct != null)
                        {
                            State[] sts = State.GetStatesByCountryCode(ct.CountryCode);
                            for (int k = 0; k < sts.Length; k++)
                            {

                                ct.CountryStates.Add(sts[k]);
                            }
                            ct.CountryStates.Sort();
                            countryList.Add(ct);
                        }
                    }
                }
            }
            return countryList;
        }

        private static decimal IncrementProfit
        {
            get
            {
                try
                {

                    decimal t = Decimal.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("IncrementProfit"));
                    if (t < (decimal)1.0)
                        return t;
                    else
                        return (decimal)0.05;
                }
                catch (Exception)
                {
                    return (decimal)0.05;
                }
            }
        }


        protected void Application_Start(Object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; //PCI
            Hashtable hs = new Hashtable();
            Countries[] cts = Countries.GetCountriess();
            for (int i = 0; i < cts.Length; i++)
            {
                hs[cts[i].CountryCode] = cts[i];
            }
            Application.Add(allCountriesKey, hs);
            efundraising.EFundraisingCRM.CountryCollections country = ShippingCountryCollections(Global.shippingCountryCodes);
            Application.Add(shippingCountriesKey, country);
           
           
            
        }

        protected void Session_Start(Object sender, EventArgs e)
        {

        
 
        }


        public static string GetValueFromWebConfig(string key, string keyValue)
        {
            return efundraising.Configuration.ApplicationSettings.GetConfig()[key, keyValue].ToString();

        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            var isHttps = Convert.ToString(GetValueFromWebConfig("allowHttpsOnly", "value"));
           
            if (isHttps == "true")
            {
                if (HttpContext.Current.Request.IsSecureConnection.Equals(false))
                {
                    Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl);
                }
            }

        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
           
            Exception ex = Server.GetLastError();
            HttpContext context = HttpContext.Current;
            if (context.Request.Url.Host.ToLower().IndexOf("localhost") == -1
                && ex.StackTrace.IndexOf("get_aspx_ver.aspx") == -1
                )
            {

                Logger.Fatal(ex);

                Components.Server.Tibo.TiboTalker tiboTalker =
                    new Components.Server.Tibo.TiboTalker(
                    "Unhandled Error", ex.Message, null, "An error occured with the CRM", null, 3);
            }

        }

        protected void Session_End(Object sender, EventArgs e)
        {

            /*if (System.Environment.MachineName != "EFR-LAVIGNEJXP")
            {
              
            }*/
         /*   string host = Components.Server.ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "Host");
            string path = "http://" + host + "/SessionExpired.aspx";

            string script = "<script language='javascript'>window.open('" + path + "','Streaming', 'width=900, height=700, location=no, menubar=no, status=no, toolbar=no, scrollbars=yes, resizable=yes')</script>";
            Page.RegisterClientScriptBlock("Open", script);

           /* ArrayList arList = (ArrayList)Session[ListReportFile/*Dumps];
            if (arList != null)
            {
                for (int i = 0; i < arList.Count; i++)
                {
                    try
                    {
                        System.IO.File.Delete((string)arList[i]);
                    }
                    catch (Exception)
                    {
                    }
                }
            }*/

        }

        protected void Application_End(Object sender, EventArgs e)
        {

        }



        #region Public Static and non Static


        static public readonly string FromIntranetWeb = "intranetweb";
        // Cache has no refresh data
        static public Hashtable scratchBookPackageName = new Hashtable();
        static public Hashtable packageDefaultProfit = new Hashtable();
        static private Hashtable groupTypeHastable = new Hashtable();
        static private Hashtable supplierHastable = new Hashtable();
        static private Hashtable eFundraisingStoreProductHastabble = new Hashtable();
        // This refreshes data every 30 minutes
        //static public CachingHastable scratchBookPackageName = new CachingHastable(0, 30, 0);
        //static private CachingHastable packageDefaultProfit = new CachingHastable(0, 30, 0);


        static public readonly string scratchBookConstCache = "SCRATCHBOOKCONSTCACHE";
        static public readonly string shippingCountriesKey = "SHIPPINGCOUNTRIES";
        static private readonly string allCountriesKey = "ALLCOUNTRIES";
        static public readonly string esubGlobalCountriesKey = "ESUBGLOBALCOUNTRIES";


        static bool IsDebugOn()
        {
            try
            {
                return bool.Parse(System.Configuration.ConfigurationSettings.AppSettings["IsDebugOn"]);
            }
            catch (Exception)
            {
                return false;
            }
        }

        static public string[] GetEditableGroups(HttpApplicationState theApp)
        {
            if (theApp.Get("GroupsRoles") == null)
            {
                try
                {
                    string roles = ApplicationSettings.GetConfig()["EFUNDRAISINGPROD.ACTUALSHIPDATE.GROUPS", 0, "roles"];
                    theApp.Add("GroupsRoles", roles.Split('|'));
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return (string[])theApp.Get("GroupsRoles");
        }

        static public bool IsEditableGroup(HttpApplicationState theApp, string groupName)
        {
            string[] allowGroups = GetEditableGroups(theApp);
            if (allowGroups != null && allowGroups.Length > 0)
            {
                for (int i = 0; i < allowGroups.Length; i++)
                {
                    if (string.Compare(groupName, allowGroups[i], true) == 0)
                        return true;
                }
            }
            return false;
        }



        static public string[] shippingCountryCodes
        {
            get
            {
                try
                {

                    string[] result = System.Configuration.ConfigurationSettings.AppSettings.Get("ShippingCountryCode").Split(',');
                    return result;
                }
                catch (Exception)
                {
                    return new string[] { "US" };
                }
            }
        }


        static public string[] eSubGlobalCountryCodes
        {
            get
            {
                try
                {

                    string[] result = System.Configuration.ConfigurationSettings.AppSettings.Get("ShippingCountryCode").Split(',');
                    return result;
                }
                catch (Exception)
                {
                    return new string[] { "US" };
                }
            }
        }

        static public Package GetPackageById(int packageId)
        {
           // if (packageDefaultProfit[packageId] == null)
           // {
                packageDefaultProfit[packageId] = Package.GetPackageByID(packageId);
           // }

            return (Package)packageDefaultProfit[packageId];
        }

        static public DataTable GetProfitValues(int packageId, ref decimal defaultProfit)
        {
            Package p = GetPackageById(packageId);
            DataTable dt = null;
            if (p != null)
            {
                if (p.profitList == null)
                {
                    dt = Package.GetDefaultProfit(packageId, ref defaultProfit, p, IncrementProfit);
                    p.profitList = dt;
                }
                else
                {
                    dt = p.profitList;
                    defaultProfit = p.ProfitDefault;
                }
            }
            return dt;
        }


        public static int PageSize
        {
            get
            {
                try
                {

                    int t = int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("PageSize"));
                    if (t <= 0)
                        return 20;
                    else
                        return t;
                }
                catch (Exception)
                {
                    return 20;
                }
            }
        }

        public static int PageButtonCount
        {
            get
            {
                try
                {
                    int t = int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("PageButtonCount"));
                    if (t <= 0)
                        return 10;
                    else
                        return t;
                }
                catch (Exception)
                {
                    return 10;
                }
            }
        }

        public static bool TopAndBottom
        {
            get
            {
                try
                {
                    return bool.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("TopAndBottom"));
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static string GetDataGridPageSize()
        {
            return System.Configuration.ConfigurationSettings.AppSettings.Get("CheckSystemPageSize");
        }

        public static string GetReportFileDumpDirectory(System.Web.UI.Page thePage)
        {
            string reportReportFileDumpDirectory = string.Empty;
            if (reportReportFileDumpDirectory == string.Empty)
                reportReportFileDumpDirectory = thePage.Server.MapPath(efundraising.EnterpriseComponents.Helper.GetWebConfigValue("Report.SaveDocumentPath"));

            return reportReportFileDumpDirectory;
        }

        public static ArrayList GetListReportFileDumps(System.Web.UI.Page thePage)
        {
            if (thePage.Session[ListReportFileDumps] == null)
                thePage.Session[ListReportFileDumps] = new ArrayList();

            return (ArrayList)thePage.Session[ListReportFileDumps];
        }



        public static efundraising.eFundraisingStore.Product GetProductObject(int prId)
        {
            if (Global.eFundraisingStoreProductHastabble[prId] == null)
            {
                Global.eFundraisingStoreProductHastabble[prId] = efundraising.eFundraisingStore.Product.GetProductByID(prId);
            }

            return Global.eFundraisingStoreProductHastabble[prId] as efundraising.eFundraisingStore.Product;
        }

        public static GroupType GetGroupTypeByLeadID(int leadId)
        {

            if (Global.groupTypeHastable[leadId] == null)
                Global.groupTypeHastable[leadId] = GroupType.GetGroupTypeByLeadID(leadId);
            return Global.groupTypeHastable[leadId] as GroupType;
        }

        private static string getSupplierByID = "GetSupplierByIDKey";

        public static Supplier GetSupplierByID(System.Web.HttpApplicationState theAppState, int supId)
        {
            if (theAppState.Get(getSupplierByID) == null)
                theAppState.Set(getSupplierByID, Supplier.GetSuppliers());

            Supplier[] suppls = (Supplier[])theAppState.Get(getSupplierByID);

            for (int i = 0; i < suppls.Length; i++)
            {
                if (suppls[i].SupplierId == supId)
                    return suppls[i];
            }
            return null;
        }


        private static string consultantsKeyByName = "ListConsultantKeyByName";
        private static string consultantsKeyById = "ListConsultantKeyById";

        public static ConsultantCollections GetConsultants(System.Web.HttpApplicationState theAppState)
        {
            if (theAppState.Get(consultantsKeyByName) == null)
            {
                ConsultantCollections cntColletions = Consultant.GetCollectionConsultants();
                cntColletions.SortByProperty("Name");
                theAppState.Set(consultantsKeyByName, cntColletions);
            }

            return (ConsultantCollections)theAppState.Get(consultantsKeyByName);
        }


        private static ConsultantCollections GetConsultantsByKeyID(System.Web.HttpApplicationState theAppState)
        {
            if (theAppState.Get(consultantsKeyById) == null)
            {
                ConsultantCollections cntColletions = Consultant.GetCollectionConsultants();
                cntColletions.SortByProperty("ConsultantId");
                theAppState.Set(consultantsKeyById, cntColletions);
            }

            return (ConsultantCollections)theAppState.Get(consultantsKeyById);
        }

        public static Consultant GetConsultant(System.Web.HttpApplicationState theAppState, int consultantId)
        {
            //			EFundraisingCRM.Consultant[] cns = GetConsultants(theAppState);
            //			for(int i=0; i < cns.Length; i++)
            //			{
            //				if (cns[i].ConsultantId == consultantId)
            //					return cns[i];
            //			}
            //			return null;
            return GetConsultantsByKeyID(theAppState).SearchByConsultantID(consultantId);
        }


        private static string PoStatusCollectionsKey = "PoStatusCollectionsKey";
        public static PoStatusCollections GetPoStatusCollection(System.Web.HttpApplicationState theAppState)
        {
            if (theAppState.Get(PoStatusCollectionsKey) == null)
            {
                PoStatusCollections poStsCol = PoStatus.GetPoStatussCollections();
                poStsCol.SortByProperty("Description");
                theAppState.Set(PoStatusCollectionsKey, poStsCol);
            }

            return (PoStatusCollections)theAppState.Get(PoStatusCollectionsKey);
        }

        private static string CarrierCollectionsKey = "CarrierCollectionsKey";
        public static CarriersCollection GetCarrierCollection(System.Web.HttpApplicationState theAppState)
        {
            if (theAppState.Get(CarrierCollectionsKey) == null)
            {
                CarriersCollection crCollections = new CarriersCollection();
                crCollections.GetAllCarriers();
                crCollections.SortByProperty("Description");
                theAppState.Set(CarrierCollectionsKey, crCollections);
            }

            return (CarriersCollection)theAppState.Get(CarrierCollectionsKey);
        }
        private static string CarrierShippingCollectionsKey = "CarrierShippingCollectionsKey";
        public static CarrierShippingOptionCollection GetCarrierShippingOptionCollection(System.Web.HttpApplicationState theAppState)
        {
            if (theAppState.Get(CarrierShippingCollectionsKey) == null)
            {
                CarrierShippingOptionCollection csoCollections = new CarrierShippingOptionCollection();
                csoCollections.GetAllCarrierShippingOptions();
                csoCollections.SortByProperty("Description");
                theAppState.Set(CarrierShippingCollectionsKey, csoCollections);
            }

            return (CarrierShippingOptionCollection)theAppState.Get(CarrierShippingCollectionsKey);
        }

        private static string SalesStatusCollectionsKey = "SalesStatusCollectionsKey";
        public static SalesStatusCollection GetSalesStatusCollection(System.Web.HttpApplicationState theAppState)
        {
            if (theAppState.Get(SalesStatusCollectionsKey) == null)
            {
                SalesStatusCollection ssCollections = new SalesStatusCollection();
                ssCollections.GetAllSalesStatuss();
                ssCollections.SortByProperty("Description");
                theAppState.Set(SalesStatusCollectionsKey, ssCollections);
            }

            return (SalesStatusCollection)theAppState.Get(SalesStatusCollectionsKey);
        }


        private static string ProductionStatusCollectionsKey = "ProductionStatusCollectionsKey";
        public static ProductionStatusCollection GetProductionStatusCollection(System.Web.HttpApplicationState theAppState)
        {
            if (theAppState.Get(ProductionStatusCollectionsKey) == null)
            {
                ProductionStatusCollection prCollections = new ProductionStatusCollection();
                prCollections.GetAllProductionStatuss();
                prCollections.SortByProperty("Description");
                theAppState.Set(ProductionStatusCollectionsKey, prCollections);
            }

            return (ProductionStatusCollection)theAppState.Get(ProductionStatusCollectionsKey);
        }


        private static string ARStatusCollectionsKey = "ARStatusCollectionsKey";
        public static ARStatusCollection GetARStatusCollection(System.Web.HttpApplicationState theAppState)
        {
            if (theAppState.Get(ARStatusCollectionsKey) == null)
            {
                ARStatusCollection arCollections = new ARStatusCollection();
                arCollections.GetAllARStatuss();
                arCollections.SortByProperty("Description");
                theAppState.Set(ARStatusCollectionsKey, arCollections);
            }

            return (ARStatusCollection)theAppState.Get(ARStatusCollectionsKey);
        }


        private static string CreditCheckStatusCollectionsKey = "CreditCheckStatusCollectionsKey";
        public static CreditCheckStatusCollection GetCreditCheckStatusCollection(System.Web.HttpApplicationState theAppState)
        {
            if (theAppState.Get(CreditCheckStatusCollectionsKey) == null)
            {
                CreditCheckStatusCollection cdckCollections = new CreditCheckStatusCollection();
                cdckCollections.GetAllCreditCheckStatuss();
                cdckCollections.SortByProperty("Description");
                theAppState.Set(CreditCheckStatusCollectionsKey, cdckCollections);
            }

            return (CreditCheckStatusCollection)theAppState.Get(CreditCheckStatusCollectionsKey);
        }


        private static string ClientSequenceCollectionsKey = "ClientSequenceCollectionsKey";
        public static ClientSequenceCollection GetClientSequenceCollection(System.Web.HttpApplicationState theAppState)
        {
            if (theAppState.Get(ClientSequenceCollectionsKey) == null)
            {
                ClientSequenceCollection csqCollections = new ClientSequenceCollection();
                csqCollections.GetAllClientSequences();
                csqCollections.SortByProperty("Description");
                theAppState.Set(ClientSequenceCollectionsKey, csqCollections);
            }

            return (ClientSequenceCollection)theAppState.Get(ClientSequenceCollectionsKey);
        }


        private static string PaymentMethodCollectionsKey = "PaymentMethodCollectionsKey";
        public static PaymentMethodCollections GetPaymentMethodCollections(System.Web.HttpApplicationState theAppState)
        {
            if (theAppState.Get(PaymentMethodCollectionsKey) == null)
            {
                PaymentMethodCollections theCollections = new PaymentMethodCollections();
                theCollections.GetAllPaymentMethods();
                theCollections.SortByProperty("Description");
                theAppState.Set(PaymentMethodCollectionsKey, theCollections);
            }

            return (PaymentMethodCollections)theAppState.Get(PaymentMethodCollectionsKey);
        }


        private static string PaymentTermCollectionsKey = "PaymentTermCollectionsKey";
        public static PaymentTermCollections GetPaymentTermCollections(System.Web.HttpApplicationState theAppState)
        {
            if (theAppState.Get(PaymentTermCollectionsKey) == null)
            {
                PaymentTermCollections theCollections = new PaymentTermCollections();
                theCollections.GetAllPaymentTerms();
                theCollections.SortByProperty("Description");
                theAppState.Set(PaymentTermCollectionsKey, theCollections);
            }

            return (PaymentTermCollections)theAppState.Get(PaymentTermCollectionsKey);
        }

        #endregion

        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }
        #endregion

        #region Sale Constant

        static public readonly string TreeViewPackage = "TreeViewPackage";
        #endregion
    }
}

