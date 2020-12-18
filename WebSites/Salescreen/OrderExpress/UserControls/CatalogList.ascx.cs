using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.CatalogTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CatalogList.
    /// </summary>
    public partial class CatalogList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_PKID + " DESC";
        protected DataView DVCatalog;
        private const string FILTER_CULTURE = "Filter_Culture";
        private const string PARAM_START = "Param_Start";
        private const string PARAM_END = "Param_End";

        protected dataDef dTblCatalog = new dataDef();

        protected System.Web.UI.WebControls.Label Label10;

        private CommonUtility clsUtil = new CommonUtility();
        private const string FILTER_STATUS_CATEGORY = "Filter_Status";

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            clsUtil.SetJScriptForOpenCalendar(hypLnkStartDate, txtStartDate);
            clsUtil.SetJScriptForOpenCalendar(hypLnkEndDate, txtEndDate);
            if (!IsPostBack) {
                //Manage Insert for Add Security
                this.imgbtnAddCatalog.Visible = (this.Page.Role >= AuthSystem.ROLE_ADMINISTRATOR); // (this.Page.RightInsert);
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
            DVCatalog = new DataView(dTblCatalog);
            this.DataSource = DVCatalog;
            this.MainDataGrid = dtgCatalog;
            dtgCatalog.DataKeyField = CatalogTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }

        protected override void FillFilter() {

            CommonUtility clsUtil = new CommonUtility();
            clsUtil.SetCultureDropDownList(ddlCulture, true);
            ViewState[FILTER_CULTURE] = ddlCulture.SelectedItem.Value;

            BusinessCalendarSystem calSys = new BusinessCalendarSystem();
            int CurrentFY = calSys.GetFiscalYear();
            //F6 Modif
            DateTime CurrentFirstDateFY = calSys.GetFirstDateOfFiscalYear(CurrentFY);
            DateTime CurrentLastDateFY = calSys.GetLastDateOfFiscalYear(CurrentFY);

            txtStartDate.Text = CurrentFirstDateFY.ToShortDateString();
            ViewState[PARAM_START] = CurrentFirstDateFY;

            txtEndDate.Text = CurrentLastDateFY.ToShortDateString();
            ViewState[PARAM_END] = CurrentLastDateFY;

            base.FillFilter();
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            QSPForm.Business.CatalogSystem objSys = new QSPForm.Business.CatalogSystem();

            string sCriteria = this.dtgCatalog.FilterExpression;

            switch (this.dtgCatalog.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }

            DateTime StartDate = Convert.ToDateTime(ViewState[PARAM_START]);
            DateTime EndDate = Convert.ToDateTime(ViewState[PARAM_END]);
            string Culture = ViewState[FILTER_CULTURE].ToString();

            dTblCatalog = objSys.SelectAll_Search(this.dtgCatalog.SearchMode, sCriteria, Culture, StartDate, EndDate);

            DVCatalog = new DataView(dTblCatalog);

            DVCatalog.Sort = dtgCatalog.SortExpression;

            lblTotal.Text = "Number of Catalog(s) : " + DVCatalog.Count.ToString();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_CULTURE] = ddlCulture.SelectedItem.Value;
            ViewState[PARAM_START] = txtStartDate.Text;
            ViewState[PARAM_END] = txtEndDate.Text;
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = ((DataRowView)e.Item.DataItem).Row[CatalogTable.FLD_PKID].ToString();
                    string sIDName = CatalogDetail.CATALOG_ID;
                    //AppItem appItem = AppItem.CatalogDetail;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, appItem, sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "CatalogDetail.aspx?", sIDName, sID, 0, 0);
                    //					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
                    //					if (imgBtnDetail != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, appItem, sIDName, sID, 0,0);
                    //					}
                    //
                    //					HyperLink hypLnkName = (HyperLink) e.Item.FindControl("hypLnkName");
                    //					if (hypLnkName != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(hypLnkName, appItem, sIDName, sID, 0,0);
                    //					}
                }
            }
        }

        private void imgbtnAddCatalog_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //			string strUrl = this.Page.GetPageToGo(QSPForm.Business.AppItem.CatalogForm_Step1);
            //			Response.Redirect(strUrl);
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