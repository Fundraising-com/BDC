using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using efundraising.EFundraisingCRM;
using efundraising.Utilities;
using efundraising.Utilities.CookieHandler;
using efundraising.Utilities.Compression;
using System.Security.Principal;
using log4net;

//using EFundraisingCRMWeb.App_Data;

namespace EFundraisingCRMWeb
{	/// Summary description for EFundraisingCRMWebBasePage.
    /// </summary>
    /// 

    using System.Web.UI.WebControls;
    using System.Security.Principal;
    using efundraising.Intranet.BusinessEntities.Security.Principal;
    using efundraising.Intranet.BusinessEntities.Security.Identity;


    public enum LogType
    {
        Information,
        Warning,
        Error
    }

    interface IViewStateCompression
    {
    }


    [Serializable]
    public class UserInfo : CustomIdentity
    {
        public string Redirect = string.Empty;
        public string RequiredParameters = string.Empty;


        public UserInfo(string userName, string roles)
            : base(userName, roles)
        {

        }
    }

    #region Interfaces
    interface IGetCurrentSale
    {
        Sale GetCurrentSale();
        Decimal GetOtherSaleFee();
        string GetSessionKeyIds(string key);
    }


    interface IPackByStudent
    {
        int GetSaleId();
        bool IsReadOnly();
    }

    #endregion

    #region Virtual Base Page
    public class EFundraisingWebBasePage : System.Web.UI.Page
    {
        public ILog Logger { get; set; }

        public EFundraisingWebBasePage()
        {
            Logger = LogManager.GetLogger(GetType());
        }
        public readonly static string NoValidation = "NOVALIDATION";

        public string appPath
        {
            get
            {
                string ApPath = Request.ApplicationPath;
                if (!ApPath.EndsWith("/"))
                    ApPath += "/";
                return ApPath;
            }
        }

        // get the current page name of a complete path (last folder + filename)
        protected string GetPageName()
        {
            return GetPageName(Request.Url.AbsolutePath);
        }

        // get the page name of a complete path (last folder + filename)
        protected string GetPageName(string absolutePath)
        {
            // 	url	"/EFundraisingCRMWeb/IT/EmailTemplates/EditEmailTemplate.aspx"	string

            string url = absolutePath;
            string[] urlSplitted = url.Split('/');
            int sectionIndex = (urlSplitted.Length == 5 ? 2 : 2);
            int pageIndex = (urlSplitted.Length == 5 ? 3 : 2);
            return urlSplitted[pageIndex] + "/" +
                urlSplitted[pageIndex + 1];
        }

        // get the current section name of a complete path (first folder)
        // generally the department name
        protected string GetSectionName()
        {
            return GetSectionName(Request.Url.AbsolutePath);
        }

        // get the section name of a complete path (first folder)
        // generally the department name
        protected string GetSectionName(string absolutePath)
        {
            string url = absolutePath;
            string[] urlSplitted = url.Split('/');
            int sectionIndex = (urlSplitted.Length == 5 ? 2 : 1);

            return urlSplitted[sectionIndex];
        }

        protected string GetUsername()
        {
            return null;
        }

        protected string[] GetRoles()
        {
            return null;
        }

        // all redirection should pass by this method
        protected void Redirect(string redirect)
        {
            Response.Redirect(redirect, false);
        }

        // static method to log an entry to tibo
        public static void InsertTiboEntry(string displayName, string extraInformation, efundraising.Core.BusinessBase obj, string objectDescription, string objectLink, int objectType)
        {
            Components.Server.Tibo.TiboTalker tiboTalker =
                new Components.Server.Tibo.TiboTalker(
                displayName, extraInformation, obj, objectDescription, objectLink, objectType);
        }

        // internal access to tibo system
        private void InsertTibo(string displayName, string extraInformation, efundraising.Core.BusinessBase obj, string objectDescription, string objectLink, int objectType)
        {
            Components.Server.Tibo.TiboTalker tiboTalker =
                new Components.Server.Tibo.TiboTalker(
                displayName, extraInformation, obj, objectDescription, objectLink, objectType);
        }

        // all log we want to log through the logging system and tibo
        protected void Log(EFundraisingCRMObject obj, LogType logType)
        {
            switch (logType)
            {
                case LogType.Information:
                    InsertTibo(obj.ToString(), "", obj, obj.ToString(), null, 1);
                    break;
                case LogType.Warning:
                    InsertTibo(obj.ToString(), "", obj, obj.ToString(), null, 2);
                    break;
                case LogType.Error:
                    InsertTibo(obj.ToString(), "", obj, obj.ToString(), null, 3);
                    break;
            }
        }
        // needed for user controls
        public static void StaticLog(EFundraisingCRMObject obj, LogType logType)
        {
            switch (logType)
            {
                case LogType.Information:
                    InsertTiboEntry(obj.ToString(), "", obj, obj.ToString(), null, 1);
                    break;
                case LogType.Warning:
                    InsertTiboEntry(obj.ToString(), "", obj, obj.ToString(), null, 2);
                    break;
                case LogType.Error:
                    InsertTiboEntry(obj.ToString(), "", obj, obj.ToString(), null, 3);
                    break;
            }
        }
        // needed for user controls
        public static void StaticLog(string message, EFundraisingCRMObject obj, LogType logType)
        {
            switch (logType)
            {
                case LogType.Information:
                    InsertTiboEntry(message, "", obj, obj.ToString(), null, 1);
                    break;
                case LogType.Warning:
                    InsertTiboEntry(message, "", obj, obj.ToString(), null, 2);
                    break;
                case LogType.Error:
                    InsertTiboEntry(message, "", obj, obj.ToString(), null, 3);
                    break;
            }
        }
        protected void Log(string message, EFundraisingCRMObject obj, LogType logType)
        {
            switch (logType)
            {
                case LogType.Information:
                    if (obj != null)
                    {
                        InsertTibo(message, "", obj, obj.ToString(), null, 1);
                    }
                    else
                    {
                        InsertTibo(message, "EFundraisingCRM Information", obj, message, null, 1);
                    }
                    break;
                case LogType.Warning:
                    if (obj != null)
                    {
                        InsertTibo(message, "", obj, obj.ToString(), null, 2);
                    }
                    else
                    {
                        InsertTibo(message, "EFundraisingCRM Information", obj, message, null, 2);
                    }
                    break;
                case LogType.Error:
                    if (obj != null)
                    {
                        InsertTibo(message, "", obj, obj.ToString(), null, 3);
                    }
                    else
                    {
                        InsertTibo(message, "EFundraisingCRM Information", obj, message, null, 3);
                    }
                    break;
            }
        }

        protected void Log(EFundraisingCRMObject[] objs, LogType logType)
        {
            foreach (EFundraisingCRMObject obj in objs)
            {
                switch (logType)
                {
                    case LogType.Information:
                        InsertTibo(obj.ToString(), "", obj, obj.ToString(), null, 1);
                        break;
                    case LogType.Warning:
                        InsertTibo(obj.ToString(), "", obj, obj.ToString(), null, 2);
                        break;
                    case LogType.Error:
                        InsertTibo(obj.ToString(), "", obj, obj.ToString(), null, 3);
                        break;
                }
            }
        }



        private bool IsAccess(bool write, string absolutePage)
        {
            bool authentication =
                (bool)(Convert.ToBoolean(efundraising.EnterpriseComponents.Helper.GetWebConfigValue("IntegratedLogin")));

            if (!authentication)
            {
                return true;
            }
            // get the current user
            Components.Server.User.CrmUser user =
                Components.Server.User.CrmUser.Create(Session);

            // get the menu three
            Components.Server.Menu.eFundraisingCrmWebMenu efundCrmWebMenu =
                Components.Server.Menu.eFundraisingCrmWebMenu.Create(Cache);

            // get the current page and section
            string pageName = null;
            string sectionName = null;

            // get current or other page
            if (absolutePage == null)
            {
                pageName = GetPageName();
                sectionName = GetSectionName();
            }
            else
            {
                pageName = GetPageName(absolutePage);
                sectionName = GetSectionName(absolutePage);
            }

            // retrieve the current section from the menu
            Components.Server.Menu.Section section =
                efundCrmWebMenu.Sections.GetSectionByID(sectionName);

            if (section == null)
            {
                return true;
            }

            // check the section if the user has the read rights
            foreach (Components.Server.User.Role userRole in user.Roles.Role)
            {
                Components.Server.Menu.Role role =
                    section.Roles.GetRoleByID(userRole.Name);
                if (role != null)
                {
                    if (!write)
                    {
                        if (role.Read.ToLower() == "true")
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (role.Write.ToLower() == "true")
                        {
                            return true;
                        }
                    }
                }
            }

            // the user might not have the rights for a specific section
            // then retrieve the current menu from the section to see
            // if the user has writes on a specific file
            Components.Server.Menu.Menu menu =
                section.Menus.GetMenuByName("../../" + sectionName + "/" + pageName);

            // loop throug the menu and check if we have the right on the specified
            // file
            if (menu != null)
            {
                foreach (Components.Server.User.Role userRole in user.Roles.Role)
                {
                    Components.Server.Menu.Role role =
                        menu.Roles.GetRoleByID(userRole.Name);
                    if (role != null)
                    {
                        if (!write)
                        {
                            if (role.Read.ToLower() == "true")
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (role.Write.ToLower() == "true")
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        // check access (current page and other pages)
        public bool IsReadAccess()
        {
            return IsAccess(false, null);
        }

        public bool IsReadAccess(string absolutePage)
        {
            return IsAccess(false, absolutePage);
        }

        public bool IsReadAccess(EFundraisingCRMWebBasePage page)
        {
            // todo, check out namespace, convert to absolute page
            throw new NotImplementedException("Not yet implemented");
        }

        public bool IsWriteAccess()
        {
            return IsAccess(true, null);
        }

        public bool IsWriteAccess(string absolutePage)
        {
            return IsAccess(true, absolutePage);
        }

        public bool IsWriteAccess(EFundraisingCRMWebBasePage page)
        {
            // todo, check out namespace, convert to absolute page
            throw new NotImplementedException("Not yet implemented");
        }

        public void MessageBox(string message)
        {
            Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('" + message.Replace("'", "\'") + "');</script>");
        }

    }
    #endregion

    #region Project Base Page
    public class EFundraisingCRMWebBasePage : EFundraisingWebBasePage
    {

        protected override void OnInit(EventArgs e)
        {

            Session["isProd"] = "0";
            bool isProd = Convert.ToBoolean(Components.Server.ManageSaleScreen.GetValueFromWebConfig("EFundraisingProd.Production", "isProduction"));
            if (isProd)
            {
                Session["isProd"] = "1";
            }
            // set all application cached values
            SetCachedValues();

            Guid Session_id = Guid.NewGuid();
            Session["SessionID"] = Session_id.ToString();

            bool authentication =
                (bool)(Convert.ToBoolean(efundraising.EnterpriseComponents.Helper.GetWebConfigValue("IntegratedLogin")));

            System.IO.FileInfo info = new FileInfo(Server.MapPath(Request.Url.AbsolutePath));
            if (info.Name.ToLower() == "FromIntranetweb.aspx".ToLower()
                || info.Name.ToLower() == "Login.aspx".ToLower())
            {
                authentication = false;
            }

            string url = Request.Url.AbsolutePath;
            string[] urlSplitted = url.Split('/');
            if (urlSplitted.Length == 3)
            {
                authentication = false;
            }
            
            /*string username = "";
            if (efundraising.Utilities.CookieHandler.CookieHandler.IsCookieEnable(System.Web.HttpContext.Current.Request))
            {
                if (CookieHandler.CookieExists(System.Web.HttpContext.Current.Request, Components.Server.ApplicationConstants.AuthenticationCookiePassword))
                {
                    username = CookieHandler.CookieValue(System.Web.HttpContext.Current.Request, Components.Server.ApplicationConstants.AuthenticationCookieName);
                    Session[Global.SessionVariables.USER_NAME] = username;
                }
            }*/
            //removed cookie on 5-19*2010
          /*  WindowsIdentity user = WindowsIdentity.GetCurrent();
            string name = user.Name.Replace("TIME-INC-CORP\\", "");
            string username = name;*/
            string username = "";
            if (Session[Global.SessionVariables.USER_NAME] != null)
            {
                username = Session[Global.SessionVariables.USER_NAME].ToString();
            }

            Components.Server.User.CrmUser crmUser;
      
            
            /*      if (username == "mlemire")
            {
                // set user information (dev)
                if (Components.Server.User.CrmUser.Create(Session) == null)
                {
                     crmUser =
                        new Components.Server.User.CrmUser(int.MinValue, "mlemire", "mlemire@qsp.com");
                    crmUser.Roles = new Components.Server.User.Roles();
                    crmUser.Roles.AddRole(new Components.Server.User.Role("gCAEFR_SalesSupport"));
                    crmUser.Save(Session);
                 }
            }
            
            else*/ if (!authentication)
            {
                // set current user with roles
                SetUser();
                efundraising.EFundraisingCRM.Consultant consultant = efundraising.EFundraisingCRM.Consultant.GetConsultantByNtLogin(username);
                if (consultant == null)
                {
                    Session[Global.SessionVariables.CONSULTANT_ID] = 0;
                }
                else
                {
                    Session[Global.SessionVariables.CONSULTANT_ID] = consultant.ConsultantId;
                }
                
        
            }
            else
            {
                SetSecurity();
                
                crmUser = Components.Server.User.CrmUser.Create(Session);
                Components.Server.User.Roles roles = crmUser.Roles;
                Session[Global.SessionVariables.ROLES] = crmUser.Roles;
                efundraising.EFundraisingCRM.Consultant consultant = efundraising.EFundraisingCRM.Consultant.GetConsultantByNtLogin(crmUser.Name);
                if (consultant == null)
                {
                    Session[Global.SessionVariables.CONSULTANT_ID] = 8;
                }
                else
                {
                    Session[Global.SessionVariables.CONSULTANT_ID] = consultant.ConsultantId;
                }
            }
            bool isConsultant = Components.Server.ManageSaleScreen.IsConsultant();


            // loop through control page to apply frame properties
            // to user controls that implements IFrame
            if (HasControls())
            {
                for (int i = 0; i < Controls.Count; i++)
                {
                    Control c = Controls[i];
                    ParseControl(c);
                }

                for (int i = 0; i < Controls.Count; i++)
                {
                    Control c = Controls[i];
                    ParseControlToSetFeedback(c);
                }
            }

            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            try
            {
                /*  if (Controls[0].FindControl("cph_PageContent") != null)
                  {
                      bool isWrite = IsWriteAccess();
                      if (!isWrite)
                      {
                          System.Web.UI.WebControls.Panel pl =
                              (System.Web.UI.WebControls.Panel)Controls[0].FindControl("cph_PageContent");
                          if (pl.HasControls())
                          {
                              for (int i = 0; i < pl.Controls.Count; i++)
                              {
                                  ParseControlToDisable(pl.Controls[i]);
                              }
                          }
                      }

                      System.Web.UI.WebControls.Panel panel =
                          (System.Web.UI.WebControls.Panel)Controls[0].FindControl("cph_PageContent");
                      if (panel.HasControls())
                      {
                          for (int i = 0; i < panel.Controls.Count; i++)
                          {
                              ParseControlToSetFeedback(panel.Controls[i]);
                          }
                      }


     


                  }*/
                base.OnPreRender(e);
            }
            catch (Exception ex)
            {
                // error occurs here because of akax contrl extender inside a master page......
                string a = ex.Message;
            }
        }


        #region User Information

        virtual protected void SetSecurity()
        {
       
            Components.Server.User.CrmUser crmUser =
                Components.Server.User.CrmUser.Create(Session);
            if (crmUser == null)
            {
                Logger.Error("line crm 1" + crmUser); 
                if (efundraising.EnterpriseComponents.Helper.GetWebConfigValue("ForceLogin").ToLower() == "false")
                {
                  /*  WindowsIdentity user = WindowsIdentity.GetCurrent();
                    string username = user.Name;
              */
                    Logger.Error("inside ForceLogin = false " + crmUser);
                    
                           if (efundraising.Utilities.CookieHandler.CookieHandler.IsCookieEnable(System.Web.HttpContext.Current.Request))
                          {
                              Logger.Error("inside IsCookieEnable");
                              Logger.Error("The cookieExist value is = " + System.Web.HttpContext.Current.Request + "---" + Components.Server.ApplicationConstants.AuthenticationCookieName);
                              Logger.Error("The cookieExist value is = " + CookieHandler.CookieExists(System.Web.HttpContext.Current.Request, Components.Server.ApplicationConstants.AuthenticationCookieName));
                               if (CookieHandler.CookieExists(System.Web.HttpContext.Current.Request, Components.Server.ApplicationConstants.AuthenticationCookieName))
                              {                    
                                 
                                 string username = CookieHandler.CookieValue(System.Web.HttpContext.Current.Request, Components.Server.ApplicationConstants.AuthenticationCookieName);
                                 Logger.Error("username=" + username ); 
                string password = "";//CookieHandler.CookieValue(System.Web.HttpContext.Current.Request, Components.Server.ApplicationConstants.AuthenticationCookiePassword);
                if (password != ""){
                        efundraising.Utilities.Encryption.DES.TripleDES tripleDES =
                            new efundraising.Utilities.Encryption.DES.TripleDES();
                        password = tripleDES.Decrypt(password, Components.Server.ApplicationConstants.EncryptionKey);
                        Logger.Error("password=" + password);
                        //  password = "qwerty1";
                        crmUser = Components.Server.User.CrmUser.FromIntegratedLogin(username, password);
                        Logger.Error("crmUser=" + crmUser);    
                    if (crmUser == null)
                        {
                            Logger.Error("line crm 200"); 
                            Response.Redirect("../../CrmLogin.aspx");
                        }
                        else
                        {
                            crmUser.Save(Session);
                        }
                    }
                                
                            
                        }
                    }
                }

                if (crmUser == null)
                {
                    Components.Server.CurrentWorkingObject.Save(Request.Url.AbsoluteUri, System.Web.HttpContext.Current.Session, "_REDIRECTION_FROM_SECURITY_", null);
                    if (Config.IsEFundraisingProdProduction)
                    {
                        Logger.Error("line crm 300");
                        Response.Redirect("../../CrmLogin.aspx");
                    }
                    else
                    {
                        Logger.Error("line crm 400");
                        Response.Redirect("../../CrmLogin.aspx");
                    }
                }
            } //if user == null

            if (crmUser.Name.ToLower() == "testuser")
            {
                Components.Server.CurrentWorkingObject.Save("YES", System.Web.HttpContext.Current.Session, "_WRITE_ACCESS_TO_THIS_PAGE_", null);
            }
            else
            {
                if (!IsReadAccess())
                {
                    Logger.Error("line crm 500");
                    Response.Redirect("../../CrmLogin.aspx");
                }

                if (!IsWriteAccess())
                {
                    Components.Server.CurrentWorkingObject.Save("NO", System.Web.HttpContext.Current.Session, "_WRITE_ACCESS_TO_THIS_PAGE_", null);
                }
                else
                {
                    Components.Server.CurrentWorkingObject.Save("YES", System.Web.HttpContext.Current.Session, "_WRITE_ACCESS_TO_THIS_PAGE_", null);
                }
            }
        }

        virtual protected void SetUser()
        {
            // set user information (dev)
            if (Components.Server.User.CrmUser.Create(Session) == null)
            {
                Components.Server.User.CrmUser crmUser =
                    new Components.Server.User.CrmUser(int.MinValue, "jbuist", "jbuist@qsp.com");
                crmUser.Roles = new Components.Server.User.Roles();
                crmUser.Roles.AddRole(new Components.Server.User.Role("gCAEFR_MIS-developers", "gCAEFR_MIS-developers"));
                crmUser.Save(Session);
            }
        }



        #endregion

        #region Page Information Method
        public string GetPageInformation()
        {
            if (this is IPage)
            {
                IPage page = (IPage)this;
                return page.PageInformation;
            }
            else
            {
                throw new Exception("This page must implement the IPage interface");
            }
        }

        public string GetPageDescription()
        {
            if (this is IPage)
            {
                IPage page = (IPage)this;
                return page.PageDescription;
            }
            else
            {
                throw new Exception("This page must implement the IPage interface");
            }
        }
        #endregion

        #region Cached Values
        private void SetCachedValues()
        {
            // load the menu from xml and save the objects in the application cache
            Components.Server.Menu.eFundraisingCrmWebMenu eFundMenu =
                Components.Server.Menu.eFundraisingCrmWebMenu.Create(Cache);
            if (eFundMenu == null)
            {
                eFundMenu = new Components.Server.Menu.eFundraisingCrmWebMenu();
                string filename = Server.MapPath("~/Ressources/Xml/Menu/Menu.xml");
                eFundMenu.Load(filename);
                eFundMenu.Save(Cache, filename);
            }
        }
        #endregion

        #region Add Frame User Control

        private void ParseControl(Control control)
        {
            if (control.HasControls())
            {
                for (int i = 0; i < control.Controls.Count; i++)
                {
                    Control c = control.Controls[i];
                    if (IsFrame(c))
                    {
                        Components.User.CommonFrame frame = GetCommonFrameControl((System.Web.UI.UserControl)c);
                        // control.Controls.RemoveAt(i);
                        control.Controls.AddAt(i, frame);
                    }

                    ParseControl(c);
                }
            }
        }

        private void ParseControlToDisable(Control control)
        {
            if (control.HasControls())
            {
                for (int i = 0; i < control.Controls.Count; i++)
                {
                    Control c = control.Controls[i];
                    if (c is System.Web.UI.WebControls.Button ||
                        c is System.Web.UI.WebControls.LinkButton ||
                        c is System.Web.UI.WebControls.ImageButton)
                    {
                        DisableControl((System.Web.UI.WebControls.WebControl)c);
                    }

                    ParseControlToDisable(c);
                }
            }
        }

        private void ParseControlToSetFeedback(Control control)
        {
            if (control.HasControls())
            {
                for (int i = 0; i < control.Controls.Count; i++)
                {
                    Control c = control.Controls[i];
                    if (c is System.Web.UI.WebControls.Button ||
                        c is System.Web.UI.WebControls.LinkButton ||
                        c is System.Web.UI.WebControls.ImageButton)
                    {
                        WebControl theControl = ((System.Web.UI.WebControls.WebControl)c);
                        if (theControl.Attributes[EFundraisingWebBasePage.NoValidation] == null)
                            theControl.Attributes.Add("OnClick", "if(typeof(ShowFeedBackForm) == 'function')ShowFeedBackForm(); ");
                    }

                    ParseControlToSetFeedback(c);
                }
            }
        }

        private void DisableControl(System.Web.UI.WebControls.WebControl wc)
        {
            wc.Enabled = false;
        }

        private bool IsFrame(Control c)
        {
            if (c is IFrame)
            {
                return true;
            }
            return false;
        }

        private Components.User.CommonFrame GetCommonFrameControl(System.Web.UI.UserControl uc)
        {
            Components.User.CommonFrame cf =
                (Components.User.CommonFrame)LoadControl("../../Components/User/CommonFrame.ascx");
            cf.AddControl(uc);
            IFrame frame = (IFrame)uc;
            cf.SetTitle(frame.Title);
            return cf;
        }

        #endregion

        #region Public Section
        public String PostBackTarget
        {
            get
            {
                return Request.Form["__EVENTTARGET"];
            }
        }

        public String PostBackArgument
        {
            get
            {
                return Request.Form["__EVENTARGUMENT"];
            }

        }



        public UserInfo CurrentUserInfo
        {
            get
            {
                return (UserInfo)Session[Global.SessionVariables.INTRANETWEBUSER];
            }
        }

        public void CreateUserInfo()
        {
            Session[Global.SessionVariables.INTRANETWEBUSER] = new UserInfo(Global.SessionVariables.INTRANETWEBUSER, "all");
        }


        public efundraising.EFundraisingCRM.PaymentMethod[] GetPaymentMethod()
        {
            if (Session["GETPAYMENTMETHOD"] == null)
            {
                return efundraising.EFundraisingCRM.PaymentMethod.GetPaymentMethods();
            }

            return (efundraising.EFundraisingCRM.PaymentMethod[])Session["GETPAYMENTMETHOD"];
        }


        public efundraising.EFundraisingCRM.PaymentTerm[] GetPaymentTerm()
        {
            if (Session["GETPAYMENTTERM"] == null)
            {
                return efundraising.EFundraisingCRM.PaymentTerm.GetPaymentTerms();
            }

            return (efundraising.EFundraisingCRM.PaymentTerm[])Session["GETPAYMENTTERM"];
        }

        // use to get a Literal control containing text (can be html)
        public Literal GetLiteral(string text)
        {
            Literal newLiteral = new Literal();
            newLiteral.Text = text;
            return newLiteral;
        }

        #endregion

        #region Private Section

        //		protected string userName
        //		{
        //			get 
        //			{
        //				if (Request["un"] == null)
        //					return string.Empty;
        //				return (string)Request["un"];
        //			}
        //		}
        //		
        //		protected string userRole
        //		{
        //			get 
        //			{
        //				if (Request["ur"] == null)
        //					return string.Empty;
        //				return (string)Request["ur"];
        //			}
        //		}

        private CustomPrincipal GetCustomPrincipalFromCookie()
        {

            CustomPrincipal custP = null;
            string roles = GetRolesFromCookie();
            string uName = GetUserNameFromCookie();
            if (roles != null && roles != string.Empty)
            {
                custP = CustomPrincipal.CreateCustomPrincipal(uName, roles);
                return custP;
            }
            return custP;
        }

        private void StoreCustomPrincipalIntoCookie(CustomPrincipal custP)
        {
            if (custP != null)
            {
                CustomIdentity custIden = custP.Identity as CustomIdentity;
                if (custIden != null)
                {
                    StoreRolesInCookie(custIden.UserRoles);
                    StoreUserNameInCookie(custIden.Name);
                }
            }
        }

        #endregion

        #region Protected Section

        //		protected string requiredParameter
        //		{
        //			
        //			get {return (string)Session["requiredParameter"]; }
        //			set {Session["requiredParameter"]= value;}
        //		}


        protected Sale oldSale
        {
            get { return (Sale)Session["oldSale"]; }
            set { Session["oldSale"] = value; }
        }

        protected readonly string cookieRole = "ROLES";
        protected readonly string cookieUserName = "NAME";
        protected IIdentity CurrentIdentity
        {
            get
            {
                return (IIdentity)Session["CurrentIdentity"];
            }
            set
            {
                Session["CurrentIdentity"] = value;
            }
        }

        protected string GetRolesFromCookie()
        {
            string role = string.Empty;
            efundraising.Utilities.Encryption.DES.TripleDES tripleDES = new efundraising.Utilities.Encryption.DES.TripleDES();
            // Get user's roles from encrypted Cookie
            if (CookieHandler.CookieExists(Request, cookieRole))
                role = tripleDES.Decrypt(CookieHandler.CookieValue(Request, cookieRole), "1n7R4N37");
            return role;
        }


        protected string GetUserNameFromCookie()
        {
            string role = string.Empty;
            efundraising.Utilities.Encryption.DES.TripleDES tripleDES = new efundraising.Utilities.Encryption.DES.TripleDES();
            // Get user's roles from encrypted Cookie
            if (CookieHandler.CookieExists(Request, cookieUserName))
                CookieHandler.CookieValue(Request, cookieUserName);
            return role;
        }


        protected void StoreUserNameInCookie(string uName)
        {
          //  CookieHandler.SetCookie(Request, Response, cookieUserName, uName, DateTime.Now.AddHours(8));
        }

        protected void StoreRolesInCookie(string roles)
        {
          //  efundraising.Utilities.Encryption.DES.TripleDES tripleDES = new efundraising.Utilities.Encryption.DES.TripleDES();
          //  CookieHandler.SetCookie(Request, Response, cookieRole, tripleDES.Encrypt(roles, "1n7R4N37"), DateTime.Now.AddHours(8));
        }

        protected CustomPrincipal GetCurrentCustomPrincipal()
        {

            CustomPrincipal custP = null;
            if (CurrentIdentity != null)
            {
                CustomIdentity custIden = CurrentIdentity as CustomIdentity;
                if (custIden != null)
                {
                    custP = new CustomPrincipal(custIden, custIden.UserRoles.Split('|'));
                }
            }

            if (custP == null)
                custP = CustomPrincipal.CreateCustomPrincipal(Request, "un", "ur");

            return custP;
        }


        protected virtual string GetPageTitle()
        {
            return "Reader Digest Fundraising";
        }


        protected override void RenderChildren(System.Web.UI.HtmlTextWriter writer)
        {
            try
            {
                Literal theControl = FindControl("StyleSheetLiteral") as Literal;
                if (theControl != null)
                    theControl.Text = string.Format(
                        "<LINK href=\"{0}\" type=\"text/css\" rel=\"stylesheet\">", appPath + "Resources/Css/style.css");

                theControl = FindControl("TitleLiteral") as Literal;
                if (theControl != null)
                    theControl.Text = GetPageTitle();
            }
            catch (Exception)
            {
            }
            base.RenderChildren(writer);
        }


        #endregion

        #region ViewState Compression




        // Save the viewstate information with compression.
        virtual protected void SaveCompressedPageStateToPersistenceMedium(object viewState)
        {
            // Used to serialize the viewstate
            LosFormatter formatter = new LosFormatter();
            StringWriter sw = new StringWriter();
            formatter.Serialize(sw, viewState);
            // Compress the viewstate information.
            string outStr = Compression.Compress(sw.ToString());
            // Store in the hidden field __CUSTOMVIEWSTATE
            Page.RegisterHiddenField("__CUSTOMVIEWSTATE", outStr);
        }
        // Load the viewstate with compression
        virtual protected object LoadCompressedPageStateFromPersistenceMedium()
        {
            // Used to Deserialize the viewstate
            LosFormatter formatter = new LosFormatter();
            // Get the compressed viewstate from the hidden field.
            string vsString = Request.Form["__CUSTOMVIEWSTATE"];
            // Deserialize the viewstate.
            string outStr = Compression.DeCompress(vsString);
            return formatter.Deserialize(outStr);
        }



        private bool IsApplyViewStateCompressed
        {
            get
            {
                bool bApplyViewStateCompressed = false;
                try
                {
                    bApplyViewStateCompressed = bool.Parse(efundraising.EnterpriseComponents.Helper.GetWebConfigValue("ApplyViewStateCompressed"));
                }
                catch (Exception)
                {
                    bApplyViewStateCompressed = false;
                }
                return bApplyViewStateCompressed;
            }
        }
        // TO ACTIVATE THE VIEWSTATE COMPRESSION:
        // 1. SET Web.config <add key="ApplyViewStateCompressed" value="true" />
        // 2. Only those pages that are devired from the IViewStateCompression interface, the viewstate information will be compressed.
        // Override those functions to Save and Load the ViewState.		
        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            // Check if the page derived from IViewStateCompression
            IViewStateCompression theViewStateCompress = this.Page as IViewStateCompression;
            if (theViewStateCompress != null && IsApplyViewStateCompressed) // If yes, Save the viewstate with compression
            {
                SaveCompressedPageStateToPersistenceMedium(viewState);
            }
            else // If no, call the base class
                base.SavePageStateToPersistenceMedium(viewState);
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            // Check if the page derived from IViewStateCompression
            IViewStateCompression theViewStateCompress = this.Page as IViewStateCompression;
            // If yes, Load the viewstate with compression
            if (theViewStateCompress != null && IsApplyViewStateCompressed)
            {
                return LoadCompressedPageStateFromPersistenceMedium();
            }
            // If no, call the base class
            return base.LoadPageStateFromPersistenceMedium();
        }



        #endregion
    }
    #endregion

}
