using System;
using System.Web;
using QSPForm.Common.DataDef;
using System.Data;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Web.Security;
using System.Web.SessionState;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>Base page for pages in QSPForm_Web</summary>
    /// <remarks>
    ///		Inherit from this page instead of System.Web.UI.Page
    ///		when you are ready to move from development to production.
    ///	</remarks>
    public class BasePage : System.Web.UI.Page {
        private const string CAMPIDNAME = "CampID";
        private const string IDNAME = "ID";
        private const string ROLENAME = QSPForm.Business.AuthSystem.ROLE;
        private const string FMIDNAME = "fm_id";
        private const string REGISTRYIDNAME = "Registry_ID";
        private const string CAMP_TABLE_INFO = "CampTableInfo";
        private const string USER_TABLE_INFO = "UserTableInfo";
        private const string PAGE_TABLE_INFO = "PageTableInfo";
        private const string CAMPAIGN_NAME = "CampaignName";
        private const string APPITEM = "AppItem";
        private int c_SetupIncrement = 0;
        protected DataTable c_dtAppItem;
        protected BaseMenuBar QSPFormMenuBar;
        //protected BaseSetup_NavBar QSPFormSetupNavBar;
        protected BaseSessionInfo QSPForm_SessionInfo;
        protected BaseFAQ_Displayer QSPForm_FAQ_Displayer;
        public event System.ComponentModel.CancelEventHandler MenuChange;
        public event System.EventHandler RefreshPage;
        public event System.ComponentModel.CancelEventHandler GoToStep;
        private System.Web.UI.HtmlControls.HtmlInputHidden hidRefresh;
        private bool hasError = false;
        private System.Web.UI.WebControls.Label lblMessage;
        private System.Web.UI.WebControls.Label lblInstruction;
        private System.Web.UI.WebControls.Label lblPageTitle;
        private System.Web.UI.WebControls.Label lblSectionTitle;
        private System.Web.UI.WebControls.Label lblDirectionTitle;
        private System.Web.UI.WebControls.Image imgIcon;
        private ValidationSummary ValSum;
        private QSPForm.Business.AppItem newAppItem;
        protected bool isFormHasToRefresh = false;
        private AppItemData dtsAppItem;
        //private QSP.WebControl.StaticPostBackPosition keepPostBackPosition;

        protected override void OnLoad(EventArgs e) {
            //Reset the Message label
            if (LabelMessage != null)
                LabelMessage.Text = "";
            //By default always set as true.
            //after this event in some case we will disable it
            this.Page.MaintainScrollPositionOnPostBack = true;


            QSPFormMenuBar = (BaseMenuBar)this.FindControl("QSPFormMenuBar");
            QSPForm_SessionInfo = (BaseSessionInfo)this.FindControl("QSPForm_SessionInfo");
            QSPForm_FAQ_Displayer = (BaseFAQ_Displayer)this.FindControl("QSPForm_FAQ_Displayer");
            if (QSPFormMenuBar != null) {
                this.QSPFormMenuBar.MenuChange += new System.ComponentModel.CancelEventHandler(this.QSPFormMenuBar_MenuChange);
            }

            if (hidRefresh != null) {
                this.hidRefresh.ServerChange += new EventHandler(hidRefresh_ServerChange);
            }

            base.OnLoad(e);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            try {
                InitControl();
                InitializeComponent();
                LoadData();
                base.OnInit(e);
                //EnsureSessionIntegrity();				
            }
            catch (Exception ex) {
                SetPageError(ex);
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            try {

            }
            catch {
            }
        }
        #endregion

        protected virtual void InitControl() {
        }

        public System.Web.UI.HtmlControls.HtmlInputHidden HiddenRefresh {
            get {
                return hidRefresh;
            }
            set {
                hidRefresh = value;
            }
        }

        public System.Web.UI.WebControls.Label LabelMessage {
            get {
                return lblMessage;
            }
            set {
                lblMessage = value;
            }
        }

        public System.Web.UI.WebControls.Label LabelInstruction {
            get {
                return lblInstruction;
            }
            set {
                lblInstruction = value;
            }
        }

        public System.Web.UI.WebControls.Label LabelPageTitle {
            get {
                return lblPageTitle;
            }
            set {
                lblPageTitle = value;
            }
        }

        public System.Web.UI.WebControls.Label LabelSectionTitle {
            get {
                return lblSectionTitle;
            }
            set {
                lblSectionTitle = value;
            }
        }

        public System.Web.UI.WebControls.Label LabelDirectionTitle {
            get {
                return lblDirectionTitle;
            }
            set {
                lblDirectionTitle = value;
            }
        }

        public System.Web.UI.WebControls.ValidationSummary ValSummary {
            get {
                return ValSum;
            }
            set {
                ValSum = value;
            }
        }

        public System.Web.UI.WebControls.Image ImageIcon {
            get {
                return imgIcon;
            }
            set {
                imgIcon = value;
            }
        }

        public bool IsPageHasToRefresh {
            get {
                return Convert.ToBoolean(Convert.ToInt32(hidRefresh.Value));
            }
            set {
                isFormHasToRefresh = value;
                if (isFormHasToRefresh)
                    hidRefresh.Value = "1";
                else
                    hidRefresh.Value = "0";
            }
        }

        protected override void OnPreRender(EventArgs e) {
            //Call the content management
            //Because this event will be called after all
            //Page or Web user control have been loaded
            //we will control all this Control from here
            //LoadData(AppItem);
            SetQSPFormMenuBar();

            if (!IsPostBack) {
                //Display Session Information
                //About the current user and the
                //current campaign
                SetSessionInfo();
                SetPageInformation();
            }
            //To save the latest version
            this.ViewState[PAGE_TABLE_INFO] = dtsAppItem;
            EnsureSessionIntegrity();
            base.OnPreRender(e);
        }

        protected override void OnDataBinding(EventArgs e) {
            LoadData();
            base.OnDataBinding(e);
        }

        private void QSPFormMenuBar_MenuChange(object sender, System.ComponentModel.CancelEventArgs e) {
            OnMenuChange(e);
        }

        protected virtual void OnMenuChange(System.ComponentModel.CancelEventArgs e) {
            if (MenuChange != null) {
                // Invokes the delegates. 
                MenuChange(this, e);
            }
            newAppItem = (QSPForm.Business.AppItem)QSPFormMenuBar.MenuItem;
            if (!e.Cancel) {
                string strPage = this.GetPageToGo(newAppItem);
                Response.Redirect(strPage);
            }
            else {
                QSPFormMenuBar.MenuItem = this.AppItem;
            }
        }

        protected virtual void OnRefreshPage(System.EventArgs e) {
            if (RefreshPage != null) {
                // Invokes the delegates. 
                RefreshPage(this, e);
            }
        }

        private void QSPFormSetupNavBar_GoToStep(object sender, System.Web.UI.ImageClickEventArgs e) {
            System.ComponentModel.CancelEventArgs eventArgs = new System.ComponentModel.CancelEventArgs();
            ImageButton source = (ImageButton)sender;
            c_SetupIncrement = Int32.Parse(source.CommandArgument.ToString());
            OnGoToStep(eventArgs);
        }

        protected virtual void OnGoToStep(System.ComponentModel.CancelEventArgs e) {
            if (GoToStep != null) {
                // Invokes the delegates. 
                GoToStep(this, e);
            }
            if (!e.Cancel) {
                newAppItem = GetStepToGo();
                string strPage = this.GetPageToGo(newAppItem);
                Response.Redirect(strPage);
            }
        }

        public string GetPageToGo(QSPForm.Business.AppItem NoMenu) {
            string url = "";
            QSPForm.Business.ContentManagerSystem CMSys = new QSPForm.Business.ContentManagerSystem();
            url = CMSys.GetAppItemToGo(NoMenu);

            return url;
        }

        public string GetPageToGo(QSPForm.Business.AppItem NoMenu, string paramName, string paramValue) {
            string url = "";

            CommonUtility clsUtil = new CommonUtility();
            url = clsUtil.GetPageUrl(NoMenu, paramName, paramValue);

            return url;
        }

        //public string GetPageToGo(string actualUrl, string paramName, string paramValue)
        //{
        //    string url = "";
        //       CommonUtility clsUtil = new CommonUtility();
        //     url = clsUtil.GetPageUrl(NoMenu, paramName, paramValue);
        //     return url;
        //}

        public QSPForm.Business.AppItem GetStepToGo() {
            int NoAppItem = Convert.ToInt32(QSPForm.Business.AppItem.Default);
            int Step = NoStep + c_SetupIncrement;
            QSPForm.Business.ContentManagerSystem CMSys = new QSPForm.Business.ContentManagerSystem();
            AppItemData appMenu;
            appMenu = CMSys.SelectOneNoStep(Step);
            DataTable dt;
            dt = appMenu.Tables[AppItemTable.TBL_APP_ITEM];

            if (dt.Rows.Count != 0) {
                DataRow drw;

                drw = dt.Rows[0];

                NoAppItem = Convert.ToInt32(drw[AppItemTable.FLD_NO]);
            }

            return (QSPForm.Business.AppItem)NoAppItem;
        }

        #region SetController

        public void SetSessionInfo() {
            if (QSPForm_SessionInfo != null) {
                QSPForm_SessionInfo.DataBind();

            }
        }

        private void SetFaqDisplayer() {
            //Display FAQ for the current page
            if (QSPForm_FAQ_Displayer != null) {

                QSPForm_FAQ_Displayer.DataBind();
            }
        }

        private void SetQSPFormMenuBar() {
            if (QSPFormMenuBar != null) {
                QSPFormMenuBar.MenuItem = AppItem;
                //QSPFormMenuBar.DisplayMenu = c_DisplayMenu;
                QSPFormMenuBar.DataBind();
            }
        }

        #endregion

        #region infoUser

        public int RegistryID {
            get {
                try {
                    return Convert.ToInt32(HttpContext.Current.Session[REGISTRYIDNAME]);
                }
                catch {

                    return -1;
                }
            }
            set {
                HttpContext.Current.Session[REGISTRYIDNAME] = value;
            }
        }

        /// <summary>
        /// Get the Role of the current user
        /// </summary>
        public string FMID {
            get {
                try {
                    return HttpContext.Current.Session[FMIDNAME].ToString();
                }
                catch {
                    //return default role
                    return "";
                }
            }
            set {
                HttpContext.Current.Session[FMIDNAME] = value;
            }
        }

        /// <summary>
        /// Get the Role of the current user
        /// </summary>
        public int Role {
            get {
                try {
                    return Convert.ToInt32(HttpContext.Current.Session[ROLENAME]);
                }
                catch {
                    //return default role
                    return 0;
                }
            }
            set {
                HttpContext.Current.Session[ROLENAME] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsIDNull() {
            try {
                return HttpContext.Current.Session[IDNAME] == null;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsRoleNull() {
            try {
                return HttpContext.Current.Session[CAMPIDNAME] == null;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// Get the ID of the current user
        /// </summary>
        public int UserID {
            get {
                try {
                    return Convert.ToInt32(HttpContext.Current.Session[IDNAME]);
                }
                catch {
                    return 0;
                }
            }
            set {
                HttpContext.Current.Session[IDNAME] = value;
            }
        }

        public void setCampIDToNull() {
            try {
                HttpContext.Current.Session[CAMPIDNAME] = null;
            }
            catch {

            }
        }

        public void setIDToNull() {
            try {
                HttpContext.Current.Session[IDNAME] = null;
            }
            catch { }
        }

        public void setRoleToNull() {
            try {
                HttpContext.Current.Session[ROLENAME] = null;
            }
            catch { }
        }

        #endregion

        public bool HasError {
            get {
                return hasError;
            }
        }

        #region ContentManagement

        /// <summary>
        /// Get the Information of the current page
        /// </summary>
        public QSPForm.Business.AppItem AppItem {
            get {
                if (this.ViewState[APPITEM] == null) {
                    return QSPForm.Business.AppItem.Default;
                }
                else {
                    int appItem = Convert.ToInt32(this.ViewState[APPITEM]);
                    return (QSPForm.Business.AppItem)appItem;
                }
            }
            set {
                int appItem = Convert.ToInt32(value);
                this.ViewState[APPITEM] = appItem;
            }
        }

        public QSPForm.Business.AppItem NewAppItem {
            get {
                return newAppItem;
            }
        }

        public void SetPageInformation() {
            SetFaqDisplayer();
            if (lblInstruction != null)
                lblInstruction.Text = Instruction;
            if (lblPageTitle != null)
                lblPageTitle.Text = PageTitle;
            if (lblSectionTitle != null)
                lblSectionTitle.Text = SectionTitle;
            if (imgIcon != null) {
                if (ImageTitleURL.Length > 0)
                    imgIcon.ImageUrl = ImageTitleURL;
                else
                    imgIcon.Visible = false;
            }
        }

        public void RefreshPageInformation() {
            //Display PAge Information			
            LoadData();
            SetPageInformation();

        }

        protected void LoadData() {
            int previousAppItem = -1;
            if (this.ViewState[PAGE_TABLE_INFO] != null) {
                dtsAppItem = (AppItemData)this.ViewState[PAGE_TABLE_INFO];
                DataTable dtAppItem = dtsAppItem.AppItem;
                if (dtAppItem.Rows.Count > 0) {
                    DataRow drw = dtAppItem.Rows[0];
                    previousAppItem = Convert.ToInt32(drw[AppItemTable.FLD_NO]);
                }
            }

            if (previousAppItem != (int)this.AppItem) {
                QSPForm.Business.ContentManagerSystem CMSys = new QSPForm.Business.ContentManagerSystem();
                dtsAppItem = CMSys.SelectAllByNoAppItem((int)this.AppItem);
                //Get Permission based on the Role and the NoAppItem via Entity Type ID
                dtsAppItem.Merge(CMSys.SelectAllPermissionsByNoAppItem((int)this.AppItem, this.Role));
                this.ViewState[PAGE_TABLE_INFO] = dtsAppItem;
            }

        }

        public string Title {
            get {
                string sValue = "";
                DataTable dtAppItem = dtsAppItem.AppItem;
                if (dtAppItem.Rows.Count > 0) {
                    DataRow drw = dtAppItem.Rows[0];
                    sValue = drw[AppItemTable.FLD_NAME].ToString();
                }
                return sValue;

            }
        }

        public string Description {
            get {
                string sValue = "";
                DataTable dtAppItem = dtsAppItem.AppItem;
                if (dtAppItem.Rows.Count > 0) {
                    DataRow drw = dtAppItem.Rows[0];
                    sValue = drw[AppItemTable.FLD_DESCRIPTION].ToString();
                }
                return sValue;
            }
        }

        public bool DisplayMenu {
            get {
                bool bValue = false;
                DataTable dtAppItem = dtsAppItem.AppItem;
                if (dtAppItem.Rows.Count > 0) {
                    DataRow drw = dtAppItem.Rows[0];
                    if (!drw.IsNull(AppItemTable.FLD_DISPLAY)) {
                        bValue = Convert.ToBoolean(drw[AppItemTable.FLD_DISPLAY]);
                    }

                }
                return bValue;
            }

        }

        public string Instruction {
            get {
                string sValue = "";
                DataTable dtAppItem = dtsAppItem.AppItem;
                if (dtAppItem.Rows.Count > 0) {
                    DataRow drw = dtAppItem.Rows[0];
                    sValue = drw[AppItemTable.FLD_INSTRUCTION].ToString();
                }
                return sValue;
            }
        }

        public string ImageTitleURL {
            get {
                string sValue = "";
                DataTable dtAppItem = dtsAppItem.AppItem;
                if (dtAppItem.Rows.Count > 0) {
                    DataRow drw = dtAppItem.Rows[0];
                    sValue = drw[AppItemTable.FLD_IMAGE_URL].ToString();
                }
                return sValue;
            }
        }

        public virtual string ControlURL {
            get {
                string sValue = "";
                DataTable dtAppItem = dtsAppItem.AppItem;
                if (dtAppItem.Rows.Count > 0) {
                    DataRow drw = dtAppItem.Rows[0];
                    sValue = drw[AppItemTable.FLD_CONTROL_URL].ToString();
                }
                return sValue;
            }
        }

        public int NoStep {
            get {
                int iValue = 0;
                DataTable dtAppItem = dtsAppItem.AppItem;
                if (dtAppItem.Rows.Count > 0) {
                    DataRow drw = dtAppItem.Rows[0];
                    if (drw[AppItemTable.FLD_NO_STEP] != System.DBNull.Value)
                        iValue = Convert.ToInt32(drw[AppItemTable.FLD_NO_STEP]);
                }
                return iValue;
            }
        }

        public bool IsSetupPage {
            get {
                bool bValue = false;
                DataTable dtAppItem = dtsAppItem.AppItem;
                if (dtAppItem.Rows.Count > 0) {
                    DataRow drw = dtAppItem.Rows[0];
                    if (drw[AppItemTable.FLD_NO_STEP] != System.DBNull.Value) {
                        int NoStep = Convert.ToInt32(drw[AppItemTable.FLD_NO_STEP]);
                        if (NoStep > 0)
                            bValue = true;
                    }
                }
                return bValue;
            }
        }

        public string PageTitle {
            get {
                string sValue = "";
                DataTable dtAppItem = dtsAppItem.AppItem;
                if (dtAppItem.Rows.Count > 0) {
                    DataRow drw = dtAppItem.Rows[0];
                    sValue = drw[AppItemTable.FLD_PAGE_TITLE].ToString();
                }
                return sValue;
            }
        }

        public string SectionTitle {
            get {
                string sValue = "";
                DataTable dtAppItem = dtsAppItem.AppItem;
                if (dtAppItem.Rows.Count > 0) {
                    DataRow drw = dtAppItem.Rows[0];
                    sValue = drw[AppItemTable.FLD_SECTION_TITLE].ToString();
                }
                return sValue;
            }
        }

        public AppItemTable PageTableInfo {
            get {
                return dtsAppItem.AppItem; ;
            }
        }

        public RolePermissionTable PermissionInfo {
            get {
                return dtsAppItem.RolePermission;
            }
        }

        public bool RightView {
            get {
                return GetRight(RolePermissionTable.FLD_RIGHT_VIEW);
            }
        }

        public bool RightInsert {
            get {
                return GetRight(RolePermissionTable.FLD_RIGHT_INSERT);
            }
        }

        public bool RightUpdate {
            get {
                return GetRight(RolePermissionTable.FLD_RIGHT_UPDATE);
            }
        }

        public bool RightDelete {
            get {
                return GetRight(RolePermissionTable.FLD_RIGHT_DELETE);
            }
        }

        private bool GetRight(string sRight) {
            bool blnRight = false;
            if (dtsAppItem != null)
                if (dtsAppItem.RolePermission != null)
                    if (dtsAppItem.RolePermission.Columns.Contains(sRight)) {
                        if (dtsAppItem.RolePermission.Rows.Count > 0) {
                            DataRow row = dtsAppItem.RolePermission.Rows[0];
                            if (!row.IsNull(sRight))
                                blnRight = Convert.ToBoolean(row[sRight]);
                        }
                    }
            return blnRight;
        }

        #endregion
        #region Page Error Management

        /// <summary>
        /// Set Error Message to the lblMessage
        /// </summary>
        /// <param name="Buisness"></param>
        private void PrintErrorMessage(QSPForm.Common.QSPFormException ex) {
            Label lblMessage = (Label)this.FindControl("lblMessage");
            if (lblMessage != null) {
                if (ex.HTMLMessage != String.Empty) {
                    lblMessage.Text = ex.HTMLMessage;
                }
                else {
                    lblMessage.Text = ex.Message;
                }
                lblMessage.Visible = true;

            }
            else {
                if (this.lblMessage != null) {
                    if (ex.HTMLMessage != String.Empty) {
                        this.lblMessage.Text = ex.HTMLMessage;
                    }
                    else {
                        this.lblMessage.Text = ex.Message;
                    }
                    this.lblMessage.Visible = true;
                }
            }
        }

        public void SetPageMessage(string msg) {
            Label lblMessage = (Label)this.FindControl("lblMessage");
            if (lblMessage != null) {
                lblMessage.Text = msg;
                lblMessage.Visible = true;
            }
            else {
                if (this.lblMessage != null) {
                    this.lblMessage.Text = msg;
                    this.lblMessage.Visible = true;
                }
            }
        }

        public void SetPageError(Exception ex) {
            //This error occured when we use the Response.Redirect method
            //The Application doesn't considered that error as a real error 
            if (!(ex is System.Threading.ThreadAbortException)) {

                if (ex is QSPForm.Common.QSPFormValidationException) {
                    PrintErrorMessage((QSPForm.Common.QSPFormException)ex);
                }
                else {
                    if ((!HttpContext.Current.IsDebuggingEnabled) && (Role < QSPForm.Business.AuthSystem.ROLE_ADMINISTRATOR)) {
                        //We replace the exception by a friendly message
                        QSPForm.Common.QSPFormMessage messageManager = new QSPForm.Common.QSPFormMessage();
                        messageManager.SetSystemErrorMessage(QSPForm.Common.QSPFormMessage.ERRMSG_SYSTEM);
                        PrintErrorMessage(new QSPForm.Common.QSPFormException(messageManager, ex));
                        QSPForm.SystemFramework.ApplicationError.ManageError(ex);
                    }
                    else {
                        //We give the real message
                        QSPForm.Common.QSPFormMessage messageManager = new QSPForm.Common.QSPFormMessage();
                        if (ex.InnerException != null)
                            messageManager.SetSystemErrorMessage(ex.InnerException.Message);
                        else
                            messageManager.SetSystemErrorMessage(ex.Message);
                        PrintErrorMessage(new QSPForm.Common.QSPFormException(messageManager, ex));
                        QSPForm.SystemFramework.ApplicationError.ManageError(ex);
                    }
                }
                this.Page.MaintainScrollPositionOnPostBack = false;
                hasError = true;
            }
        }

        protected override void OnError(EventArgs e) {
            Exception ex = System.Web.HttpContext.Current.Server.GetLastError().GetBaseException();
            QSPForm.SystemFramework.ApplicationError.ManageError(ex);

            base.OnError(e);
        }

        #endregion

        private void EnsureSessionIntegrity() {
            //This method is called in the Page PreRender
            try {
                //Check if all information is there to set correctly the security
                if (HttpContext.Current.Session[ROLENAME] == null && !((this is BaseLogin) || (this is BaseSessionOffPage) || (this is BaseLogOut) || (this is BaseErrorsPage)))//||(!Session.IsNewSession && this is BaseDefault)))
				{
                    string sUrl = this.GetPageToGo(QSPForm.Business.AppItem.SessionOff);
                    Response.Redirect(sUrl);
                }
                //Check if the security is enough
                if (!(((this is BaseLogin) || (this is BaseSessionOffPage) || (this is BaseLogOut) || (this is BaseErrorsPage) || (this is BaseDefault) || (this is BaseBusinessCalendar)))) {
                    bool IsValid = false;
                    if (PermissionInfo != null) {
                        IsValid = RightView;
                    }
                    if (!IsValid) {
                        string sUrl = this.GetPageToGo(QSPForm.Business.AppItem.ErrorPage);
                        string sDescription = "The Access to this Page is forbidden";
                        string sMessage = "Please verify with your administrator, that you have the appropriate access.";
                        sUrl = sUrl + "?" + BaseErrorsPage.PARAM_DESCRIPTION + "=" + sDescription;
                        sUrl = sUrl + "&" + BaseErrorsPage.PARAM_MESSAGE + "=" + sMessage;
                        Response.Redirect(sUrl);
                    }
                }
            }
            catch (Exception ex) {
                //Nothing for now - todo F6
            }
        }

        private void hidRefresh_ServerChange(object sender, EventArgs e) {
            OnRefreshPage(e);
        }

        public PageMode Mode {
            get {
                if (this.ViewState["Mode"] != null) {
                    return (PageMode)this.ViewState["Mode"];
                }
                else {
                    this.ViewState["Mode"] = PageMode.ReadOnly;
                    return PageMode.ReadOnly;
                }
            }
            set { this.ViewState["Mode"] = value; }
        }
    }
}