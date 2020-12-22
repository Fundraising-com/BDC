using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.AccountStatusChangeTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AccountSubList.
    /// </summary>
    public partial class AccountStatusChangeList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_PKID + " DESC";
        protected dataDef dTblAccountStatusChanges = new dataDef();
        protected DataView DVAccountStatusChanges;
        private CommonUtility clsUtil = new CommonUtility();
        private int c_AccountID;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            dtgAccount.Columns[1].Visible = (this.Page.Role == AuthSystem.ROLE_FM);
            dtgAccount.Columns[2].Visible = (this.Page.Role > AuthSystem.ROLE_FM);
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
            DVAccountStatusChanges = new DataView(dTblAccountStatusChanges);
            this.DataSource = DVAccountStatusChanges;
            this.MainDataGrid = dtgAccount;
            dtgAccount.DataKeyField = AccountTable.FLD_PKID;
            base.LabelTotal = lblTotal;

        }

        #endregion

        public int AccountID {
            get {
                return c_AccountID;
            }
            set {
                c_AccountID = value;
            }
        }

        private void GetParamQueryStringFilter() {
            if (Request["AccountID"] != null) {
                c_AccountID = Convert.ToInt32(Request["AccountID"]);

            }
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            QSPForm.Business.AccountSystem ordSys = new QSPForm.Business.AccountSystem();

            //Set the Account ID Parameter
            GetParamQueryStringFilter();

            dTblAccountStatusChanges = ordSys.SelectAllAccountStatusChangeByAccountID(c_AccountID);

            DVAccountStatusChanges = new DataView(dTblAccountStatusChanges);
            DVAccountStatusChanges.Sort = this.dtgAccount.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVAccountStatusChanges;

            lblTotal.Text = "Number of Change(s) : " + DVAccountStatusChanges.Count.ToString();
        }

        protected override void OnItemCreated(System.Web.UI.WebControls.DataGridItemEventArgs e) {

            //            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            //            {
            //                String sID = "0";
            //                if (e.Item.DataItem != null)
            //                {
            //                    //sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
            //                    sID = dtgAccount.DataKeys[e.Item.ItemIndex].ToString();
            //                    string sIDName = BaseAccountDetail.ORDER_ID;
            //                    clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.AccountDetailInfo, sIDName, sID, 0,0);

            ////					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
            ////					if (imgBtnDetail != null)
            ////					{
            ////						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.AccountDetail, sIDName, sID, 0,0);
            ////					}					
            //                }		
            //            }			
        }
    }
}