//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_Login_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'Login.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Security.Principal;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web
{
    /// <summary>
    /// Summary description for Login.
    /// </summary>
    public partial class Login : BaseLogin
    {
        private const string MSGPASUSERVALID = "Your User Name and your Password are valid, but you don't have any Campaign Associated<BR>Please contact the System Administrator.";
        private const string MSGACCESSDENIED = "Access&nbsp;denied,&nbsp;please&nbsp;verify&nbsp;your&nbsp;User&nbsp;Name&nbsp;and&nbsp;your&nbsp;Password.<br>Forgot&nbsp;your&nbsp;Password?&nbsp;&nbsp;<br>Please&nbsp;call&nbsp;<nobr>1-866-238-3272</nobr>,&nbsp;9&nbsp;am&nbsp;to&nbsp;5&nbsp;pm&nbsp;EST<br>&nbsp;or&nbsp;<A href='mailto:QDSFieldSupport@QSP.Com?subject=QSP%20Field%20Support:%20Order%20Express'>Click&nbsp;here</A>&nbsp;to&nbsp;send&nbsp;an&nbsp;email&nbsp;for&nbsp;assistance.";
        private const string MSGFMACCESSDENIED = "Access denied, please verify your User Name and your Password.<br />Forgot your Password?  Please call 1-866-238-3272, 9 am to 5 pm EST <br> or email <A href='mailto:QDSFieldSupport@QSP.Com?subject=QSP%20Field%20Support:%20Order%20Express'>QDSFieldSupport@QSP.Com</A>";
        private const string MSGACCESSGRANTED = "Access Granted<BR>You will redirect to the Default Page in few seconds.";
        private const string MSGALLOWCOOKIE = "Your browser must allow cookies";
        private const string MSGSUPPORTJAVASCRIPT = "Your browser must support JavaScript";
        private const string MSGACCESSDENIED_SYSUSER = "Access denied, user with system process role cannot access to the site.";
        protected System.Web.UI.WebControls.Image Image2;

        protected void Page_Load(object sender, System.EventArgs e)
        {

            this.Master.ValSummaryVisibility = false;
            this.Master.BusyBoxVisible = false;

            // Put user code to initialize the page here
            if (!IsPostBack)
            {
                this.Page.SetFocus(this.txtLogin);
                //CommonUtility.SetInitialFocus(this.txtLogin);
            }
            //to reset the Server error message
            imgbtnLogin.Attributes.Add("onclick", "document.all." + lblMessage.ClientID + ".innerHTML = '';");

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imgbtnLogin.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnLogin_Click);

        }
        #endregion

        protected override void InitControl()
        {
            //this.AppItem = QSPForm.Business.AppItem.Login;			
            //base.LabelMessage = lblMessage;
            //base.LabelInstruction = lblInstruction;
            base.InitControl();
            VerifyLogin();
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
        }

        private void imgbtnLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (!Request.Browser.Cookies)
            {
                lblMessage.Text = MSGALLOWCOOKIE;
                return;
            }

            if (!Request.Browser.JavaScript)
            {
                lblMessage.Text = MSGSUPPORTJAVASCRIPT;
                return;
            }

            this.Validate();
            if (!this.IsValid)
            {
                return;
            }
            string userName = txtLogin.Text.Trim();
            string pwd = txtPwd.Text.Trim();
            DoLogin(AuthSystem.MODE_LOGIN_USER, userName, pwd, this.GetPageToGo(QSPForm.Business.AppItem.Default));
        }

        private string QueryUserName
        {
            get
            {
                if (this.Request.QueryString["U"] != null)
                    return this.Request.QueryString["U"].ToString();
                else
                    return String.Empty;
            }
        }

        private string QueryPassword
        {
            get
            {
                if (this.Request.QueryString["P"] != null)
                    return this.Request.QueryString["P"].ToString();
                else
                    return String.Empty;
            }
        }

        private string QueryDestination
        {
            get
            {
                if (this.Request.QueryString["D"] != null)
                    return this.Request.QueryString["D"].ToString();
                else
                    return String.Empty;
            }
        }

        private string QueryOrderID
        {
            get
            {
                if (this.Request.QueryString["O"] != null)
                    return this.Request.QueryString["O"].ToString();
                else
                    return String.Empty;
            }
        }

        private void VerifyLogin()
        {
            // verify if it come from account track
            if ((QueryUserName != String.Empty) && (QueryPassword != String.Empty) && (QueryDestination != String.Empty))
            {
                string userName = QueryUserName;
                string password = QueryPassword;
                string orderID = QueryOrderID;
                QSPForm.Business.CryptoSystem cryp = new CryptoSystem();

                userName = cryp.Decrypt(userName);
                password = cryp.Decrypt(password);
                orderID = cryp.Decrypt(orderID);

                //verify if values are ok, else show error message
                if (((userName != String.Empty) && (password != String.Empty)) && ((QueryDestination != String.Empty) || (orderID != String.Empty)))
                {
                    if (QueryOrderID != String.Empty)
                    {
                        DoLogin(AuthSystem.MODE_LOGIN_FM, userName, password, this.GetPageToGo(QSPForm.Business.AppItem.OrderDetailInfo) + "&OrderID=" + orderID);
                    }
                    else
                    {
                        DoLogin(AuthSystem.MODE_LOGIN_FM, userName, password, QueryDestination);
                    }
                }
                else
                {
                    //lblMessage.Text = MSGACCESSDENIED;
                    SetPageError(new Exception(MSGACCESSDENIED));
                }
            }
        }

        private void DoLogin(int ModeLogin, string userName, string pwd, string url)
        {
            try
            {
                //int ModeLogin  = AuthSystem.MODE_LOGIN_USER;

                int ID = 0;

                //				string userName = txtLogin.Text.Trim();
                //				string pwd = txtPwd.Text.Trim();
                QSPForm.Business.AuthSystem authSys = new QSPForm.Business.AuthSystem();
                ID = authSys.QSPForm_Authentication(userName, pwd, ModeLogin);

                //if User is found in DB
                if (ID != 0)
                {
                    //Store the info in the session via the base page
                    UserID = ID;
                    this.Role = authSys.Role;
                    this.FMID = authSys.FM_ID;
                    this.RegistryID = authSys.RegistryID;

                    //Only Role > 0 can be access to the site
                    //Below zero, this a System Role process
                    if (this.Role > 0)
                    {
                        if (Request.Params["ReturnUrl"] != null)
                        {
                            #region Create logged user object

                            QSPForm.Business.UserSystem userSystem = new UserSystem();

                            QSP.OrderExpress.Business.Entity.User user = userSystem.GetUser(UserID);
                            QSP.OrderExpress.Business.Entity.Role userRole = userSystem.GetRole(user.RoleId.Value);

                            LoggedUser loggedUser = new LoggedUser();
                            loggedUser.UserId = user.UserId;
                            loggedUser.UserName = user.UserName;
                            loggedUser.FirstName = user.FirstName;
                            loggedUser.LastName = user.LastName;
                            loggedUser.Email = user.Email;
                            loggedUser.FMId = this.FMID;
                            loggedUser.UserTypeId = userRole.RoleId;
                            loggedUser.UserTypeDescription = userRole.Name;

                            Session["LoggedUser"] = loggedUser;

                            #endregion

                            FormsAuthentication.RedirectFromLoginPage(userName, false);
                        }
                        else
                        {
                            // Redirect to start page wherever that may be
                            FormsAuthentication.SetAuthCookie(userName, false);

                            #region Create logged user object

                            QSPForm.Business.UserSystem userSystem = new UserSystem();

                            QSP.OrderExpress.Business.Entity.User user = userSystem.GetUser(UserID);
                            QSP.OrderExpress.Business.Entity.Role userRole = userSystem.GetRole(user.RoleId.Value);

                            LoggedUser loggedUser = new LoggedUser();
                            loggedUser.UserId = user.UserId;
                            loggedUser.UserName = user.UserName;
                            loggedUser.FirstName = user.FirstName;
                            loggedUser.LastName = user.LastName;
                            loggedUser.Email = user.Email;
                            loggedUser.FMId = this.FMID;
                            loggedUser.UserTypeId = userRole.RoleId;
                            loggedUser.UserTypeDescription = userRole.Name;

                            Session["LoggedUser"] = loggedUser;

                            #endregion

                            Response.Redirect("~/Default.aspx", false);
                        }
                    }
                    else
                    {
                        lblMessage.Text = MSGACCESSDENIED_SYSUSER;
                    }
                }
                else
                {
                    if (ModeLogin == AuthSystem.MODE_LOGIN_FM)
                    {
                        lblMessage.Text = MSGFMACCESSDENIED;
                    }
                    else
                    {
                        lblMessage.Text = MSGACCESSDENIED;
                    }
                }
            }
            catch (Exception ex)
            {
                SetPageError(ex);
            }
        }
    }
}