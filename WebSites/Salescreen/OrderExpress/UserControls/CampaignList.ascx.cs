    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using QSPForm.Common.DataDef;
    using QSPForm.Business;
    using dataDef = QSPForm.Common.DataDef.CampaignTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CampaignList.
    /// </summary>
    public partial class CampaignList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = CampaignTable.FLD_NAME + " ASC";
        protected dataDef dTblCampaigns = new dataDef();
        protected DataView DVCampaigns;
        private const string FILTER_FY = "Filter_FY";
        private const string FILTER_STATE = "Filter_State";
        private const string FILTER_TYPE = "Filter_Type";
        private const string PARAM_START = "Param_Start";
        private const string PARAM_END = "Param_End";
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
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
            DVCampaigns = new DataView(dTblCampaigns);
            this.DataSource = DVCampaigns;
            this.MainDataGrid = dtgCamp;
            dtgCamp.DataKeyField = CampaignTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }

        #endregion

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();

            clsUtil.SetProgramTypeDropDownList(ddlProgramType, true);
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProgramType.SelectedItem.Value);

            clsUtil.SetUSStateDropDownList(ddlState, true);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;

            clsUtil.SetFiscalYearDropDownList(ddlFiscalYear, false);
            int CurrentFY = Convert.ToInt32(ddlFiscalYear.SelectedItem.Value);
            ViewState[FILTER_FY] = CurrentFY;

            DateTime CurrentFirstDateFY = new DateTime((CurrentFY - 1), 7, 1);
            DateTime CurrentLastDateFY = CurrentFirstDateFY.AddYears(1).AddDays(-1);
            txtStartDate.Text = CurrentFirstDateFY.ToShortDateString();
            ViewState[PARAM_START] = CurrentFirstDateFY;

            txtEndDate.Text = CurrentLastDateFY.ToShortDateString();
            ViewState[PARAM_END] = CurrentLastDateFY;

            base.FillFilter();
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer
            QSPForm.Business.CampaignSystem campSys = new QSPForm.Business.CampaignSystem();

            string sCriteria = dtgCamp.FilterExpression;
            if (!IsPostBack) {
                if (Page.Role > 1)
                    sCriteria = "A";
            }
            switch (this.dtgCamp.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }
            string SubdivisionCode = ViewState[FILTER_STATE].ToString();
            int ProgramType = Convert.ToInt32(ViewState[FILTER_TYPE]);
            int FY = Convert.ToInt32(ViewState[FILTER_FY]);
            DateTime StartDate = Convert.ToDateTime(ViewState[PARAM_START]);
            DateTime EndDate = Convert.ToDateTime(ViewState[PARAM_END]);
            //FM Hierarchy Filter
            string FMID = "";
            if (Page.Role == AuthSystem.ROLE_FM)
                FMID = Page.FMID;

            dTblCampaigns = campSys.SelectAll_Search(this.dtgCamp.SearchMode, sCriteria, FMID, FY, ProgramType, SubdivisionCode, StartDate, EndDate, true);

            DVCampaigns = new DataView(dTblCampaigns);
            DVCampaigns.Sort = this.dtgCamp.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVCampaigns;

            lblTotal.Text = "Number of Campaign(s) : " + DVCampaigns.Count.ToString();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProgramType.SelectedItem.Value);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;
            ViewState[FILTER_FY] = Convert.ToInt32(ddlFiscalYear.SelectedItem.Value);
            ViewState[PARAM_START] = txtStartDate.Text;
            ViewState[PARAM_END] = txtEndDate.Text;
        }

        protected override void OnItemCreated(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    //sID = dtgCamp.DataKeys[e.Item.ItemIndex].ToString();
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.CampaignDetail, CampaignDetail.CAMP_ID, sID, 0,0, "OnDblClick");

                    //ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
                    //if (imgBtnDetail != null)
                    //{
                    //    clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.CampaignDetail, CampaignDetail.CAMP_ID, sID, 0,0);
                    //}
                    //LinkButton lnkBtnCampaign = (LinkButton) e.Item.FindControl("lnkBtnCampaign");
                    //if (lnkBtnCampaign != null)
                    //{
                    //    clsUtil.SetJScriptForOpenDetail(lnkBtnCampaign, QSPForm.Business.AppItem.CampaignDetail, CampaignDetail.CAMP_ID, sID, 0,0);
                    //}
                }

            }
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}