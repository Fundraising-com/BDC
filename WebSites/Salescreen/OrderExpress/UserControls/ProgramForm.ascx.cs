using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.ProgramTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for ProgramForm.
    /// </summary>
    public partial class ProgramForm : BaseWebFormControl {
        protected dataDef dTblProgram;

        private int c_ProgramID = 0;

        private CommonUtility clsUtil = new CommonUtility();

        override protected void OnLoad(EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
                //FillDataTableForDropDownList();
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            //			InitControl();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion

        public int ProgramID {
            get {
                return c_ProgramID;
            }
            set {
                c_ProgramID = value;
            }
        }

        public dataDef DataSource {
            get {
                return dTblProgram;
            }
            set {
                dTblProgram = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public new void BindForm() {
            if (!IsPostBack)
                FillDataTableForDropDownList();

            DataRow programRow = dTblProgram.Rows[0];
            //First section -- Account Name and Billing Address
            if (programRow.RowState == DataRowState.Added)
                lblProgramID.Text = "New Program";
            else
                lblProgramID.Text = programRow[dataDef.FLD_PKID].ToString();

            txtProgramName.Text = programRow[dataDef.FLD_PROGRAM_NAME].ToString();
            txtDescription.Text = programRow[dataDef.FLD_DESCRIPTION].ToString();

            if (!programRow.IsNull(dataDef.FLD_PROGRAM_TYPE_ID)) {
                ListItem lstItem = ddlProgramType.Items.FindByValue(programRow[dataDef.FLD_PROGRAM_TYPE_ID].ToString());
                if (lstItem != null) {
                    ddlProgramType.ClearSelection();
                    lstItem.Selected = true;
                }
            }
        }

        public bool ValidateForm() {
            if (!IsValid()) {
                return false;
            }

            //if everything have been ok
            return true;
        }

        public bool UpdateDataSource() {
            if (dTblProgram.Rows.Count > 0) {
                DataRow programRow = dTblProgram.Rows[0];
                //-------------------------------------------------
                //		Program Information
                //-------------------------------------------------

                int programID = Convert.ToInt32(programRow[ProgramTable.FLD_PKID]);

                clsUtil.UpdateRow(programRow, ProgramTable.FLD_PROGRAM_NAME, txtProgramName.Text);
                clsUtil.UpdateRow(programRow, ProgramTable.FLD_DESCRIPTION, txtDescription.Text);
                clsUtil.UpdateRow(programRow, ProgramTable.FLD_PROGRAM_TYPE_ID, ddlProgramType.SelectedValue);
            }

            return true;
        }

        protected override void OnDataBinding(EventArgs e) {
            BindForm();
        }

        private void FillDataTableForDropDownList() {
            try {
                CommonUtility clsUtil = new CommonUtility();
                clsUtil.SetProgramTypeDropDownList(ddlProgramType, true);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}