using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.VendorTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CampaignSelector.
    /// </summary>
    public partial class VendorSelector : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_NAME;
        protected dataDef dTblList = new dataDef();
        protected DataView DVList;
        private String IDRefCtrl = "";
        private String NameRefCtrl = "";
        private const string FILTER_STATE = "Filter_State";
        private CommonUtility clsUtil = new CommonUtility();
        //public event System.EventHandler SelectedIndexChanged;
        public event SelectedVendorHandler OnSelectedVendor;

        override protected void OnLoad(System.EventArgs e) {
            // Put user code to initialize the page here
            if (Request["IDRefCtrl"] != null) {
                IDRefCtrl = Request["IDRefCtrl"].ToString();
            }

            if (Request["NameRefCtrl"] != null) {
                NameRefCtrl = Request["NameRefCtrl"].ToString();
            }
            base.OnLoad(e);

            AdjustDisplay();
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
            DVList = new DataView(dTblList);
            this.DataSource = DVList;
            this.MainDataGrid = dtgList;
            dtgList.DataKeyField = dataDef.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;

            this.dtgList.ItemCommand += new DataGridCommandEventHandler(dtgList_ItemCommand);
        }

        #endregion

        #region

        public bool ShowButton {
            get {
                if (this.ViewState["ShowButton"] != null)
                    return Convert.ToBoolean(this.ViewState["ShowButton"].ToString());
                else
                    return true;
            }
            set {
                this.ViewState["ShowButton"] = value;
            }
        }

        public bool CloseAfterSelect {
            get {
                try {
                    if (this.ViewState["CloseAfterSelect"] != null)
                        return Convert.ToBoolean(this.ViewState["CloseAfterSelect"].ToString());
                    else
                        return true;
                }
                catch {
                    return true;
                }
            }
            set {
                this.ViewState["CloseAfterSelect"] = value;
            }
        }

        #endregion

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();
            clsUtil.SetUSStateDropDownList(ddlState, true);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;

            base.FillFilter();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer
            QSPForm.Business.VendorSystem objSys = new QSPForm.Business.VendorSystem();

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

            dTblList = objSys.SelectAll_Search(dtgList.SearchMode, sCriteria, SubdivisionCode);

            DVList = new DataView(dTblList);
            DVList.Sort = this.dtgList.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVList;

            lblTotal.Text = "Number of Vendor(s) : " + DVList.Count.ToString();
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                //Nothing for now
                //				string sValueField = "VendorNumber";
                //				//string sTextField = dataDef.FLD_NAME;
                //				String sID = "0";	
                //				sID = ((DataRowView)e.Item.DataItem).Row[sValueField].ToString();	
                //				clsUtil.SetJScriptForOpenDetail(e.Item,"CampaignDetail",CampaignDetail.CAMP_ID,sID,0,0,"OnDblClick");				

            }
        }

        private void AdjustDisplay() {
            this.imgBtnOK.Visible = this.ShowButton;
            this.hypLnkCancel.Visible = this.ShowButton;
        }

        //		private void Page_PreRender(object sender, EventArgs e)
        //		{
        //			if (dtgList.SelectedIndex > -1)
        //			{
        //				DataGridItem dgItem = dtgList.Items[dtgList.SelectedIndex];
        //				Label lblID = (Label) dgItem.FindControl("lblID");
        //				if (lblID != null)
        //				{
        //					if (lblID.Text.Length >0)
        //					{
        //						String sID = lblID.Text;
        //						HyperLink hypLnkName = (HyperLink) dgItem.FindControl("hypLnkName");
        //						String sName = hypLnkName.Text;
        //						clsUtil.SetJScriptForCloseSelector(imgBtnOK,sID,sName,IDRefCtrl,NameRefCtrl);
        //					}
        //				}
        //			}
        //		}

        private void dtgList_ItemCommand(object source, DataGridCommandEventArgs e) {
            if (!ShowButton) {
                if (e.CommandName.ToLower() == "select") {
                    if (CloseAfterSelect) {
                        string sID = "";
                        string sName = "";
                        sID = ((Label)e.Item.FindControl("lblID")).Text;
                        sName = ((HyperLink)e.Item.FindControl("hypLnkName")).Text;
                        this.Page.RegisterClientScriptBlock("scriptClose", clsUtil.GetJScriptForCloseSelector(sID, sName, IDRefCtrl, NameRefCtrl));
                    }
                    else {
                        //dtgList.SelectedIndex = e.Item.ItemIndex;
                        //SelectedIndexChanged(this, e);
                        string sID = ((Label)e.Item.FindControl("lblID")).Text;
                        OnSelectedVendor(this, new SelectedVendorArgs(Convert.ToInt32(sID)));
                    }
                }
            }
        }

        //protected void dtgList_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    if (SelectedIndexChanged != null)
        //    {
        //        SelectedIndexChanged(sender, e);	//Raising the event
        //    }
        //}

        public int SelectedVendorID {
            get {
                try {

                    return Convert.ToInt32(((Label)dtgList.SelectedItem.FindControl("lblID")).Text);
                }
                catch {
                    return 0;
                }
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

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}