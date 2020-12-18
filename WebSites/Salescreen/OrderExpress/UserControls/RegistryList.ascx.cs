using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.RegistryTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for RegistryList.
    /// </summary>
    public partial class RegistryList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_PKID + " DESC";
        protected DataView DVRegistry;
        private const string FILTER_EXCLUDE_ROLE_ID = "Filter_ExcludeRoleID";
        private const string PARAM_START = "Param_Start";
        private const string PARAM_END = "Param_End";

        protected dataDef dTblRegistry = new dataDef();

        protected System.Web.UI.WebControls.Label Label10;

        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            clsUtil.SetJScriptForOpenCalendar(hypLnkStartDate, txtStartDate);
            clsUtil.SetJScriptForOpenCalendar(hypLnkEndDate, txtEndDate);
            if (!IsPostBack) {
                //Manage Insert for Add Security				
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
            DVRegistry = new DataView(dTblRegistry);
            this.DataSource = DVRegistry;
            this.MainDataGrid = dtgRegistry;
            dtgRegistry.DataKeyField = RegistryTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }

        protected override void FillFilter() {
            ViewState[FILTER_EXCLUDE_ROLE_ID] = ddlExcludeRole.SelectedItem.Value;
            //F6 Modif
            DateTime StartDate = DateTime.Today;
            DateTime EndDate = DateTime.Today.AddDays(1);

            txtStartDate.Text = StartDate.ToShortDateString();
            ViewState[PARAM_START] = StartDate;

            txtEndDate.Text = EndDate.ToShortDateString();
            ViewState[PARAM_END] = EndDate;

            base.FillFilter();
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer
            QSPForm.Business.AuthSystem objSys = new QSPForm.Business.AuthSystem();

            string sCriteria = this.dtgRegistry.FilterExpression;

            switch (this.dtgRegistry.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }

            DateTime StartDate = Convert.ToDateTime(ViewState[PARAM_START]);
            DateTime EndDate = Convert.ToDateTime(ViewState[PARAM_END]);
            int ExcludeRoleID = Convert.ToInt32(ViewState[FILTER_EXCLUDE_ROLE_ID]);

            dTblRegistry = objSys.Registry_SelectAll_Search(this.dtgRegistry.SearchMode, sCriteria, StartDate, EndDate, ExcludeRoleID);

            DVRegistry = new DataView(dTblRegistry);
            DVRegistry.Sort = dtgRegistry.SortExpression;

            lblTotal.Text = "Number of Registry Line(s) : " + DVRegistry.Count.ToString();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_EXCLUDE_ROLE_ID] = ddlExcludeRole.SelectedItem.Value;
            ViewState[PARAM_START] = txtStartDate.Text;
            ViewState[PARAM_END] = txtEndDate.Text;
        }

        //		protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e)
        //		{
        //			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        //			{
        //				String sID = "0";
        //				if (e.Item.DataItem != null)
        //				{
        //					sID = ((DataRowView)e.Item.DataItem).Row[RegistryTable.FLD_PKID].ToString();
        //					string sIDName = RegistryDetail.CATALOG_ID;
        //					AppItem appItem = AppItem.RegistryDetail;
        //					clsUtil.SetJScriptForOpenDetail(e.Item, appItem, sIDName, sID, 0, 0);
        //					
        ////					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
        ////					if (imgBtnDetail != null)
        ////					{
        ////						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, appItem, sIDName, sID, 0,0);
        ////					}
        ////
        ////					HyperLink hypLnkName = (HyperLink) e.Item.FindControl("hypLnkName");
        ////					if (hypLnkName != null)
        ////					{
        ////						clsUtil.SetJScriptForOpenDetail(hypLnkName, appItem, sIDName, sID, 0,0);
        ////					}
        //				}
        //				
        //			}
        //			
        //		}
        //
        //		private void imgbtnAddRegistry_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        //		{
        ////			string strUrl = this.Page.GetPageToGo(QSPForm.Business.AppItem.RegistryForm_Step1);
        ////			Response.Redirect(strUrl);
        //		}
        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}