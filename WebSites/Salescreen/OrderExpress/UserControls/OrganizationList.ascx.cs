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
    ///		Summary description for OrganizationList.
    /// </summary>
    public partial class OrganizationList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = "OrganizationName"; //OrganizationTable.FLD_NAME + " ASC";
        protected IEnumerable<Entity.OrganizationList> organizationList;
        private const string FILTER_TYPE = "Filter_Type";
        private const string FILTER_STATE = "Filter_State";
        private const string FILTER_FSM_DISPLAY_MODE = "Filter_FSM_DisplayMode";
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
                tblFieldSupportFilterOption.Visible = (this.Page.Role > AuthSystem.ROLE_FM);
                tblFMFilterOption.Visible = (this.Page.Role == AuthSystem.ROLE_FM);
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
        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            this.MainDataGrid = dtgOrganization;
            dtgOrganization.DataKeyField = "OrganizationId";
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;

        }
        #endregion

        protected override void LoadDataSourceGrid() {
            QSPForm.Business.OrganizationSystem orgSys = new QSPForm.Business.OrganizationSystem();

            // Create a Settings class instance containing search options.
            SearchSettings Settings = new SearchSettings();
            Settings.Sort = dtgOrganization.SortExpression;
            Settings.PageSize = dtgOrganization.PageSize;
            Settings.PageIndex = dtgOrganization.CurrentPageIndex;

            // Apply search mode.
            if (dtgOrganization.SearchMode == 0 && dtgOrganization.FilterExpression != "%") // Search by first char. To display All, FirstChar is "", not "%".
                Settings.FirstChar = dtgOrganization.FilterExpression;
            else if (dtgOrganization.SearchMode == 1) // Search by organization name
                Settings.OrganizationName = dtgOrganization.FilterExpression;
            else if (dtgOrganization.SearchMode == 5) // Search by city
                Settings.City = dtgOrganization.FilterExpression;
            else if (dtgOrganization.SearchMode == 6) // Search by organization ID
                Settings.OrganizationId = dtgOrganization.FilterExpression;
            else if (dtgOrganization.SearchMode == 4) // Search by zip code
                Settings.ZipCode = dtgOrganization.FilterExpression;

            // Apply other filters.
            if (int.Parse(ddlOrganizationType.SelectedValue) > 0)
                Settings.OrganizationTypeId = int.Parse(ddlOrganizationType.SelectedValue);
            Settings.SubdivisionCode = ddlState.SelectedValue;
            Settings.FsmId = txtFSMID.Text;
            Settings.FsmName = txtFSMName.Text;

            // For field sales managers, instead of displaying all accounts, a combobox appears with 3 display options.
            if (Page.Role == AuthSystem.ROLE_FM && int.Parse(ddlFSMDisplayMode.SelectedValue) > 0) {
                Settings.FsmId = Page.FMID;
                Settings.DisplayMode = (DisplayMode)int.Parse(ddlFSMDisplayMode.SelectedValue);
            }

            // Account list count is only calculated if it is 0.
            int ListCount = dtgOrganization.VirtualItemCount;
            organizationList = orgSys.SelectAll_Search(Settings, ref ListCount);
            dtgOrganization.DataSource = organizationList;
            dtgOrganization.VirtualItemCount = ListCount;
            lblTotal.Text = "Number of Organization(s) : " + dtgOrganization.VirtualItemCount.ToString();
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();

            clsUtil.SetOrganizationTypeDropDownList(ddlOrganizationType, true);
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlOrganizationType.SelectedItem.Value);

            clsUtil.SetUSStateDropDownList(ddlState, true);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;

            //It's 0 by default
            ViewState[FILTER_FSM_DISPLAY_MODE] = ddlFSMDisplayMode.SelectedItem.Value;

            base.FillFilter();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlOrganizationType.SelectedItem.Value);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;
            ViewState[FILTER_FSM_DISPLAY_MODE] = ddlFSMDisplayMode.SelectedItem.Value;
            // Make sure we get the new items count since it is cached for performance when paging.
            dtgOrganization.VirtualItemCount = 0;
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                if (e.Item.DataItem != null) {
                    Entity.OrganizationList Org = (Entity.OrganizationList)e.Item.DataItem;
                    string sID = Org.OrganizationId.ToString();
                    string sIDName = OrganizationDetailInfo.ORG_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, AppItem.OrganizationDetailInfo, sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "OrganizationDetailInfo.aspx?", sIDName, sID, 0, 0);
                    //((Label)e.Item.FindControl("lblZip")).Text = clsUtil.FormatZipCode(((Label)e.Item.FindControl("lblZip")).Text);
                }
            }
        }

        private void imgBtnRefresh_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            QSPFormSearchModule.ApplySearch();
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
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