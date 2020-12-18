using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.FormTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for FormHeaderDetailForm.
    /// </summary>
    public partial class FormHeaderDetailForm : BaseWebFormControl {
        private int c_FormID = 0;
        protected dataDef dtblForm = new dataDef();
        protected DataTable dtblProgType = new DataTable();
        protected ProgramTable dtblProgram = new ProgramTable();
        protected DataTable dtblEntityType = new DataTable();
        protected DataTable dtblPostalType = new DataTable();
        protected FormTable dtblParentForm = new FormTable();
        QSPForm.Business.FormSystem campSys = new QSPForm.Business.FormSystem();
        protected System.Web.UI.WebControls.Label lblExpression;
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
                bool IsNew = false;
                if (dtblForm.Rows.Count > 0) {
                    if (dtblForm.Rows[0].RowState == DataRowState.Added)
                        IsNew = true;
                }
                //Init
                trParentForm.Visible = false;
                trEntityTypeEdit.Visible = false;
                trEntityTypeInfo.Visible = true;
                if (!IsNew) {
                    trParentForm.Visible = (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_SUPER_USER);
                    trEntityTypeEdit.Visible = (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_SUPER_USER);
                    trEntityTypeInfo.Visible = false;
                }
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

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        protected override void LoadData() {
            //			dtblForm = campSys.SelectOne(c_FormID);
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

        public int FormID {
            get {
                return c_FormID;
            }
            set {
                c_FormID = value;
                ViewState["FormID"] = c_FormID;
            }
        }

        public dataDef DataSource {
            get {
                return dtblForm;
            }
            set {
                dtblForm = value;
            }
        }

        public int EntityTypeID {
            get {
                int entityTypeID = 0;
                if (ddlEntityType.SelectedIndex > 0)
                    entityTypeID = Convert.ToInt32(ddlEntityType.SelectedValue);
                return entityTypeID;
            }
        }

        public int BaseFormID {
            get {
                int baseFormID = 0;
                if (ddlParentForm.SelectedIndex > -1)
                    baseFormID = Convert.ToInt32(ddlParentForm.SelectedValue);
                return baseFormID;
            }
        }

        public override void BindForm() {
            FillList();
            if (dtblForm.Rows.Count > 0) {
                DataRow row;
                row = dtblForm.Rows[0];
                txtFormID.Text = row[dataDef.FLD_PKID].ToString();
                lblFormGroupID.Text = row[dataDef.FLD_FORM_GROUP_ID].ToString();
                lblVersion.Text = row[dataDef.FLD_VERSION].ToString();
                txtName.Text = row[dataDef.FLD_FORM_NAME].ToString();
                txtFormCode.Text = row[dataDef.FLD_FORM_CODE].ToString();
                txtDescription.Text = row[dataDef.FLD_DESCRIPTION].ToString();
                txtProgramBasics.Text = row[dataDef.FLD_PROGRAM_BASICS_TEXT].ToString();
                txtOrderTerms.Text = row[dataDef.FLD_ORDER_TERMS_TEXT].ToString();

                if (row[dataDef.FLD_PROGRAM_TYPE_ID] != DBNull.Value) {
                    ListItem item = ddlProgType.Items.FindByValue(row[dataDef.FLD_PROGRAM_TYPE_ID].ToString());
                    if (item != null) {
                        item.Selected = true;
                    }
                }
                if (row[dataDef.FLD_PROGRAM_ID] != DBNull.Value) {
                    ListItem item = ddlProgram.Items.FindByValue(row[dataDef.FLD_PROGRAM_ID].ToString());
                    if (item != null) {
                        item.Selected = true;
                    }
                }
                if (row[dataDef.FLD_ENTITY_TYPE_ID] != DBNull.Value) {
                    ListItem item = ddlEntityType.Items.FindByValue(row[dataDef.FLD_ENTITY_TYPE_ID].ToString());
                    if (item != null) {
                        item.Selected = true;
                        lblEntityType.Text = item.Text;
                    }
                }
                if (row[dataDef.FLD_TAX_POSTAL_ADDRESS_TYPE_ID] != DBNull.Value) {
                    ListItem item = ddlTaxPostalType.Items.FindByValue(row[dataDef.FLD_TAX_POSTAL_ADDRESS_TYPE_ID].ToString());
                    if (item != null) {
                        item.Selected = true;
                    }
                }
                if (row[dataDef.FLD_PARENT_FORM_ID] != DBNull.Value) {
                    ListItem item = ddlParentForm.Items.FindByValue(row[dataDef.FLD_PARENT_FORM_ID].ToString());
                    if (item != null) {
                        item.Selected = true;
                    }
                }
                if (!row.IsNull(dataDef.FLD_IS_PRODUCT_PRICE_UPDATABLE))
                    chkBoxIsProductPriceUpdatable.Checked = Convert.ToBoolean(row[dataDef.FLD_IS_PRODUCT_PRICE_UPDATABLE]);
                else
                    chkBoxIsProductPriceUpdatable.Checked = false;

                if (!row.IsNull(dataDef.FLD_IS_QUANTITY_ADJUSTMENT_ALLOWED))
                    chkBoxIsQtyAdjustmentAllowed.Checked = Convert.ToBoolean(row[dataDef.FLD_IS_QUANTITY_ADJUSTMENT_ALLOWED]);
                else
                    chkBoxIsQtyAdjustmentAllowed.Checked = false;

                if (!row.IsNull(dataDef.FLD_CLOSING_TIMES))
                    txtClosingTime.Text = Convert.ToDateTime(row[dataDef.FLD_CLOSING_TIMES]).ToShortTimeString();
                else
                    txtClosingTime.Text = "00:00";

                txtImageURL.Text = row[dataDef.FLD_IMAGE_URL].ToString();

                if (!row.IsNull(dataDef.FLD_IS_BASE_FORM))
                    chkBoxIsBaseForm.Checked = Convert.ToBoolean(row[dataDef.FLD_IS_BASE_FORM]);
                else
                    chkBoxIsBaseForm.Checked = false;

                if (!row.IsNull(dataDef.FLD_ENABLED))
                    chkBoxEnabled.Checked = Convert.ToBoolean(row[dataDef.FLD_ENABLED]);
                else
                    chkBoxEnabled.Checked = false;
            }
            else {
                Page.SetPageMessage("This Form has been deleted");
            }
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;
            // get edited row values in grid
            DataRow row = dtblForm.Rows[0];

            row[dataDef.FLD_FORM_CODE] = txtFormCode.Text;
            row[dataDef.FLD_FORM_NAME] = txtName.Text;
            row[dataDef.FLD_DESCRIPTION] = txtDescription.Text;
            row[dataDef.FLD_PROGRAM_BASICS_TEXT] = txtProgramBasics.Text;
            row[dataDef.FLD_ORDER_TERMS_TEXT] = txtOrderTerms.Text;
            if (ddlProgType.SelectedIndex > 0) {
                row[dataDef.FLD_PROGRAM_TYPE_ID] = Convert.ToInt32(ddlProgType.SelectedValue);
            }
            else {
                row[dataDef.FLD_PROGRAM_TYPE_ID] = DBNull.Value;
            }
            if (ddlProgram.SelectedIndex > 0) {
                row[dataDef.FLD_PROGRAM_ID] = Convert.ToInt32(ddlProgram.SelectedValue);
            }
            else {
                row[dataDef.FLD_PROGRAM_ID] = DBNull.Value;
            }
            if (ddlEntityType.SelectedIndex > 0) {
                row[dataDef.FLD_ENTITY_TYPE_ID] = Convert.ToInt32(ddlEntityType.SelectedValue);
            }
            else {
                row[dataDef.FLD_ENTITY_TYPE_ID] = DBNull.Value;
            }
            if (ddlTaxPostalType.SelectedIndex > 0) {
                row[dataDef.FLD_TAX_POSTAL_ADDRESS_TYPE_ID] = Convert.ToInt32(ddlTaxPostalType.SelectedValue);
            }
            else {
                row[dataDef.FLD_TAX_POSTAL_ADDRESS_TYPE_ID] = DBNull.Value;
            }
            if (ddlParentForm.SelectedIndex > 0) {
                row[dataDef.FLD_PARENT_FORM_ID] = Convert.ToInt32(ddlParentForm.SelectedValue);
            }
            else {
                row[dataDef.FLD_PARENT_FORM_ID] = DBNull.Value;
            }

            row[dataDef.FLD_IS_PRODUCT_PRICE_UPDATABLE] = chkBoxIsProductPriceUpdatable.Checked;
            row[dataDef.FLD_IS_QUANTITY_ADJUSTMENT_ALLOWED] = chkBoxIsQtyAdjustmentAllowed.Checked;
            row[dataDef.FLD_CLOSING_TIMES] = Convert.ToDateTime(txtClosingTime.Text);
            row[dataDef.FLD_IMAGE_URL] = txtImageURL.Text;
            row[dataDef.FLD_ENABLED] = chkBoxEnabled.Checked;

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
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                //Entity Type
                dtblEntityType = comSys.SelectAllEntityType();
                dtblEntityType.Rows.InsertAt(dtblEntityType.NewRow(), 0);
                ddlEntityType.DataSource = dtblEntityType;
                ddlEntityType.DataBind();
                //Program Type
                dtblProgType = comSys.SelectAllProgramType();
                dtblProgType.Rows.InsertAt(dtblProgType.NewRow(), 0);
                ddlProgType.DataSource = dtblProgType;
                ddlProgType.DataBind();

                //Program
                QSPForm.Business.ProgramSystem prgSys = new QSPForm.Business.ProgramSystem();
                dtblProgram = prgSys.SelectAll();
                dtblProgram.Rows.InsertAt(dtblProgram.NewRow(), 0);
                ddlProgram.DataSource = dtblProgram;
                ddlProgram.DataBind();

                //Postal Address Type
                dtblPostalType = comSys.SelectAllPostalAddressType();
                dtblPostalType.Rows.InsertAt(dtblPostalType.NewRow(), 0);
                ddlTaxPostalType.DataSource = dtblPostalType;
                ddlTaxPostalType.DataBind();

                QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
                //Parent Form
                dtblParentForm = formSys.SelectAll(true);
                DataRow newRow = dtblParentForm.NewRow();
                newRow[dataDef.FLD_FORM_CODE] = "BASE";
                dtblParentForm.Rows.InsertAt(newRow, 0);
                DataView dvBaseForm = new DataView(dtblParentForm);
                dvBaseForm.RowFilter = dataDef.FLD_FORM_CODE + " LIKE 'BASE*'";

                ddlParentForm.DataSource = dvBaseForm;
                ddlParentForm.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected int getSelectedIndex(DataTable dt, String sValue) {
            int iIndex = -1;
            try {
                if (sValue != "") {
                    DataView dv = new DataView(dt);
                    dv.Sort = dt.Columns[0].ColumnName;
                    iIndex = dv.Find(sValue);
                }

            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
            return iIndex;
        }
    }
}