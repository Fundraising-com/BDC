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
    ///		Summary description for VendorList.
    /// </summary>
    public partial class VendorList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_NAME + " ASC";
        protected System.Web.UI.WebControls.Label Label1;
        protected DataView DVVendor;
        private const string FILTER_STATE = "Filter_State";
        protected dataDef dTblVendor = new dataDef();

        protected System.Web.UI.WebControls.Label Label3;
        protected System.Web.UI.WebControls.Label Label10;

        private CommonUtility clsUtil = new CommonUtility();
        private const string FILTER_STATUS_CATEGORY = "Filter_Status";

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
                //Manage Insert for Add Security
                this.imgbtnAddVendor.Visible = (this.Page.Role == AuthSystem.ROLE_SUPER_USER); // (this.Page.RightInsert);
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
        }

        #endregion

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVVendor = new DataView(dTblVendor);
            this.DataSource = DVVendor;
            this.MainDataGrid = dtgVendor;
            dtgVendor.DataKeyField = VendorTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer
            QSPForm.Business.VendorSystem venSys = new QSPForm.Business.VendorSystem();

            string sCriteria = this.dtgVendor.FilterExpression;

            switch (this.dtgVendor.SearchMode) {
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

            dTblVendor = venSys.SelectAll_Search(this.dtgVendor.SearchMode, sCriteria, SubdivisionCode);

            DVVendor = new DataView(dTblVendor);
            DVVendor.Sort = dtgVendor.SortExpression;

            lblTotal.Text = "Number of Vendor(s) : " + DVVendor.Count.ToString();
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();
            clsUtil.SetUSStateDropDownList(ddlState, true);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;

            base.FillFilter();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;
        }

        //        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e)
        //        {
        ////            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        ////            {
        ////                String sID = "0";
        ////                if (e.Item.DataItem != null)
        ////                {
        //////					sID = ((DataRowView)e.Item.DataItem).Row[VendorTable.FLD_PKID].ToString();
        //////					string sIDName = VendorDetailInfo.WH_ID;
        //////					clsUtil.SetJScriptForOpenDetail(e.Item, AppItem.VendorDetailInfo, sIDName, sID, 720, 600);

        //////					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
        //////					if (imgBtnDetail != null)
        //////					{
        //////						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, AppItem.VendorDetailInfo, sIDName, sID, 0,0);
        //////					}
        //////					HyperLink hypLnkName = (HyperLink) e.Item.FindControl("hypLnkName");
        //////					if (hypLnkName != null)
        //////					{
        //////						clsUtil.SetJScriptForOpenDetail(hypLnkName, AppItem.VendorDetailInfo, sIDName, sID, 0,0);
        //////					}
        ////                }
        ////            }
        //        }

        private void imgbtnAddVendor_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //			string strUrl = this.Page.GetPageToGo(QSPForm.Business.AppItem.VendorForm_Step1);
            //			Response.Redirect(strUrl);
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}