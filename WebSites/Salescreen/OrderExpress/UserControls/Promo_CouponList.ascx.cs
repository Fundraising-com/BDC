using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Common;
using QSPForm.Business;
using QSP.WebControl;
using dataDef = QSPForm.Common.DataDef.PromoCouponTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>ToDoList</summary>
    public partial class Promo_CouponList : BaseWebSubFormControl {
        //private const string FILTER_EXPIRATION_MIN = "Filter_Expiration_min";
        //private const string FILTER_EXPIRATION_MAX = "Filter_Expiration_max";
        private const string PARAM_START = "Param_Start";
        private const string PARAM_END = "Param_End";
        private const string FILTER_RPT_TO = "Filter_Reported_To";
        private const string FILTER_SUBDIVISION = "Filter_Subdivision";
        private const string FILTER_IS_NATIONAL = "Filter_Is_National";
        //private const string FILTER_DISPLAY_STATUS = "Filter_Display_Status";
        private const string FILTER_RECEIVED = "Filter_Received";

        protected DataView DVPromo_Coupon;
        protected dataDef dTblPromo_Coupon = new dataDef();
        private const string DEFAULT_SORT = PromoCouponTable.FLD_PKID + " DESC";
        protected QSP.WebControl.DataGridControl.SortedDataGrid dtgPromo_Promo_Coupon;
        protected System.Web.UI.WebControls.Label Label1;
        //protected QSP.WebControl.DataGridControl.SortedDataGrid dtgPromo_Coupon;
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            clsUtil.SetJScriptForOpenCalendar(hypLnkStartDate, txtStartDate);
            clsUtil.SetJScriptForOpenCalendar(hypLnkEndDate, txtEndDate);
            if (!IsPostBack) {
                if (Page.Role == AuthSystem.ROLE_FM) {
                    tdFilterFMReportedTo.Visible = true;
                }
                else {
                    tdFilterFMReportedTo.Visible = false;
                }
                //imgbtnAddOrder.Visible = (this.Page.RightInsert);

                //clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.CouponStep_1, "", "0", 0, 0);
                //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.CouponStep_1);

                string url = "~/CouponStep_Selection.aspx";
                hypLnkAddNew.NavigateUrl = url;
            }
        }

        #region auto-generated, Initialization code
        ///<summary>Required method for Designer support</summary>

        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            InitControl();
            this.QSPFormSearchModule.OnSearch += new SearchModuleEventHandler(this.QSPFormSearchModule_OnSearch);
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
        }
        #endregion auto-generated, Initialization code

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVPromo_Coupon = new DataView(dTblPromo_Coupon);
            this.DataSource = DVPromo_Coupon;
            this.MainDataGrid = dtgPromo_Coupon;
            dtgPromo_Coupon.DataKeyField = PromoCouponTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
            //	clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.Promo_CouponDetail, Promo_CouponDetail.Promo_Coupon_ID, "0", 0,0);
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();

            clsUtil.SetRegionDropDownList(ddlSubdivision, true);
            clsUtil.SetUSSubdivisionDropDownList(ddlSubdivision, true);
            ViewState[FILTER_SUBDIVISION] = ddlSubdivision.SelectedItem.Value;

            ViewState[FILTER_IS_NATIONAL] = ddlNational.SelectedItem.Value;

            ViewState[FILTER_RECEIVED] = ddlReceivedStatus.SelectedItem.Value;

            if (tdFilterFMReportedTo.Visible) {
                ViewState[FILTER_RPT_TO] = chkReportedTo.Checked;
            }
            else {
                ViewState[FILTER_RPT_TO] = false;
            }

            BusinessCalendarSystem calSys = new BusinessCalendarSystem();
            int CurrentFY = calSys.GetFiscalYear();
            //F6 Modif
            DateTime CurrentFirstDateFY = calSys.GetFirstDateOfFiscalYear(CurrentFY);
            DateTime CurrentLastDateFY = calSys.GetLastDateOfFiscalYear(CurrentFY);

            txtStartDate.Text = CurrentFirstDateFY.ToShortDateString();
            ViewState[PARAM_START] = CurrentFirstDateFY;

            txtEndDate.Text = CurrentLastDateFY.ToShortDateString();
            ViewState[PARAM_END] = CurrentLastDateFY;

            ViewState[FILTER_RECEIVED] = ddlReceivedStatus.SelectedItem.Value;

            base.FillFilter();
        }

        public override void BindGrid() {
            this.Page.Validate();
            if (this.Page.IsValid)
                base.BindGrid();
        }

        protected override void LoadDataSourceGrid() {
            //	Call the appropriate Class from the Biz layer
            QSPForm.Business.PromoCouponSystem Promo_CouponSys = new PromoCouponSystem();
            string sCriteria = this.dtgPromo_Coupon.FilterExpression;
            switch (this.dtgPromo_Coupon.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }

            DateTime ExpirationDateMin = Convert.ToDateTime(ViewState[PARAM_START]);
            DateTime ExpirationDateMax = Convert.ToDateTime(ViewState[PARAM_END]);

            bool IncludeFMReportedTo = Convert.ToBoolean(ViewState[FILTER_RPT_TO]);
            int IsReceived = Convert.ToInt32(ViewState[FILTER_RECEIVED]);
            string Subdivision = ViewState[FILTER_SUBDIVISION].ToString();
            int NationalStatus = Convert.ToInt32(ViewState[FILTER_IS_NATIONAL]);
            //int DisplayStatus = Convert.ToInt32(ViewState[FILTER_DISPLAY_STATUS]);

            //FM Hierarchy Filter
            string FM_ID = "";
            if (Page.Role == AuthSystem.ROLE_FM)
                FM_ID = Page.FMID;

            dTblPromo_Coupon = Promo_CouponSys.SelectAll_Search(this.dtgPromo_Coupon.SearchMode, sCriteria, ExpirationDateMin, ExpirationDateMax, IsReceived, Subdivision, IncludeFMReportedTo, NationalStatus);

            DVPromo_Coupon = new DataView();
            DVPromo_Coupon.Table = dTblPromo_Coupon;

            DVPromo_Coupon.Sort = this.dtgPromo_Coupon.SortExpression;
            lblTotal.Text = "Number of Coupon(s) Agreement : " + DVPromo_Coupon.Count.ToString();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_SUBDIVISION] = ddlSubdivision.SelectedItem.Value;
            ViewState[FILTER_IS_NATIONAL] = ddlNational.SelectedItem.Value;
            ViewState[FILTER_RECEIVED] = ddlReceivedStatus.SelectedItem.Value;
            if (tdFilterFMReportedTo.Visible) {
                ViewState[FILTER_RPT_TO] = chkReportedTo.Checked;
            }
            else {
                ViewState[FILTER_RPT_TO] = false;
            }
            ViewState[PARAM_START] = Convert.ToDateTime(txtStartDate.Text);
            ViewState[PARAM_END] = Convert.ToDateTime(txtEndDate.Text);
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    ((ImageButton)e.Item.FindControl("imgBtnLogo")).ImageUrl = QSPForm.Common.QSPFormConfiguration.Promo_LogoImagePreviewPath +
                        ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PROMO_LOGO_ID].ToString() + "." +
                       QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;

                    sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
                    string sIDName = Promo_CouponDetailInfo.Promo_Coupon_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.Promo_CouponDetailInfo, sIDName, sID, 0,0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "Promo_CouponDetailInfo.aspx?", sIDName, sID, 0, 0);
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