using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.CampaignTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CampaignSelector.
    /// </summary>
    public partial class CampaignSelector : BaseWebSubFormControl {
        private const string DEFAULT_SORT = CampaignTable.FLD_NAME + " ASC";
        protected dataDef dTblList = new dataDef();
        protected DataView DVList;
        private String IDRefCtrl = "";
        private String NameRefCtrl = "";
        private CommonUtility clsUtil = new CommonUtility();
        public event System.EventHandler SelectedIndexChanged;
        private bool buttonVisible = true;

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
        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVList = new DataView(dTblList);
            this.DataSource = DVList;
            this.MainDataGrid = dtgList;
            dtgList.DataKeyField = CampaignTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }

        #endregion

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer
            //			if (!IsPostBack)
            //				dtgList.FilterExpression = "A";
            QSPForm.Business.CampaignSystem campSys = new QSPForm.Business.CampaignSystem();

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
            BusinessCalendarSystem calSys = new BusinessCalendarSystem();
            int CurrentFY = calSys.GetFiscalYear();
            DateTime CurrentFirstDateFY = new DateTime((CurrentFY - 1), 7, 1);
            DateTime CurrentLastDateFY = CurrentFirstDateFY.AddYears(1).AddDays(-1);
            //In this case we just include those of the current FY
            //FM Hierarchy Filter
            string FMID = "";
            if (Page.Role == AuthSystem.ROLE_FM)
                FMID = Page.FMID;
            dTblList = campSys.SelectAll_Search(this.dtgList.SearchMode, sCriteria, FMID, CurrentFY, 0, "", CurrentFirstDateFY, CurrentLastDateFY, false);

            DVList = new DataView(dTblList);
            DVList.Sort = this.dtgList.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVList;

            lblTotal.Text = "Number of Campaign(s) : " + DVList.Count.ToString();
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {

            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                //string sValueField = dataDef.FLD_PKID;
                ////string sTextField = dataDef.FLD_NAME;
                //String sID = "0";	
                //sID = ((DataRowView)e.Item.DataItem).Row[sValueField].ToString();	
                //clsUtil.SetJScriptForOpenDetail(e.Item,QSPForm.Business.AppItem.CampaignDetail,CampaignDetail.CAMP_ID,sID,0,0,"OnDblClick");				
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