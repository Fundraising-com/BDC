using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.OrganizationTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrganizationSelector.
    /// </summary>
    public partial class OrganizationSelector : BaseWebSubFormControl {
        private const string DEFAULT_SORT = OrganizationTable.FLD_NAME + " ASC";
        protected dataDef dTblList = new dataDef();
        protected DataView DVList;
        private String IDRefCtrl = "";
        private String NameRefCtrl = "";
        private CommonUtility clsUtil = new CommonUtility();
        public event System.EventHandler SelectedIndexChanged;
        private bool buttonVisible = true;
        private string buttonSelectURL = "";
        private const string FILTER_TYPE = "Filter_Type";
        private const string FILTER_STATE = "Filter_State";
        private const string FILTER_FSM_DISPLAY_MODE = "Filter_FSM_DisplayMode";

        override protected void OnLoad(System.EventArgs e) {
            // Put user code to initialize the page here
            if (Request["IDRefCtrl"] != null) {
                IDRefCtrl = Request["IDRefCtrl"].ToString();
            }

            if (Request["NameRefCtrl"] != null) {
                NameRefCtrl = Request["NameRefCtrl"].ToString();
            }

            if (!IsPostBack) {
                tblFMFilterOption.Visible = (this.Page.Role == AuthSystem.ROLE_FM);
                tblFieldSupportFilterOption.Visible = (this.Page.Role > AuthSystem.ROLE_FM);
            }

            base.OnLoad(e);
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            InitControl();
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
            DVList = new DataView(dTblList);
            this.DataSource = DVList;
            this.MainDataGrid = dtgList;
            dtgList.DataKeyField = dataDef.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;

        }

        #endregion

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer
            if (!IsPostBack)
                return;
            QSPForm.Business.OrganizationSystem objSys = new QSPForm.Business.OrganizationSystem();

            string sCriteria = dtgList.FilterExpression;
            switch (this.dtgList.SearchMode) {
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
            int OrgType = Convert.ToInt32(ViewState[FILTER_TYPE]);
            //FM Hierarchy Filter
            string sFMID = "";
            string sFMName = "";
            int FSM_DisplayMode = Convert.ToInt32(ViewState[FILTER_FSM_DISPLAY_MODE]);
            //If the Display mode is not All QSP Accounts
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

            dTblList = objSys.SelectAll_Search(dtgList.SearchMode, sCriteria, OrgType, SubdivisionCode, sFMID, FSM_DisplayMode, sFMName);

            DVList = new DataView(dTblList);
            DVList.Sort = this.dtgList.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVList;

            lblTotal.Text = "Number of Organization(s) : " + DVList.Count.ToString();
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
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                string sValueField = dataDef.FLD_PKID;
                //string sTextField = dataDef.FLD_NAME;
                String sID = "0";
                sID = ((DataRowView)e.Item.DataItem).Row[sValueField].ToString();
                //clsUtil.SetJScriptForOpenDetail(e.Item,QSPForm.Business.AppItem.OrganizationDetail,OrganizationDetailInfo.ORG_ID,sID,0,0,"OnDblClick");
                clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "OrganizationDetailInfo.aspx?", OrganizationDetailInfo.ORG_ID, sID, 0, 0, "OnDblClick");

                ((Label)e.Item.FindControl("lblZip")).Text = clsUtil.FormatZipCode(((Label)e.Item.FindControl("lblZip")).Text);
                ImageButton imgBtnDetail = (ImageButton)e.Item.FindControl("imgBtnDetail");
                if (imgBtnDetail != null) {
                    //clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.OrganizationDetailInfo,OrganizationDetailInfo.ORG_ID, sID, 0,0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetail, "OrganizationDetailInfo.aspx?", OrganizationDetailInfo.ORG_ID, sID, 0, 0);
                }

                HyperLink hyplink = (HyperLink)e.Item.FindControl("hypLnkName");
                if (hyplink != null) {
                    clsUtil.SetJScriptForOpenDetailNoCMS(hyplink, "OrganizationDetailInfo.aspx?", OrganizationDetailInfo.ORG_ID, sID, 0, 0);
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e) {
            trButton.Visible = buttonVisible;

            if (buttonVisible) {
                if (dtgList.SelectedIndex > -1) {
                    DataGridItem dgItem = dtgList.Items[dtgList.SelectedIndex];
                    Label lblID = (Label)dgItem.FindControl("lblID");
                    if (lblID != null) {
                        if (lblID.Text.Length > 0) {
                            String sID = lblID.Text;
                            HyperLink hypLnkName = (HyperLink)dgItem.FindControl("hypLnkName");
                            String sName = hypLnkName.Text;
                            clsUtil.SetJScriptForCloseSelector(imgBtnOK, sID, sName, IDRefCtrl, NameRefCtrl);
                        }
                    }
                }
            }
        }

        public int SelectedValue {
            get {
                int iValue = -1;
                try {
                    if (dtgList.SelectedIndex > -1) {
                        iValue = Convert.ToInt32(dtgList.DataKeys[dtgList.SelectedIndex]);
                    }
                }
                catch (Exception ex) { }

                return iValue;
            }
        }

        public string SelectedText {
            get {
                string sValue = "";
                try {
                    if (dtgList.SelectedIndex > -1) {
                        sValue = ((HyperLink)dtgList.Items[dtgList.SelectedIndex].FindControl("hypLnkName")).Text;
                    }
                }
                catch (Exception ex) { }
                return sValue;
            }
        }

        public QSPForm.Business.AppItem SearchAppItem {
            get {
                return QSPFormSearchModule.SearchAppItem;
            }
            set {
                QSPFormSearchModule.SearchAppItem = value;
            }
        }

        public bool ButtonVisible {
            get {
                return buttonVisible;
            }
            set {
                buttonVisible = value;
            }
        }

        protected void dtgList_SelectedIndexChanged(object sender, System.EventArgs e) {
            if (SelectedIndexChanged != null) {
                SelectedIndexChanged(sender, e);	//Raising the event
            }
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}