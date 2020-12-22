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
    ///		Summary description for AccountSubList.
    /// </summary>
    public partial class AccountSubList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = AccountTable.FLD_PKID + " ASC";
        protected DataView DVAccount;
        private int c_OrganizationID;
        protected dataDef dTblAccount = new dataDef();
        private CommonUtility clsUtil = new CommonUtility();
        public const string ACC_ID = "AccID";

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
                //				dtgAccount.Columns[1].Visible = (Page.Role > AuthSystem.ROLE_FM);
                //				dtgAccount.Columns[2].Visible = (Page.Role > AuthSystem.ROLE_FM);
            }
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
            DVAccount = new DataView(dTblAccount);
            this.DataSource = DVAccount;
            this.MainDataGrid = dtgAccount;
            dtgAccount.DataKeyField = AccountTable.FLD_PKID;
            base.LabelTotal = lblTotal;
        }
        #endregion

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
            //FM Hierarchy Filter
            string sFMID = "";
            if (Page.Role == AuthSystem.ROLE_FM) {
                sFMID = Page.FMID;
            }

            dTblAccount = accSys.SelectAllByOrganizationID(c_OrganizationID, sFMID, 1);

            DVAccount = new DataView();
            DVAccount.Table = dTblAccount;

            DVAccount.Sort = this.dtgAccount.SortExpression;
            lblTotal.Text = "Number of Account(s) : " + DVAccount.Count.ToString();
        }

        public int OrganizationID {
            get {
                return c_OrganizationID;
            }
            set {
                c_OrganizationID = value;
            }
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = ((DataRowView)e.Item.DataItem).Row[AccountTable.FLD_PKID].ToString();
                    string sIDName = ACC_ID; // BaseAccountDetail.ACC_ID;
                    clsUtil.SetJScriptForOpenDetail(e.Item, AppItem.AccountDetailInfo, sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "AccountDetailInfo.aspx?", sIDName, sID, 0, 0);


                    //					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
                    //					if (imgBtnDetail != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, AppItem.AccountDetail, sIDName, sID, 0,0);
                    //					}
                    //					HyperLink hypLnkName = (HyperLink) e.Item.FindControl("hypLnkName");
                    //					if (hypLnkName != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(hypLnkName, AppItem.AccountDetail, sIDName, sID, 0,0);
                    //					}
                }
            }
        }
    }
}