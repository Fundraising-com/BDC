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
    ///		Summary description for AccountSelector.
    /// </summary>
    public partial class AccountSelector : BaseWebSubFormControl {
        private const string DEFAULT_SORT = AccountTable.FLD_PKID + " ASC";
        protected dataDef dTblList = new dataDef();
        protected DataView DVList;
        private String IDRefCtrl = "";
        private String NameRefCtrl = "";
        private CommonUtility clsUtil = new CommonUtility();
        public event System.EventHandler SelectedIndexChanged;
        private bool buttonVisible = true;
        private int programTypeID = 0;

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
            this.PreRender += new EventHandler(Page_PreRender);
            this.dtgList.SelectedIndexChanged += new EventHandler(dtgList_SelectedIndexChanged);
        }

        #endregion

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVList = new DataView(dTblList);
            this.DataSource = DVList;
            this.MainDataGrid = dtgList;
            dtgList.DataKeyField = dataDef.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer
            //			if (!IsPostBack)
            //				dtgList.FilterExpression = "A";
            QSPForm.Business.AccountSystem objSys = new QSPForm.Business.AccountSystem();

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
            //FM Hierarchy Filter
            string FMID = "";
            if (Page.Role == AuthSystem.ROLE_FM)
                FMID = Page.FMID;
            dTblList = objSys.SelectAll_Search(dtgList.SearchMode, sCriteria, programTypeID, "", FMID, 0, 0, "");

            DVList = new DataView(dTblList);
            DVList.Sort = this.dtgList.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVList;

            lblTotal.Text = "Number of Account(s) : " + DVList.Count.ToString();
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = ((DataRowView)e.Item.DataItem).Row[AccountTable.FLD_PKID].ToString();
                    string sIDName = AccountDetailInfo.ACC_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, AppItem.AccountDetailInfo, sIDName, sID, 0, 0, "OnDblClick");
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "AccountDetailInfo.aspx?", sIDName, sID, 0, 0, "OnDblClick");

                    ImageButton imgBtnDetail = (ImageButton)e.Item.FindControl("imgBtnDetail");
                    if (imgBtnDetail != null) {
                        //clsUtil.SetJScriptForOpenDetail(imgBtnDetail, AppItem.AccountDetailInfo, sIDName, sID, 0,0);
                        clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetail, "AccountDetailInfo.aspx?", sIDName, sID, 0, 0);
                    }
                    HyperLink hypLnkName = (HyperLink)e.Item.FindControl("hypLnkName");
                    if (hypLnkName != null) {
                        //clsUtil.SetJScriptForOpenDetail(hypLnkName, AppItem.AccountDetailInfo, sIDName, sID, 0,0);
                        clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkName, "AccountDetailInfo.aspx?", sIDName, sID, 0, 0);
                    }
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e) {
            trButton.Visible = buttonVisible;

            if (buttonVisible) {
                if (dtgList.SelectedIndex > -1) {
                    DataGridItem dgItem = dtgList.Items[dtgList.SelectedIndex];
                    string sID = dtgList.DataKeys[dtgList.SelectedIndex].ToString();
                    if (sID.Length > 0) {
                        HyperLink hypLnkName = (HyperLink)dgItem.FindControl("hypLnkName");
                        String sName = hypLnkName.Text;
                        clsUtil.SetJScriptForCloseSelector(imgBtnOK, sID, sName, IDRefCtrl, NameRefCtrl);
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

        public string SelectedAccountNumber {
            get {
                string sValue = "";
                try {
                    if (dtgList.SelectedIndex > -1) {
                        sValue = ((Label)dtgList.Items[dtgList.SelectedIndex].FindControl("lblAccountNumber")).Text;
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

        public int ProgramTypeID {
            get {
                return programTypeID;
            }
            set {
                programTypeID = value;
            }
        }

        private void dtgList_SelectedIndexChanged(object sender, System.EventArgs e) {
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