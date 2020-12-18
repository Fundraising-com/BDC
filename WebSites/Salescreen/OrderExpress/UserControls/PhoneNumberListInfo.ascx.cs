using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.PhoneNumberEntityTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for Phone Number List.
    /// </summary>
    public partial class PhoneNumberListInfo : BaseWebUserControl {
        protected dataDef dtPhoneNumber = new dataDef();
        protected System.Web.UI.WebControls.Label lblFormTitle;
        private int c_ParentID;
        private int c_PhoneID;
        private int c_ParentType;

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
        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList

                this.dtgPhoneNumber.EditItemIndex = -1;
                BindGrid();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        public int PhoneID {
            get {
                return c_PhoneID;
            }
            set {
                c_PhoneID = value;
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
            //4= Contact
            get {
                return c_ParentType;
            }
            set {
                c_ParentType = value;
            }
        }

        public PhoneNumberEntityTable DataSource {
            get {
                return dtPhoneNumber;
            }
            set {
                dtPhoneNumber = value;
            }
        }

        public void LoadDataSource() {
            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.
            //dtPhoneNumber = phoneSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        private void BindGrid() {
            this.dtgPhoneNumber.DataBind();
        }
    }
}