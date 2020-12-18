using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.CMDRTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CampaignSelector.
    /// </summary>
    public partial class MDRSchoolSelector : BaseWebSubFormControl {
        private const string DEFAULT_SORT = "Name";
        protected dataDef dTblList = new dataDef();
        protected DataView DVList;
        private String IDRefCtrl = "";
        private String NameRefCtrl = "";
        private CommonUtility clsUtil = new CommonUtility();
        public event System.EventHandler SelectedIndexChanged;
        private bool buttonVisible = true;
        private const string FILTER_TYPE = "Filter_Type";
        private const string FILTER_STATE = "Filter_State";

        override protected void OnLoad(System.EventArgs e) {
            // Put user code to initialize the page here
            if (Request["IDRefCtrl"] != null) {
                IDRefCtrl = Request["IDRefCtrl"].ToString();
            }

            if (Request["NameRefCtrl"] != null) {
                NameRefCtrl = Request["NameRefCtrl"].ToString();
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
            this.QSPFormSearchModule.OnSearch += new SearchModuleEventHandler(this.QSPFormSearchModule_OnSearch);
        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVList = new DataView(dTblList);
            this.DataSource = DVList;
            this.MainDataGrid = dtgList;
            dtgList.DataKeyField = dataDef.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;

            this.dtgList.ItemCommand += new DataGridCommandEventHandler(dtgList_ItemCommand);
        }

        #endregion

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer
            if (!IsPostBack)
                return;

            QSPForm.Business.MDRSystem objSys = new QSPForm.Business.MDRSystem();

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
            string StateCode = ViewState[FILTER_STATE].ToString();
            int OrgType = Convert.ToInt32(ViewState[FILTER_TYPE]);

            dTblList = objSys.SelectAll_Search(dtgList.SearchMode, sCriteria, OrgType, StateCode);

            DVList = new DataView(dTblList);
            DVList.Sort = this.dtgList.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVList;

            lblTotal.Text = "Number of MDR School(s) : " + DVList.Count.ToString();
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();

            clsUtil.SetOrganizationTypeDropDownList(ddlOrganizationType, true);
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlOrganizationType.SelectedItem.Value);

            clsUtil.SetUSStateDropDownList(ddlState, true);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;

            base.FillFilter();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlOrganizationType.SelectedItem.Value);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {

            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                string sValueField = "PID";
                //string sTextField = dataDef.FLD_NAME;
                String sID = "0";
                sID = ((DataRowView)e.Item.DataItem).Row[sValueField].ToString();
                //	clsUtil.SetJScriptForOpenDetail(e.Item,AppItem.MDR_Detail,"MDRPID",sID,0,0,"OnDblClick");
                clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "MDR_Detail.aspx?", "MDRPID", sID, 0, 0, "OnDblClick");

                ImageButton imgBtnDetail = (ImageButton)e.Item.FindControl("imgBtnDetail");
                if (imgBtnDetail != null) {
                    //clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.MDR_Detail,"MDRPID", sID, 0,0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetail, "MDR_Detail.aspx?", "MDRPID", sID, 0, 0);
                }

                Label lblOrg = (Label)e.Item.FindControl("lblOrganizationID");
                ImageButton imgBtnSelect = (ImageButton)e.Item.FindControl("imgBtnSelect");
                if (lblOrg != null) {
                    if ((lblOrg.Text.Trim() != String.Empty) && (lblOrg.Text != "0")) {
                        imgBtnSelect.Attributes.Add("onclick", "return confirm('A QSP Organization is in the system for this MDR School.  Do you want to Add a New Account to this organization?');");
                    }
                }
            }
        }

        private void dtgList_ItemCommand(object source, DataGridCommandEventArgs e) {
            //SelectedOrganization = Convert.ToInt32( ((Label)e.Item.FindControl("lblOrganizationID")).Text );

            if (buttonVisible) {
                if (e.CommandName.ToLower() == "select") {
                    string sID = "";
                    string sName = "";
                    sID = ((Label)e.Item.FindControl("lblID")).Text;
                    sName = ((HyperLink)e.Item.FindControl("hypLnkName")).Text;
                    this.Page.RegisterClientScriptBlock("scriptClose", clsUtil.GetJScriptForCloseSelector(sID, sName, IDRefCtrl, NameRefCtrl));
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e) {
            trButton.Visible = buttonVisible;

            //			if (buttonVisible)
            //			{
            //				if (dtgList.SelectedIndex > -1)
            //				{
            //					DataGridItem dgItem = dtgList.Items[dtgList.SelectedIndex];
            //					Label lblID = (Label) dgItem.FindControl("lblID");
            //					if (lblID != null)
            //					{
            //						if (lblID.Text.Length >0)
            //						{
            //							String sID = lblID.Text;
            //							HyperLink hypLnkName = (HyperLink) dgItem.FindControl("hypLnkName");
            //							String sName = hypLnkName.Text;
            //							clsUtil.SetJScriptForCloseSelector(imgBtnOK,sID,sName,IDRefCtrl,NameRefCtrl);
            //						}
            //					}
            //				}
            //			}
        }

        public string SelectedValue {
            get {
                string sValue = "";
                try {
                    if (dtgList.SelectedIndex > -1) {
                        sValue = dtgList.DataKeys[dtgList.SelectedIndex].ToString();
                    }
                }
                catch (Exception ex) { }

                return sValue;
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

        public int SelectedOrganization {
            get {
                int iValue = 0;
                try {
                    if (dtgList.SelectedIndex > -1) {
                        string sValue = "";
                        sValue = ((Label)dtgList.Items[dtgList.SelectedIndex].FindControl("lblOrganizationID")).Text;
                        if (sValue.Length > 0)
                            iValue = Convert.ToInt32(sValue);
                    }
                }
                catch (Exception ex) { }
                return iValue;
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