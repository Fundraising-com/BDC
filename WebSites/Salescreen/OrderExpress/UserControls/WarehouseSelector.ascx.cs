using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.WarehouseTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CampaignSelector.
    /// </summary>
    public partial class WarehouseSelector : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_NAME;
        protected dataDef dTblList = new dataDef();
        protected DataView DVList;
        private int c_FormID = 0;
        public const string FORM_ID = "FormID";
        private String IDRefCtrl = "";
        private String NameRefCtrl = "";
        private const string FILTER_STATE = "Filter_State";
        private CommonUtility clsUtil = new CommonUtility();

        override protected void OnLoad(System.EventArgs e) {
            // Put user code to initialize the page here
            if (Request["IDRefCtrl"] != null) {
                IDRefCtrl = Request["IDRefCtrl"].ToString();
            }

            if (Request["NameRefCtrl"] != null) {
                NameRefCtrl = Request["NameRefCtrl"].ToString();
            }

            if (Request[FORM_ID] != null) {
                c_FormID = Convert.ToInt32(Request[FORM_ID]);
            }

            CommonUtility clsUtil = new CommonUtility();
            //clsUtil.SetJScriptForOpenDetail(hypLnk_Map, QSPForm.Business.AppItem.WarehouseMapInfo,"","",0,0);
            clsUtil.SetJScriptForOpenDetailNoCMS(hypLnk_Map, "WarehouseMapInfo.aspx?", "", "", 0, 0);
            //string sUrl = Page.GetPageToGo(QSPForm.Business.AppItem.WarehouseMapInfo);

            base.OnLoad(e);
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
            QSPForm.Business.WarehouseSystem objSys = new QSPForm.Business.WarehouseSystem();

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

            dTblList = objSys.SelectAll_Search(dtgList.SearchMode, sCriteria, SubdivisionCode, true);

            DVList = new DataView(dTblList);
            DVList.Sort = this.dtgList.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVList;

            lblTotal.Text = "Number of Warehouse(s) : " + DVList.Count.ToString();
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {

            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = ((DataRowView)e.Item.DataItem).Row[WarehouseTable.FLD_PKID].ToString();
                    string sIDName = WarehouseDetailInfo.WH_ID;
                    HyperLink hypLnkName = (HyperLink)e.Item.FindControl("hypLnkName");
                    if (hypLnkName != null) {
                        if (c_FormID > 0) {
                            //clsUtil.SetJScriptForOpenDetail(hypLnkName, AppItem.WarehouseDetailInfo, sIDName, sID, 0, 0, "OnClick", "&FormID=" + c_FormID.ToString());
                            clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkName, "WarehouseDetailInfo.aspx?", sIDName, sID, 0, 0, "OnClick", "&FormID=" + c_FormID.ToString());
                        }
                        else {
                            // clsUtil.SetJScriptForOpenDetail(hypLnkName, AppItem.WarehouseDetailInfo, sIDName, sID, 0, 0);
                            clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkName, "WarehouseDetailInfo.aspx?", sIDName, sID, 0, 0);
                        }
                        hypLnkName.ToolTip = "Click here to access Product Inventory";
                    }
                }
            }
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

            if (e.CommandName.ToLower() == "select") {
                string sID = "";
                string sName = "";
                sID = ((Label)e.Item.FindControl("lblFulfID")).Text;
                sName = ((HyperLink)e.Item.FindControl("hypLnkName")).Text;
                this.Page.RegisterClientScriptBlock("scriptClose", clsUtil.GetJScriptForCloseSelector(sID, sName, IDRefCtrl, NameRefCtrl));
            }
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}