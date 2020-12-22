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
using dataDef = QSPForm.Common.DataDef.Promo_TextTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>ToDoList</summary>
    public partial class Promo_TextList : BaseWebSubFormControl {
        protected DataView DVPromo_Text;
        private const string FILTER_SUBDIVISION = "Filter_Subdivision";
        private const string FILTER_IS_NATIONAL = "Filter_Is_National";
        private const string FILTER_DISPLAY_STATUS = "Filter_Display_Status";
        private const string PARAM_START = "Param_Start";
        private const string PARAM_END = "Param_End";
        private const string FILTER_RPT_TO = "Filter_Reported_To";
        protected dataDef dTblPromo_Text = new dataDef();
        private const string DEFAULT_SORT = Promo_TextTable.FLD_PKID + " DESC";
        protected QSP.WebControl.DataGridControl.SortedDataGrid dtgPromo_Promo_Text;
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
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
            DVPromo_Text = new DataView(dTblPromo_Text);
            this.DataSource = DVPromo_Text;
            this.MainDataGrid = dtgPromo_Text;
            dtgPromo_Text.DataKeyField = Promo_TextTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
            //clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.Promo_TextDetail, BasePromo_TextDetail.PROMO_TEXT_ID, "0", 0,0);
            clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkAddNew, "Promo_TextDetail.aspx?", BasePromo_TextDetail.PROMO_TEXT_ID, "0", 0, 0);
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();

            //clsUtil.SetRegionDropDownList(ddlRegion, true);
            clsUtil.SetUSSubdivisionDropDownList(ddlSubdivision, true);
            ViewState[FILTER_SUBDIVISION] = ddlSubdivision.SelectedItem.Value;

            ViewState[FILTER_IS_NATIONAL] = ddlNational.SelectedItem.Value;

            ViewState[FILTER_DISPLAY_STATUS] = ddlDisplayStatus.SelectedItem.Value;

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
            QSPForm.Business.Promo_TextSystem Promo_TextSys = new Promo_TextSystem();
            string sCriteria = this.dtgPromo_Text.FilterExpression;
            switch (this.dtgPromo_Text.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }
            string SubdivisionID = ViewState[FILTER_SUBDIVISION].ToString();
            int NationalStatus = Convert.ToInt32(ViewState[FILTER_IS_NATIONAL]);
            int DisplayStatus = Convert.ToInt32(ViewState[FILTER_DISPLAY_STATUS]);
            bool IncludeFMReportedTo = Convert.ToBoolean(ViewState[FILTER_RPT_TO]);

            //FM Hierarchy Filter
            string FM_ID = "";
            if (Page.Role == AuthSystem.ROLE_FM)
                FM_ID = Page.FMID;

            dTblPromo_Text = Promo_TextSys.SelectAll_Search(this.dtgPromo_Text.SearchMode, sCriteria, SubdivisionID, NationalStatus, DisplayStatus, FM_ID, IncludeFMReportedTo);

            DVPromo_Text = new DataView();
            DVPromo_Text.Table = dTblPromo_Text;

            DVPromo_Text.Sort = this.dtgPromo_Text.SortExpression;
            lblTotal.Text = "Number of Logo(s) : " + DVPromo_Text.Count.ToString();
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
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
                    string sIDName = Promo_TextDetailInfo.PROMO_TEXT_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.Promo_TextDetailInfo, sIDName, sID, 0,0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "Promo_TextDetailInfo.aspx?", sIDName, sID, 0, 0);
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