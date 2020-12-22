using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.AccountTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AccountList.
    /// </summary>
    public partial class AccountList_Add : BaseWebSubFormControl {
        private const string DEFAULT_SORT = AccountTable.FLD_PKID + " DESC";
        protected DataView DVAccount;
        private const string FILTER_STATE = "Filter_State";
        private const string FILTER_TYPE = "Filter_Type";
        public const string PARAM_PROG = "ProgType";
        protected string addButton_ImageURL = "images/BtnAddOrder.gif";
        private const string FILTER_FSM_DISPLAY_MODE = "Filter_FSM_DisplayMode";
        protected dataDef dTblAccount = new dataDef();
        private CommonUtility clsUtil = new CommonUtility();
        public event System.EventHandler SelectedIndexChanged;
        private int c_EntityTypeID = 0;

        protected void Page_Load(object sender, System.EventArgs e) { }

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

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVAccount = new DataView(dTblAccount);
            this.DataSource = DVAccount;
            this.MainDataGrid = dtgAccount;
            dtgAccount.DataKeyField = dataDef.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }
        #endregion

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();

            string sCriteria = this.dtgAccount.FilterExpression;

            switch (this.dtgAccount.SearchMode) {
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
            int ProgramType = Convert.ToInt32(ViewState[FILTER_TYPE]);
            //FM Hierarchy Filter
            string sFMID = "";
            string sFMName = "";
            int FSM_DisplayMode = 0;
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
            dTblAccount = accSys.SelectAll_Search(this.dtgAccount.SearchMode, sCriteria, ProgramType, SubdivisionCode, sFMID, 0, 0, sFMName);

            DVAccount = new DataView();
            DVAccount.Table = dTblAccount;

            DVAccount.Sort = this.dtgAccount.SortExpression;
            lblTotal.Text = "Number of Account(s) : " + DVAccount.Count.ToString();
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();
            clsUtil.SetProgramTypeDropDownList(ddlProgramType, c_EntityTypeID, false);
            string sProgTypeID = "11";  //Set To Chocolate as Default
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

            base.FillFilter();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProgramType.SelectedItem.Value);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                ((Label)e.Item.FindControl("lblZip")).Text = clsUtil.FormatZipCode(((Label)e.Item.FindControl("lblZip")).Text);
                //				String sID = "0";
                //				if (e.Item.DataItem != null)
                //				{
                //					ImageButton imgBtnSelect = (ImageButton) e.Item.FindControl("imgBtnSelect");
                //					/*
                //					 *  Apply Jscript to skip the step 2 if the account is renew
                //					*/
                //					if(Convert.ToBoolean(((Label)e.Item.FindControl("lblIsRenew")).Text))
                //					{
                //						imgBtnSelect.Attributes["onclick"] = "SkipStep()"; 
                //					}
                //
                //					if (imgBtnSelect != null)
                //					{
                //						e.Item.Attributes.Add("OnClick", "document.getElementById('" + imgBtnSelect.ClientID + "').click();");	
                ////						HyperLink hypLnkName = (HyperLink) e.Item.FindControl("hypLnkName");
                ////						if (hypLnkName != null)
                ////						{
                ////							hypLnkName.NavigateUrl = "javascript:document.getElementById('" + imgBtnSelect.ClientID + "').click();";
                ////							//hypLnkName.Attributes.Add("OnClick", "alert('sdds');document.getElementById('" + imgBtnSelect.ClientID + "').click();");	
                ////						}
                //					}
                //					
                //				}	
            }
        }

        public int SelectedValue {
            get {
                int iValue = -1;
                try {
                    if (dtgAccount.SelectedIndex > -1) {
                        iValue = Convert.ToInt32(dtgAccount.DataKeys[dtgAccount.SelectedIndex]);
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
                    if (dtgAccount.SelectedIndex > -1) {
                        sValue = ((HyperLink)dtgAccount.Items[dtgAccount.SelectedIndex].FindControl("hypLnkName")).Text;
                    }
                }
                catch (Exception ex) { }
                return sValue;
            }
        }

        public string SelectedAccountNumber {
            get {
                string sValue = "";
                try {
                    if (dtgAccount.SelectedIndex > -1) {
                        sValue = ((Label)dtgAccount.Items[dtgAccount.SelectedIndex].FindControl("lblAccountNumber")).Text;
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

        public int EntityTypeID {
            get {
                return c_EntityTypeID;
            }
            set {
                c_EntityTypeID = value;
            }
        }

        protected void dtgAccount_SelectedIndexChanged(object sender, System.EventArgs e) {
            if (SelectedIndexChanged != null) {
                SelectedIndexChanged(sender, e);	//Raising the event	
            }
        }

        public string AddButton_ImageURL {
            get {
                return addButton_ImageURL;
            }
            set {
                addButton_ImageURL = value;
            }

        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}