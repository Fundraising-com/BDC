using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.TemplateEmailTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>TemplateEmail item Information - read only</summary>
    public partial class TemplateEmailInfo : BaseWebFormControl {
        private CommonUtility util = new CommonUtility();
        protected dataRef dTblTemplateEmail;

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

        protected void Page_Load(object sender, System.EventArgs e) {
        }

        public dataRef DataSource {
            get {
                return dTblTemplateEmail;
            }
            set {
                dTblTemplateEmail = value;
            }
        }

        public override void BindForm() {
            if (dTblTemplateEmail.Rows.Count > 0) {
                DataRow row = dTblTemplateEmail.Rows[0];

                lblTemplateID.Text = row[dataRef.FLD_PKID].ToString();
                lblTemplateName.Text = row[dataRef.FLD_TEMPLATE_EMAIL_NAME].ToString();
                lblDescription.Text = row[dataRef.FLD_DESCRIPTION].ToString();
                lblFrom.Text = row[dataRef.FLD_FROM].ToString();
                lblSubject.Text = row[dataRef.FLD_SUBJECT].ToString();
                lblBodyHTML.Text = row[dataRef.FLD_BODY_HTML].ToString();
                lblStoredProcName.Text = row[dataRef.FLD_TEMPLATE_EMAIL_SP].ToString();
                lblStoredProcParameterName.Text = row[dataRef.FLD_PARAMETER_NAME].ToString();
            }
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }
    }
}