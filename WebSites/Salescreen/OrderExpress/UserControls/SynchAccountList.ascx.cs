using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.OLMCUSPTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for SynchAccountList.
    /// </summary>
    public partial class SynchAccountList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = OLMCUSPTable.FLD_PKID + " DESC";
        protected System.Web.UI.WebControls.Label Label1;
        protected DataView DVAccount;
        private const string FILTER_STATE = "Filter_State";
        protected dataDef dTblAccount = new dataDef();
        protected System.Web.UI.WebControls.DropDownList ddlProgramType;
        protected System.Web.UI.WebControls.Label Label3;
        protected System.Web.UI.WebControls.CheckBox chkReportedTo;
        private CommonUtility clsUtil = new CommonUtility();
        private const string FILTER_STATUS_CATEGORY = "Filter_Status";

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
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

        #endregion

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVAccount = new DataView(dTblAccount);
            this.DataSource = DVAccount;
            this.MainDataGrid = dtgAccount;
            dtgAccount.DataKeyField = dataDef.FLD_PKID;
            base.LabelTotal = lblTotal;
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer			

            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();

            string sCriteria = this.dtgAccount.FilterExpression;

            switch (this.dtgAccount.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "*";
                        break;
                    }
                default: {
                        sCriteria = "*" + sCriteria + "*";
                        break;
                    }
            }

            string SubdivisionCode = ViewState[FILTER_STATE].ToString();
            int StatusCategoryID = Convert.ToInt32(ViewState[FILTER_STATUS_CATEGORY]);
            string sRowFilter = "";
            dTblAccount = accSys.ExchangeTable_SelectAll();
            //Only do a Search by Account Name for the moment // this.dtgAccount.SearchMode, 
            if (sCriteria.Replace("*", "").Length > 0) {
                sRowFilter = dataDef.FLD_BILL_NAME + " LIKE '" + sCriteria + "'";
            }
            if (SubdivisionCode.Length > 0) {
                if (sRowFilter.Length > 0) {
                    sRowFilter = sRowFilter + " AND ";
                }
                sRowFilter = dataDef.FLD_BILL_STATE + " = '" + SubdivisionCode.Replace("US-", "") + "'";
            }

            //sCriteria, ProgramType, SubdivisionCode, FMID, ReportedTo, StatusCategoryID);

            DVAccount = new DataView();
            DVAccount.Table = dTblAccount;
            //Always do a secondary sort on Account_Name			
            DVAccount.Sort = this.dtgAccount.SortExpression + ", " + dataDef.FLD_BILL_NAME;
            DVAccount.RowFilter = sRowFilter;
            lblTotal.Text = "Number of Account(s) : " + DVAccount.Count.ToString();
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();
            //			clsUtil.SetProgramTypeDropDownList(ddlProgramType, true);
            //			//Set To Chocolate as Default
            //			ListItem lstItem =  ddlProgramType.Items.FindByValue("11");
            //			if (lstItem != null)
            //			{
            //				ddlProgramType.ClearSelection();
            //				lstItem.Selected = true;
            //			}
            //			ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProgramType.SelectedItem.Value);

            clsUtil.SetUSStateDropDownList(ddlState, true);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;

            clsUtil.SetStatusCategoryDropDownList(ddlStatusCategory, true);
            ViewState[FILTER_STATUS_CATEGORY] = ddlStatusCategory.SelectedItem.Value;

            base.FillFilter();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            //			ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProgramType.SelectedItem.Value);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;
            ViewState[FILTER_STATUS_CATEGORY] = ddlStatusCategory.SelectedItem.Value;
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_ACCOUNT_ID].ToString();
                    string sIDName = AccountDetailInfo.ACC_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, AppItem.AccountDetailInfo, sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "AccountDetailInfo.aspx?", sIDName, sID, 0, 0);

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
            //string strUrl = this.Page.GetPageToGo(QSPForm.Business.AppItem.AccountForm_Step1);
            string strUrl = "~/AccountStep_Search.aspx";
            Response.Redirect(strUrl);
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}