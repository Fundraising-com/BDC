using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSP.WebControl;
using dataDef = QSPForm.Common.DataDef.PromoTable;
//using dataDef = QSPForm.Common.DataDef.ToDoTable; <-- DataDef pour PromoLogo
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>ToDoList</summary>
    public partial class PromoLogoList : BaseWebSubFormControl {
        protected DataView DVPromo;
        private const string FILTER_SUBDIVISION = "Filter_Subdivision";
        private const string FILTER_IS_NATIONAL = "Filter_Is_National";
        private const string FILTER_DISPLAY_STATUS = "Filter_Display_Status";
        private const string PARAM_START = "Param_Start";
        private const string PARAM_END = "Param_End";
        private const string FILTER_RPT_TO = "Filter_Reported_To";
        //		private const string FM_ID = "fm_id";
        protected dataDef dTblPromo = new dataDef();
        private const string DEFAULT_SORT = PromoTable.FLD_PKID + " DESC";
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
            DVPromo = new DataView(dTblPromo);
            this.DataSource = DVPromo;
            this.MainDataGrid = dtgPromo;
            dtgPromo.DataKeyField = PromoTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
            //clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.PromoDetail, BasePromoDetail.PROMO_ID, "0", 0,0);
            clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkAddNew, "PromoDetail.aspx?", BasePromoDetail.PROMO_ID, "0", 0, 0);
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();

            clsUtil.SetUSSubdivisionDropDownList(ddlSubdivision, true);
            ViewState[FILTER_SUBDIVISION] = ddlSubdivision.SelectedItem.Value;

            ViewState[FILTER_IS_NATIONAL] = ddlNational.SelectedItem.Value;

            ViewState[FILTER_DISPLAY_STATUS] = ddlDisplayStatus.SelectedItem.Value;

            BusinessCalendarSystem calSys = new BusinessCalendarSystem();
            int CurrentFY = calSys.GetFiscalYear();
            //F6 Modif
            DateTime CurrentFirstDateFY = calSys.GetFirstDateOfFiscalYear(CurrentFY);
            DateTime CurrentLastDateFY = calSys.GetLastDateOfFiscalYear(CurrentFY);

            txtStartDate.Text = CurrentFirstDateFY.ToShortDateString();
            ViewState[PARAM_START] = CurrentFirstDateFY;

            txtEndDate.Text = CurrentLastDateFY.ToShortDateString();
            ViewState[PARAM_END] = CurrentLastDateFY;

            if (tdFilterFMReportedTo.Visible) {
                ViewState[FILTER_RPT_TO] = chkReportedTo.Checked;
            }
            else {
                ViewState[FILTER_RPT_TO] = false;
            }

            base.FillFilter();
        }

        public override void BindGrid() {
            this.Page.Validate();
            if (this.Page.IsValid)
                base.BindGrid();
        }

        protected override void LoadDataSourceGrid() {
            //	Call the appropriate Class from the Biz layer
            QSPForm.Business.PromoSystem promoSys = new PromoSystem();
            string sCriteria = this.dtgPromo.FilterExpression;
            switch (this.dtgPromo.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }
            string Subdivision = ViewState[FILTER_SUBDIVISION].ToString();
            int NationalStatus = Convert.ToInt32(ViewState[FILTER_IS_NATIONAL]);
            int DisplayStatus = Convert.ToInt32(ViewState[FILTER_DISPLAY_STATUS]);
            bool IncludeFMReportedTo = Convert.ToBoolean(ViewState[FILTER_RPT_TO]);
            DateTime StartDate = Convert.ToDateTime(ViewState[PARAM_START]);
            DateTime EndDate = Convert.ToDateTime(ViewState[PARAM_END]);
            //string StartDate = ViewState[PARAM_START].ToString();
            //string EndDate = ViewState[PARAM_END].ToString();

            //FM Hierarchy Filter
            string FM_ID = "";
            if (Page.Role == AuthSystem.ROLE_FM)
                FM_ID = Page.FMID;

            dTblPromo = promoSys.SelectAll_Search(this.dtgPromo.SearchMode, sCriteria, Subdivision, NationalStatus, DisplayStatus, StartDate, EndDate, FM_ID, IncludeFMReportedTo);

            DVPromo = new DataView();
            DVPromo.Table = dTblPromo;

            DVPromo.Sort = this.dtgPromo.SortExpression;
            lblTotal.Text = "Number of Promo(s) : " + DVPromo.Count.ToString();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_SUBDIVISION] = ddlSubdivision.SelectedItem.Value;
            ViewState[FILTER_IS_NATIONAL] = ddlNational.SelectedItem.Value;
            ViewState[FILTER_DISPLAY_STATUS] = ddlDisplayStatus.SelectedItem.Value;
            if (tdFilterFMReportedTo.Visible) {
                ViewState[FILTER_RPT_TO] = chkReportedTo.Checked;
            }
            else {
                ViewState[FILTER_RPT_TO] = false;
            }
            ViewState[PARAM_START] = txtStartDate.Text;
            ViewState[PARAM_END] = txtEndDate.Text;
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    ((ImageButton)e.Item.FindControl("imgBtnDetail")).ImageUrl = QSPForm.Common.QSPFormConfiguration.PromoImagePreviewPath +
                        ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString() + "." +
                            QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;
                    //((DataRowView)e.Item.DataItem).Row[dataDef.FLD_FILE_EXTENSION].ToString();

                    sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
                    string sIDName = PromoDetailInfo.PROMO_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.PromoDetailInfo, sIDName, sID, 0,0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "PromoDetailInfo.aspx?", sIDName, sID, 0, 0);
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