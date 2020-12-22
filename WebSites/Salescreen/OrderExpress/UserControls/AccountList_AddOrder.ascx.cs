using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using System.Text.RegularExpressions;
using dataDef = QSPForm.Common.DataDef.AccountTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AccountList.
    /// </summary>
    public partial class AccountList_AddOrder : BaseWebSubFormControl {
        private const string DEFAULT_SORT = AccountTable.FLD_NAME + " ASC";
        protected DataView DVAccount;
        private const string FILTER_STATE = "Filter_State";
        private const string FILTER_TYPE = "Filter_Type";
        public const string PARAM_PROG = "ProgType";
        private const string FILTER_FSM_DISPLAY_MODE = "Filter_FSM_DisplayMode";
        protected dataDef dTblAccount = new dataDef();
        private CommonUtility clsUtil = new CommonUtility();
        private static Regex _isNumber = new Regex(@"^\d+$");
        public event System.EventHandler SelectedIndexChanged;

        protected void Page_Load(object sender, System.EventArgs e) {
            trFieldSupportFilterOption.Visible = (Page.Role > AuthSystem.ROLE_FM);
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
                case 2:
                    bool IsInt = IsInteger(sCriteria);

                    if (!IsInt) {
                        if (sCriteria.Contains("-")) {
                            //int index = sCriteria.LastIndexOf("-");
                            //sCriteria = sCriteria.Substring(index+1);
                            sCriteria = sCriteria.Replace("-", "");
                            sCriteria = "%" + sCriteria + "%";
                        }
                        else {
                            sCriteria = string.Empty;
                        }
                    }
                    break;
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

        public static bool IsInteger(string theValue) {
            Match m = _isNumber.Match(theValue);
            return m.Success;
        } //IsInteger

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();
            clsUtil.SetProgramTypeDropDownList(ddlProgramType, false);
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
                Label lblStatusID = ((Label)e.Item.FindControl("lblStatusID"));
                ImageButton imgBtnSelect = (ImageButton)e.Item.FindControl("imgBtnSelect");
                if (lblStatusID != null) {
                    int StatusID = 0;
                    if (lblStatusID.Text.Trim().Length > 0) {
                        StatusID = Convert.ToInt32(lblStatusID.Text.Trim());
                        if (imgBtnSelect != null) {
                            if (StatusID == QSPForm.Common.AccountStatus.CLOSE ||
                                StatusID == QSPForm.Common.AccountStatus.CLOSE_PROCESSED ||
                                StatusID == QSPForm.Common.AccountStatus.CLOSE_IN_PROCESS ||
                                StatusID == QSPForm.Common.AccountStatus.CLOSE_NOT_SUBMITTED) {
                                imgBtnSelect.Attributes.Add("onclick", "alert('Account Closed - Please Call QSP Field Support For Assistance.');return false;");
                            }
                            else if (StatusID == QSPForm.Common.AccountStatus.IN_COLLECTION ||
                                    StatusID == QSPForm.Common.AccountStatus.IN_COLLECTION_PROCESSED) {
                                imgBtnSelect.Attributes.Add("onclick", "alert('Account In Collection - Please Call QSP Field Support For Assistance.');return false;");
                            }
                            string sCtrlID = imgBtnSelect.ClientID;
                            e.Item.Attributes.Add("OnClick", "document.getElementById('" + sCtrlID + "').click();");
                        }
                    }

                    BusinessCalendarSystem calSys = new BusinessCalendarSystem();
                    int fiscalYear = Convert.ToInt32(((Label)e.Item.FindControl("lblFiscalYear")).Text);
                    if (fiscalYear > calSys.GetFiscalYear())
                    {
                        //imgBtnSelect.Attributes.Add("onclick", "alert('Account Scheduled For Next Fiscal Year - Cannot Accept Order Until Then.');return false;");
                    }
                }

                #region
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
                #endregion
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

        public int SelectedIndex {
            get {
                return dtgAccount.SelectedIndex;
            }
            set {
                dtgAccount.SelectedIndex = value;
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

        protected void dtgAccount_SelectedIndexChanged(object sender, System.EventArgs e) {
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