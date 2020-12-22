using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.CatalogTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CatalogHeaderDetailForm.
    /// </summary>
    public partial class CatalogHeaderDetailForm : BaseWebFormControl {
        private int c_CatalogID = 0;
        protected dataDef dtblCatalog = new dataDef();
        protected System.Web.UI.WebControls.Label lblExpression;
        protected System.Web.UI.WebControls.RangeValidator rangVal_StartDate;
        protected System.Web.UI.WebControls.RangeValidator rangVal_EndDate;
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
            }
        }

        #region Web Catalog Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Catalog Designer.
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

        protected override void LoadData() {
            //			dtblCatalog = campSys.SelectOne(c_CatalogID);
            //			base.LoadData ();
        }

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList				
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public int CatalogID {
            get {
                return c_CatalogID;
            }
            set {
                c_CatalogID = value;
                ViewState["CatalogID"] = c_CatalogID;
            }
        }

        public dataDef DataSource {
            get {
                return dtblCatalog;
            }
            set {
                dtblCatalog = value;
            }
        }

        public override void BindForm() {
            FillList();
            if (dtblCatalog.Rows.Count > 0) {
                DataRow row;
                row = dtblCatalog.Rows[0];
                txtCatalogID.Text = row[dataDef.FLD_PKID].ToString();
                txtName.Text = row[dataDef.FLD_NAME].ToString();
                txtCatalogCode.Text = row[dataDef.FLD_CODE].ToString();
                txtDescription.Text = row[dataDef.FLD_DESCRIPTION].ToString();
                if (row[dataDef.FLD_START_DATE] != DBNull.Value)
                    txtStartDate.Text = Convert.ToDateTime(row[dataDef.FLD_START_DATE]).ToShortDateString();
                if (row[dataDef.FLD_END_DATE] != DBNull.Value)
                    txtEndDate.Text = Convert.ToDateTime(row[dataDef.FLD_END_DATE]).ToShortDateString();

                if (row[dataDef.FLD_CULTURE] != DBNull.Value) {
                    ddlCulture.ClearSelection();
                    ListItem item = ddlCulture.Items.FindByValue(row[dataDef.FLD_CULTURE].ToString());
                    if (item != null) {
                        item.Selected = true;
                    }
                }
            }
            else {
                Page.SetPageMessage("This Catalog has been deleted");
            }
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;
            // get edited row values in grid
            DataRow row = dtblCatalog.Rows[0];

            row[dataDef.FLD_CODE] = txtCatalogCode.Text;
            row[dataDef.FLD_NAME] = txtName.Text;
            row[dataDef.FLD_DESCRIPTION] = txtDescription.Text;
            if (ddlCulture.SelectedIndex > 0) {
                row[dataDef.FLD_CULTURE] = Convert.ToInt32(ddlCulture.SelectedValue);
            }
            else {
                row[dataDef.FLD_CULTURE] = DBNull.Value;
            }

            if (row.RowState == DataRowState.Added) {
                row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
            }
            else {
                row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
            }

            IsSuccess = true;

            return IsSuccess;
        }

        private void FillList() {
            try {
                //Culture
                clsUtil.SetCultureDropDownList(ddlCulture, true);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}