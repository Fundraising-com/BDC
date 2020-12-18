using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.PostalAddressEntityTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CustomerInfo.
    /// </summary>
    public partial class PostalAddressInfo : BaseWebUserControl {
        protected dataDef dtAddress = new dataDef();
        private int c_ParentID = 0;
        private int c_AddressID = 0;
        private int c_ParentType = 0;
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private QSPForm.Business.PostalAddressSystem addrSys = new QSPForm.Business.PostalAddressSystem();
        protected System.Data.DataTable tblState = new DataTable();
        protected System.Data.DataTable tblTypeAddress = new DataTable();
        protected System.Web.UI.HtmlControls.HtmlTable tblAddButton;
        bool c_HideTypeAddress = false;
        bool c_HideTitleAddress = true;
        int c_FilterTypeAddress = 0;
        protected DataView DVAddress;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here								
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
            this.dtLstAddress.ItemCreated += new DataListItemEventHandler(dtLstAddress_ItemCreated);
        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList								
                BindDataList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void LoadDataSet() {
            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.

            // Attempt to fill the temporary dataset.
            //dtAddress = addrSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        private void BindDataList() {
            DVAddress = new DataView(dtAddress);
            if (c_FilterTypeAddress > 0)
                DVAddress.RowFilter = PostalAddressEntityTable.FLD_TYPE + " = " + c_FilterTypeAddress.ToString();
            dtLstAddress.DataBind();
        }

        public int Count {
            get {
                return this.dtLstAddress.Items.Count;
            }
        }

        public PostalAddressEntityTable DataSource {
            get {
                return dtAddress;
            }
            set {
                dtAddress = value;
            }
        }

        public int AddressID {
            get {
                return c_AddressID;
            }
            set {
                c_AddressID = value;
            }
        }

        public RepeatDirection RepeatDirection {
            get {
                return dtLstAddress.RepeatDirection;
            }
            set {
                dtLstAddress.RepeatDirection = value;
            }
        }

        public int RepeatColumns {
            get {
                return dtLstAddress.RepeatColumns;
            }
            set {
                dtLstAddress.RepeatColumns = value;
            }
        }

        public int ParentID {
            get {
                return c_ParentID;
            }
            set {
                c_ParentID = value;
            }
        }

        public int ParentType {
            //Identify on wich we have to do our operation
            //0= Nothing (direct to the postal address table)
            //1= Organization
            //2= Account
            //3= Campaign
            //4= Order
            get {
                return c_ParentType;
            }
            set {
                c_ParentType = value;
            }
        }

        public bool HideTypeAddress {
            get {
                return c_HideTypeAddress;
            }
            set {
                c_HideTypeAddress = value;
            }
        }

        public bool HideTitleAddress {
            get {
                return c_HideTitleAddress;
            }
            set {
                c_HideTitleAddress = value;
            }
        }

        public int FilterTypeAddress {
            get {
                return c_FilterTypeAddress;
            }
            set {
                c_FilterTypeAddress = value;
            }
        }

        private void dtLstAddress_ItemCreated(object sender, DataListItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                HtmlTableRow htmlTblRowTypeAddress = (HtmlTableRow)e.Item.FindControl("htmlTblRowTypeAddress");
                if (htmlTblRowTypeAddress != null) {
                    htmlTblRowTypeAddress.Visible = !c_HideTypeAddress;
                }

                HtmlTableRow htmlTblRowTitleAddress = (HtmlTableRow)e.Item.FindControl("htmlTblRowTitleAddress");
                if (htmlTblRowTitleAddress != null) {
                    htmlTblRowTitleAddress.Visible = !c_HideTitleAddress;
                }
            }
        }
    }
}