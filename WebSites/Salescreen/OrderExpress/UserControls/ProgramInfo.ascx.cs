using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.ProgramTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>Program item Information - read only</summary>
    public partial class ProgramInfo : BaseWebFormControl {
        private CommonUtility util = new CommonUtility();
        protected dataRef dTblProgram;

        protected void Page_Load(object sender, System.EventArgs e) {
        }

        #region Web Form Designer generated code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion Web Form Designer generated code

        public dataRef DataSource {
            get {
                return dTblProgram;
            }
            set {
                dTblProgram = value;
            }
        }

        public override void BindForm() {
            if (dTblProgram.Rows.Count > 0) {
                DataRow row = dTblProgram.Rows[0];
                lblProgramID.Text = row[dataRef.FLD_PKID].ToString();
                lblProgramName.Text = row[dataRef.FLD_PROGRAM_NAME].ToString();

                lblProgramTypeName.Text = row[dataRef.FLD_PROGRAM_TYPE_NAME].ToString();
                lblDescription.Text = row[dataRef.FLD_DESCRIPTION].ToString();
            }
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }
    }
}