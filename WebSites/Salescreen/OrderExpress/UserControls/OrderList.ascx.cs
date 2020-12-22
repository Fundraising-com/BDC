using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using System.Configuration;
using dataDef = QSPForm.Common.DataDef.OrderHeaderTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderList.
    /// </summary>
    public partial class OrderList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = OrderHeaderTable.FLD_PKID + " DESC";
        protected dataDef dTblOrders = new dataDef();
        protected DataView DVOrders;
        private const string FILTER_STATE = "Filter_State";
        private const string FILTER_TYPE = "Filter_Type";
        private const string FILTER_STATUS_CATEGORY = "Filter_Status";
        private const string PARAM_START = "Param_Start";
        private const string PARAM_END = "Param_End";
        private CommonUtility clsUtil = new CommonUtility();
        public const string PARAM_PROG = "ProgType";
        private static Regex _isNumber = new Regex(@"^\d+$");
        private string c_MinimumStartDate = string.Empty;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            clsUtil.SetJScriptForOpenCalendar(hypLnkStartDate, txtStartDate);
            clsUtil.SetJScriptForOpenCalendar(hypLnkEndDate, txtEndDate);
            if (!IsPostBack) {
                imgbtnAddOrder.Visible = (this.Page.Role == AuthSystem.ROLE_FM || this.Page.Role >= AuthSystem.ROLE_ADMINISTRATOR); // (this.Page.RightInsert);
                dtgOrder.Columns[14].Visible = (Page.Role > AuthSystem.ROLE_FM);
                dtgOrder.Columns[15].Visible = (Page.Role > AuthSystem.ROLE_FM);
            }
            trFieldSupportFilterOption.Visible = (this.Page.Role > AuthSystem.ROLE_FM);
            MinimumStartDateTextBox.Text = MinimumStartDate;
        }

        /// <summary>
        /// WFC Spring shutdown Start Date
        /// </summary>
        public String MinimumStartDate {
            get {
                //return c_ShutDownStartDate;
                try {
                    return Convert.ToString(ConfigurationManager.AppSettings["QSPOrderExpress.OrderList.MinimumStartDate"]);
                }
                catch (Exception ex) {
                    Page.SetPageError(ex); return c_MinimumStartDate;
                }
            }
            set {
                c_MinimumStartDate = value;
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
            this.imgbtnAddOrder.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnAddOrder_Click);

        }

        private void InitControl() {

            this.DefaultSort = DEFAULT_SORT;
            DVOrders = new DataView(dTblOrders);
            this.DataSource = DVOrders;
            this.MainDataGrid = dtgOrder;
            dtgOrder.DataKeyField = OrderHeaderTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;

        }


        #endregion

        protected void Page_PreRender(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
            }
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

            clsUtil.SetOrderStatusCategoryDropDownList(ddlStatusCategory, true);
            ViewState[FILTER_STATUS_CATEGORY] = ddlStatusCategory.SelectedItem.Value;

            BusinessCalendarSystem calSys = new BusinessCalendarSystem();
            int CurrentFY = calSys.GetFiscalYear();
            //F6 Modif
            DateTime CurrentFirstDateFY = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-2); // calSys.GetFirstDateOfFiscalYear(CurrentFY);
            if (this.Page.Role > AuthSystem.ROLE_FIELD_SUPPORT)
                CurrentFirstDateFY = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime CurrentLastDateFY = calSys.GetLastDateOfFiscalYear(CurrentFY);

            txtStartDate.Text = CurrentFirstDateFY.ToShortDateString();
            ViewState[PARAM_START] = CurrentFirstDateFY;

            txtEndDate.Text = CurrentLastDateFY.ToShortDateString();
            ViewState[PARAM_END] = CurrentLastDateFY;

            base.FillFilter();
        }

        public override void BindGrid() {
            this.Page.Validate();
            if (this.Page.IsValid)
                base.BindGrid();
        }

        public static bool IsInteger(string theValue) {
            Match m = _isNumber.Match(theValue);
            return m.Success;
        } //IsInteger

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer
            //			if (!IsPostBack)
            //			{
            //				GetParamQueryStringFilter();
            //			}

            //			if (!IsPostBack)
            //				return;

            QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();

            string sCriteria = dtgOrder.FilterExpression;
            switch (this.dtgOrder.SearchMode) {

                case 8:
                case 7:
                case 5:
                    bool IsInt = IsInteger(sCriteria);

                    if (!IsInt) {
                        if (sCriteria.Contains("-")) {
                            //int index = sCriteria.LastIndexOf("-");
                            //sCriteria = sCriteria.Substring(index+1);
                            sCriteria = sCriteria.Replace("-", "");
                        }
                        else {
                            sCriteria = string.Empty;
                        }
                    }
                    break;

                case 0:
                case 9: {
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
            int StatusCategoryID = Convert.ToInt32(ViewState[FILTER_STATUS_CATEGORY]);
            DateTime StartDate = Convert.ToDateTime(ViewState[PARAM_START]);
            DateTime EndDate = Convert.ToDateTime(ViewState[PARAM_END]);
            //FM Hierarchy Filter
            string sFMID = "";
            string sFMName = "";
            //If the Display mode is not All QSP Accounts
            if ((Page.Role == AuthSystem.ROLE_FM)) {
                sFMID = Page.FMID;
                //blnAllFMReportedTo = chkReportedTo.Checked;
            }
            if (Page.Role > AuthSystem.ROLE_FM) {
                if (txtFSMID.Text.Trim().Length > 0) {
                    if (txtFSMID.Text.Trim().Length > 4) {
                        sFMID = txtFSMID.Text.Trim().Substring(0, 4);
                    }
                    else
                        sFMID = txtFSMID.Text.Trim().Substring(0, txtFSMID.Text.Trim().Length);
                }

                if (txtFSMName.Text.Trim().Length > 0) {
                    sFMName = "%" + txtFSMName.Text.Trim().Replace(" ", "%") + "%";
                }
            }

            dTblOrders = ordSys.SelectAll_Search(this.dtgOrder.SearchMode, sCriteria, sFMID, StatusCategoryID, ProgramType, SubdivisionCode, StartDate, EndDate, sFMName);

            DVOrders = new DataView(dTblOrders);
            //Always do a secondary sort on Account_Name	
            DVOrders.Sort = this.dtgOrder.SortExpression + ", " + CampaignTable.FLD_NAME;
            //Resynchronize the DataSource
            base.DataSource = DVOrders;

            lblTotal.Text = "Number of Order(s) : " + DVOrders.Count.ToString();

            //Set Print Button
            string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.Report_OrderList);

            url = url + "&SearchType=" + this.dtgOrder.SearchMode;
            url = url + "&Criteria=" + sCriteria;
            url = url + "&FMID=" + sFMID;
            if (StatusCategoryID > 0)
                url = url + "&StatusCategoryID=" + StatusCategoryID;
            if (ProgramType > 0)
                url = url + "&ProgramTypeID=" + ProgramType;
            url = url + "&StartDate=" + StartDate.ToShortDateString();
            url = url + "&EndDate=" + EndDate.ToShortDateString();
            imgBtnPrint.Attributes.Add("onclick", "window.open('" + url + "');");
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProgramType.SelectedItem.Value);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;
            ViewState[FILTER_STATUS_CATEGORY] = ddlStatusCategory.SelectedItem.Value;
            ViewState[PARAM_START] = txtStartDate.Text;
            ViewState[PARAM_END] = txtEndDate.Text;
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = dtgOrder.DataKeys[e.Item.ItemIndex].ToString();
                    DataRowView drvRow = (DataRowView)e.Item.DataItem;
                    string sIDName = BaseOrderDetail.ORDER_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.OrderDetailInfo, sIDName, sID, 0,0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "NewOrderDetailDisplay.aspx?", sIDName, sID, 0, 0);


                    ((Label)e.Item.FindControl("lblZip")).Text = clsUtil.FormatZipCode(((Label)e.Item.FindControl("lblZip")).Text);

                    string name = drvRow.Row[dataDef.FLD_CREATE_LAST_NAME].ToString() + " " + drvRow.Row[dataDef.FLD_CREATE_FIRST_NAME].ToString();
                    if (name.Trim().Length == 0)
                        name = "System";
                    else
                        name = name.Replace(" ", "&nbsp;");
                    ((Label)e.Item.FindControl("lblCreatorName")).Text = name;

                    //					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
                    //					if (imgBtnDetail != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.OrderDetailInfo, sIDName, sID, 0,0);
                    //					}
                    //					HyperLink hypLnkName = (HyperLink) e.Item.FindControl("hypLnkName");
                    //					if (hypLnkName != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(hypLnkName, QSPForm.Business.AppItem.OrderDetailInfo, sIDName, sID, 0,0);
                    //					}
                }
            }
        }

        private void GetParamQueryStringFilter() {
            //if (Request[CampaignDetail.CAMP_ID] != null)
            //{
            //    QSPFormSearchModule.FilterExpression = Request[CampaignDetail.CAMP_ID].ToString();
            //    QSPFormSearchModule.SearchMode = 2;
            //}
        }

        private void imgbtnAddOrder_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string strUrl = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step1);
            string strUrl = "~/OrderStep_Search.aspx?";
            string strProgType = ddlProgramType.SelectedValue;
            if (strProgType == "0")
                strProgType = "11";

            Response.Redirect(strUrl + "&ProgType=" + strProgType);
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