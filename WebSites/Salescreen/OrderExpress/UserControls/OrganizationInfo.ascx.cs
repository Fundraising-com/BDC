using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.OrganizationData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class OrganizationInfo : BaseWebFormControl {
        private CommonUtility util = new CommonUtility();
        protected dataRef dtsOrganization;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.Label Label3;
        private int c_OrganizationID;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
                FillList();
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        public dataRef DataSource {
            get {
                return dtsOrganization;
            }
            set {
                dtsOrganization = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public int OrganizationID {
            get {
                return c_OrganizationID;
            }
            set {
                c_OrganizationID = value;
            }
        }

        public override void BindForm() {
            //Organization Detail
            OrganizationHeaderInfo_Final.DataSource = dtsOrganization.Organization;
            OrganizationHeaderInfo_Final.DataBind();

            //Bill To Address
            AddressInfo_Billing.ParentID = c_OrganizationID;
            AddressInfo_Billing.ParentType = QSPForm.Common.EntityType.TYPE_ORGANIZATION; //Org
            AddressInfo_Billing.DataSource = dtsOrganization;
            AddressInfo_Billing.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_BILLING; //Billing
            AddressInfo_Billing.DataBind();

            //Ship To Address
            AddressInfo_Shipping.ParentID = c_OrganizationID;
            AddressInfo_Shipping.ParentType = QSPForm.Common.EntityType.TYPE_ORGANIZATION; //Org
            AddressInfo_Shipping.DataSource = dtsOrganization;
            AddressInfo_Shipping.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING; //Shipping
            AddressInfo_Shipping.DataBind();

            AccountSubList_Final.OrganizationID = c_OrganizationID;
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }

        private void FillList() {
        }
    }
}