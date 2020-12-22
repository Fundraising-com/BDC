using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.TemplateEmailTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>ToDoHeaderForm</summary>
    public partial class TemplateEmailHeaderForm : BaseWebFormControl {
        #region Item Declarations

        private int c_TemplateEmailID = 0;
        private dataDef dtblTemplateEmail = new dataDef();

        QSPForm.Business.TemplateEmailSystem teSys = new QSPForm.Business.TemplateEmailSystem();

        private CommonUtility clsUtil = new CommonUtility();
        #endregion Item Declarations

        protected void Page_Load(object sender, System.EventArgs e) {
        }

        #region auto-generated code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion auto-generated code
        protected void btnValidateSP_Click(object sender, System.EventArgs e) {
            ShowAvailableTags();
        }

        protected override void LoadData() {
            this.DataSource = this.teSys.SelectOne(c_TemplateEmailID);
            base.LoadData();
        }

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public int TemplateEmailID {
            get {
                return c_TemplateEmailID;
            }
            set {
                c_TemplateEmailID = value;
            }
        }

        public string HtmlCode {
            get { return Server.HtmlDecode(HtmlEditor.Value); }
            set { this.HtmlEditor.Value = value; }
        }

        public dataDef DataSource {
            get {
                return dtblTemplateEmail;
            }
            set {
                dtblTemplateEmail = value;
            }
        }

        public override void BindForm() {
            LoadData();

            if (this.DataSource.Rows.Count > 0) {
                DataRow row;
                row = this.DataSource.Rows[0];

                int iTemplateEmailID = Convert.ToInt32(row[dataDef.FLD_PKID]);
                if (iTemplateEmailID == 0) {
                    this.lblTemplateID.Text = "New";
                }
                else {
                    this.lblTemplateID.Text = TemplateEmailID.ToString();
                }

                txtTemplateName.Text = row[dataDef.FLD_TEMPLATE_EMAIL_NAME].ToString();
                txtDescription.Text = row[dataDef.FLD_DESCRIPTION].ToString();
                txtFrom.Text = row[dataDef.FLD_FROM].ToString();
                txtSubject.Text = row[dataDef.FLD_SUBJECT].ToString();
                this.HtmlCode = row[dataDef.FLD_BODY_HTML].ToString();
                txtStoredProcName.Text = row[dataDef.FLD_TEMPLATE_EMAIL_SP].ToString();
                txtStoredProcParameterName.Text = row[dataDef.FLD_PARAMETER_NAME].ToString();

                ShowAvailableTags();
            }
            else if (this.TemplateEmailID == 0) {
                this.lblTemplateID.Text = "New";
            }
            else {
                string MSG = "This template item - id # " + this.TemplateEmailID.ToString() + " does not exist";
                MSG += "<br>The item may have been deleted";
                Page.SetPageMessage(MSG);
            }
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;

            DataRow row;
            row = this.DataSource.Rows[0];
            CommonUtility clsUtil = new CommonUtility();

            clsUtil.UpdateRow(row, dataDef.FLD_TEMPLATE_EMAIL_NAME, this.txtTemplateName.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_DESCRIPTION, this.txtDescription.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_FROM, this.txtFrom.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_SUBJECT, this.txtSubject.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_BODY_HTML, this.HtmlCode);
            clsUtil.UpdateRow(row, dataDef.FLD_TEMPLATE_EMAIL_SP, this.txtStoredProcName.Text);
            clsUtil.UpdateRow(row, dataDef.FLD_PARAMETER_NAME, this.txtStoredProcParameterName.Text);

            if (row.RowState != DataRowState.Unchanged) {
                if (row.RowState == DataRowState.Added) {
                    row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                }
                else {
                    row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                }
                IsSuccess = true;
            }
            return IsSuccess;
        }

        public bool ValidateForm() {
            this.Page.Validate();
            return this.IsValid();
        }

        public void ShowAvailableTags() {
            try {
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                DataTable result = comSys.SelectAllTags(this.txtStoredProcName.Text, this.txtStoredProcParameterName.Text);
                this.lblAvailableTag.Text = "";
                for (int i = 0; i < result.Columns.Count; i++) {
                    lblAvailableTag.Text += "[" + result.Columns[i].ColumnName + "]";
                    if (i < result.Columns.Count - 1)
                        lblAvailableTag.Text += " , ";
                }
            }
            catch {
                //do nothing
            }
        }
    }
}