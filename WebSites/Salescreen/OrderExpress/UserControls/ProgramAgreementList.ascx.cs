using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.ProgramAgreementTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for ProgramAgreementList.
    /// </summary>
    public partial class ProgramAgreementList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = ProgramAgreementTable.FLD_PKID + " DESC";
        protected DataView DVProgramAgreement;
        private const string FILTER_STATE = "Filter_State";
        private const string FILTER_TYPE = "Filter_Type";
        private const string FILTER_FSM_DISPLAY_MODE = "Filter_FSM_DisplayMode";
        protected dataDef dTblProgramAgreement = new dataDef();
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
                this.imgbtnAddProgramAgreement.Visible = (this.Page.Role == AuthSystem.ROLE_FM || this.Page.Role >= AuthSystem.ROLE_ADMINISTRATOR); // (this.Page.RightInsert);
                //Don,t display the status column for  FSM
                dtgProgramAgreement.Columns[1].Visible = (Page.Role > AuthSystem.ROLE_FM);
                dtgProgramAgreement.Columns[2].Visible = (Page.Role > AuthSystem.ROLE_FM);
                dtgProgramAgreement.Columns[13].Visible = (Page.Role > AuthSystem.ROLE_FM);
                dtgProgramAgreement.Columns[14].Visible = (Page.Role > AuthSystem.ROLE_FM);
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
            this.Load += new EventHandler(Page_Load);
            this.PreRender += new EventHandler(Page_PreRender);
            this.imgbtnAddProgramAgreement.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnAddProgramAgreement_Click);
        }

        #endregion

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVProgramAgreement = new DataView(dTblProgramAgreement);
            this.DataSource = DVProgramAgreement;
            this.MainDataGrid = dtgProgramAgreement;
            dtgProgramAgreement.DataKeyField = ProgramAgreementTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
            }
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer
            //			if (!IsPostBack)
            //				return;

            QSPForm.Business.ProgramAgreementSystem accSys = new QSPForm.Business.ProgramAgreementSystem();

            string sCriteria = this.dtgProgramAgreement.FilterExpression;
            //			if (!IsPostBack)
            //			{
            //				if (Page.Role > 1)
            //					sCriteria = "A";
            //			}

            // Integer values
            if (dtgProgramAgreement.SearchMode == 2 ||
                dtgProgramAgreement.SearchMode == 3 ||
                dtgProgramAgreement.SearchMode == 7) {
                int value = 0;

                Int32.TryParse(sCriteria, out value);

                sCriteria = value.ToString();
            }

            string SubdivisionCode = ViewState[FILTER_STATE].ToString();
            int ProgramType = Convert.ToInt32(ViewState[FILTER_TYPE]);
            int StatusCategoryID = Convert.ToInt32(ViewState[FILTER_STATUS_CATEGORY]);
            //FM Hierarchy Filter
            //FM Hierarchy Filter
            string sFMID = "";
            string sFMName = "";
            int FSM_DisplayMode = Convert.ToInt32(ViewState[FILTER_FSM_DISPLAY_MODE]);
            //If the Display mode is not All QSP ProgramAgreements
            if ((Page.Role == AuthSystem.ROLE_FM) && (FSM_DisplayMode < 3)) {
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

            dTblProgramAgreement = accSys.SelectAll_Search(this.dtgProgramAgreement.SearchMode, sCriteria, ProgramType, SubdivisionCode, sFMID, FSM_DisplayMode, StatusCategoryID, sFMName);

            DVProgramAgreement = new DataView();
            DVProgramAgreement.Table = dTblProgramAgreement;
            //Always do a secondary sort on ProgramAgreement_Name			
            DVProgramAgreement.Sort = this.dtgProgramAgreement.SortExpression + ", " + ProgramAgreementTable.FLD_NAME;
            lblTotal.Text = "Number of Program Agreement(s) : " + DVProgramAgreement.Count.ToString();
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();
            clsUtil.SetProgramTypeDropDownList(ddlProgramType, true);
            string sProgTypeID = "7";  //Set To Other Food as Default
            if (Request[PARAM_PROG] != null)
                sProgTypeID = Request[PARAM_PROG].ToString();
            ListItem lstItem = ddlProgramType.Items.FindByValue(sProgTypeID);
            if (lstItem != null) {
                ddlProgramType.SelectedIndex = ddlProgramType.Items.IndexOf(lstItem);
            }
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProgramType.SelectedItem.Value);

            clsUtil.SetUSStateDropDownList(ddlState, true);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;

            clsUtil.SetProgramAgreementStatusCategoryDropDownList(ddlStatusCategory, true);
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
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    DataRowView drvRow = (DataRowView)e.Item.DataItem;
                    sID = drvRow.Row[ProgramAgreementTable.FLD_PKID].ToString();
                    string sIDName = ProgramAgreementDetailInfo.PROGRAM_AGREEMENT_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, AppItem.ProgramAgreementDetailInfo, sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "ProgramAgreementDetailInfo.aspx?", sIDName, sID, 0, 0);

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
                    //						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, AppItem.ProgramAgreementDetailInfo, sIDName, sID, 0,0);
                    //					}
                    //					HyperLink hypLnkName = (HyperLink) e.Item.FindControl("hypLnkName");
                    //					if (hypLnkName != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(hypLnkName, AppItem.ProgramAgreementDetailInfo, sIDName, sID, 0,0);
                    //					}
                }
            }
        }

        private void imgbtnAddProgramAgreement_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string strUrl = this.Page.GetPageToGo(QSPForm.Business.AppItem.ProgramAgreementForm_Step1);
            string url = "ProgramAgreementStep_Search.aspx";
            Response.Redirect(url);
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