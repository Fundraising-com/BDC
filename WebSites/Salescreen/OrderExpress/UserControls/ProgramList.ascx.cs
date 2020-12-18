using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSP.WebControl;
using dataDef = QSPForm.Common.DataDef.ProgramTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>ProgramList</summary>
    public partial class ProgramList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = ProgramTable.FLD_PROGRAM_NAME + " ASC";
        protected ProgramTable dTblProgram = new ProgramTable();
        protected DataView DVProgram;
        protected System.Web.UI.WebControls.ImageButton imgbtnAddProgram;
        private CommonUtility clsUtil = new CommonUtility();
        private const string FILTER_TYPE = "Filter_Type";

        #region auto-generated, Initialization code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            InitControl();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            this.DVProgram = new DataView(dTblProgram);
            this.DataSource = this.DVProgram;
            this.MainDataGrid = this.dtgProgramItems;
            this.dtgProgramItems.DataKeyField = dataDef.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }
        #endregion auto-generated, Initialization code

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!Page.IsPostBack) {
                if (this.Page.RightInsert) {
                    hypLnkAddNew.Visible = true;
                    //clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.ProgramDetail, BaseProgramDetail.PROGRAM_ID, "0", 690, 600);
                    clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkAddNew, "ProgramDetail.aspx?", BaseProgramDetail.PROGRAM_ID, "0", 690, 600);
                }
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        protected override void FillFilter() {
            clsUtil.SetProgramTypeDropDownList(ddlProgramType, true);
            ViewState[FILTER_TYPE] = ddlProgramType.SelectedItem.Value;

            base.FillFilter();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_TYPE] = ddlProgramType.SelectedItem.Value;
        }

        protected override void LoadDataSourceGrid() {
            QSPForm.Business.ProgramSystem prgSys = new QSPForm.Business.ProgramSystem();
            string sCriteria = this.dtgProgramItems.FilterExpression;
            switch (this.dtgProgramItems.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }

            int ProgramTypeID = Convert.ToInt32(ViewState[FILTER_TYPE]);
            dTblProgram = prgSys.SelectAll_Search(dtgProgramItems.SearchMode, sCriteria, ProgramTypeID);

            this.DVProgram = new DataView(dTblProgram);
            this.DVProgram.Sort = this.dtgProgramItems.SortExpression;
            this.DataSource = this.DVProgram;
            lblTotal.Text = "Number of Program(s) : " + this.DVProgram.Count.ToString();
        }

        protected override void OnItemCreated(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = dtgProgramItems.DataKeys[e.Item.ItemIndex].ToString();
                    string sIDName = ProgramDetailInfo.PROGRAM_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.ProgramDetailInfo, sIDName, sID, 690, 600);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "ProgramDetailInfo.aspx?", sIDName, sID, 690, 600);
                }
            }
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