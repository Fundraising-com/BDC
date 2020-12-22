using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using Entity = QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AccountList.
    /// </summary>
    public partial class AccountList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = ""; // = AccountTable.FLD_PKID + " DESC";
        protected IEnumerable<Entity.AccountList> accountList;
        private const string FILTER_STATE = "Filter_State";
        private const string FILTER_TYPE = "Filter_Type";
        private const string FILTER_FSM_DISPLAY_MODE = "Filter_FSM_DisplayMode";
        private CommonUtility clsUtil = new CommonUtility();
        private const string FILTER_STATUS_CATEGORY = "Filter_Status";
        public const string PARAM_PROG = "ProgType";

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
                tblFMFilterOption.Visible = (Page.Role == AuthSystem.ROLE_FM);
                tblFilterStatusCategory.Visible = (Page.Role > AuthSystem.ROLE_FM);
                trFieldSupportFilterOption.Visible = (Page.Role > AuthSystem.ROLE_FM);
                //Manage Insert for Add Security
                this.imgbtnAddAccount.Visible = (this.Page.Role == AuthSystem.ROLE_FM || this.Page.Role >= AuthSystem.ROLE_ADMINISTRATOR); // (this.Page.RightInsert);
                //Don,t display the status column for  FSM
                dtgAccount.Columns[1].Visible = (Page.Role > AuthSystem.ROLE_FM);
                dtgAccount.Columns[2].Visible = (Page.Role > AuthSystem.ROLE_FM);
                dtgAccount.Columns[13].Visible = (Page.Role > AuthSystem.ROLE_FM);
                dtgAccount.Columns[14].Visible = (Page.Role > AuthSystem.ROLE_FM);
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            InitControl();
            this.QSPFormSearchModule.OnSearch += new SearchModuleEventHandler(this.QSPFormSearchModule_OnSearch);
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgbtnAddAccount.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnAddAccount_Click);
        }

        #endregion

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            this.MainDataGrid = dtgAccount;
            dtgAccount.DataKeyField = "AccountId";
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
            }
        }

        protected override void LoadDataSourceGrid() {
            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();

            // Create a Settings class instance containing search options.
            SearchSettings Settings = new SearchSettings();
            Settings.Sort = dtgAccount.SortExpression;
            Settings.PageSize = dtgAccount.PageSize;
            Settings.PageIndex = dtgAccount.CurrentPageIndex;

            // Apply search mode.
            if (dtgAccount.SearchMode == 0 && dtgAccount.FilterExpression != "%") // Search by first char. To display All, FirstChar is "", not "%".
                Settings.FirstChar = dtgAccount.FilterExpression;
            else if (dtgAccount.SearchMode == 1) // Search by account name
                Settings.AccountName = dtgAccount.FilterExpression;
            else if (dtgAccount.SearchMode == 6) // Search by city
                Settings.City = dtgAccount.FilterExpression;
            else if (dtgAccount.SearchMode == 2) // Search by EDS account ID
                Settings.EdsAccountId = dtgAccount.FilterExpression;
            else if (dtgAccount.SearchMode == 7) // Search by QSP account ID
                Settings.AccountId = dtgAccount.FilterExpression;
            else if (dtgAccount.SearchMode == 5) // Search by zip code
                Settings.ZipCode = dtgAccount.FilterExpression;

            // Apply other filters.
            if (int.Parse(ddlProgramType.SelectedValue) > 0)
                Settings.ProgramTypeId = int.Parse(ddlProgramType.SelectedValue);
            if (int.Parse(ddlStatusCategory.SelectedValue) > 0)
                Settings.StatusCategoryId = int.Parse(ddlStatusCategory.SelectedValue);
            Settings.SubdivisionCode = ddlState.SelectedValue;
            Settings.FsmId = txtFSMID.Text;
            Settings.FsmName = txtFSMName.Text;

            // For field sales managers, instead of displaying all accounts, a combobox appears with 3 display options.
            if (Page.Role == AuthSystem.ROLE_FM && int.Parse(ddlFSMDisplayMode.SelectedValue) > 0) {
                Settings.FsmId = Page.FMID;
                Settings.DisplayMode = (DisplayMode)int.Parse(ddlFSMDisplayMode.SelectedValue);
            }

            // Account list count is only calculated if it is 0.
            int ListCount = dtgAccount.VirtualItemCount;
            accountList = accSys.SelectAll_Search(Settings, ref ListCount);
            dtgAccount.DataSource = accountList;
            dtgAccount.VirtualItemCount = ListCount;
            lblTotal.Text = "Number of Account(s) : " + dtgAccount.VirtualItemCount.ToString();
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();
            clsUtil.SetProgramTypeDropDownList(ddlProgramType, true);
            string sProgTypeID = "";  //Set To ALL as Default
            if (Request[PARAM_PROG] != null)
                sProgTypeID = Request[PARAM_PROG].ToString();
            ListItem lstItem = ddlProgramType.Items.FindByValue(sProgTypeID);
            if (lstItem != null) {
                ddlProgramType.ClearSelection();
                lstItem.Selected = true;
            }
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProgramType.SelectedItem.Value);

            clsUtil.SetUSStateDropDownList(ddlState, true);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;

            clsUtil.SetAccountStatusCategoryDropDownList(ddlStatusCategory, true);
            ViewState[FILTER_STATUS_CATEGORY] = ddlStatusCategory.SelectedItem.Value;

            //It's 0 by default
            ViewState[FILTER_FSM_DISPLAY_MODE] = ddlFSMDisplayMode.SelectedItem.Value;

            base.FillFilter();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProgramType.SelectedItem.Value);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;
            ViewState[FILTER_STATUS_CATEGORY] = ddlStatusCategory.SelectedItem.Value;
            ViewState[FILTER_FSM_DISPLAY_MODE] = ddlFSMDisplayMode.SelectedItem.Value;
            // Make sure we get the new items count since it is cached for performance when paging.
            dtgAccount.VirtualItemCount = 0;
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    Entity.AccountList Acc = (Entity.AccountList)e.Item.DataItem;
                    sID = Acc.AccountId.ToString();
                    string sIDName = AccountDetailInfo.ACC_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, AppItem.AccountDetailInfo, sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "AccountDetailInfo.aspx?", sIDName, sID, 0, 0);

                    ((Label)e.Item.FindControl("lblZip")).Text = clsUtil.FormatZipCode(((Label)e.Item.FindControl("lblZip")).Text);

                    //string name = drvRow.Row[QSPForm.Common.DataDef.AccountTable.FLD_CREATE_LAST_NAME].ToString() + " " + drvRow.Row[QSPForm.Common.DataDef.AccountTable.FLD_CREATE_FIRST_NAME].ToString();
                    //if (name.Trim().Length == 0)
                    //    name = "System";
                    //else
                    //    name = name.Replace(" ", "&nbsp;");
                    //((Label)e.Item.FindControl("lblCreatorName")).Text = name;

                    //					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
                    //					if (imgBtnDetail != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, AppItem.AccountDetailInfo, sIDName, sID, 0,0);
                    //					}
                    //					HyperLink hypLnkName = (HyperLink) e.Item.FindControl("hypLnkName");
                    //					if (hypLnkName != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(hypLnkName, AppItem.AccountDetailInfo, sIDName, sID, 0,0);
                    //					}
                }
            }
        }

        private void imgbtnAddAccount_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string strUrl = this.Page.GetPageToGo(Business.AppItem.AccountForm_Step1);
            string strUrl = "AccountStep_Search.aspx";
            Response.Redirect(strUrl);
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }

        public Label SearchName {
            get {
                return QSPFormSearchModule.LabelSearchByAlpha;
            }
            set {
                QSPFormSearchModule.LabelSearchByAlpha = value;
            }
        }
    }
}