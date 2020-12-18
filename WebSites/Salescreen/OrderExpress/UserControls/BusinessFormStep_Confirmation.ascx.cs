using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.FormData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AccountStep_Continue.
    /// </summary>
    public partial class BusinessFormStep_Confirmation : BaseWebUserControl {
        protected System.Web.UI.WebControls.Label lblLabelDeliveryDate;
        protected System.Web.UI.WebControls.Label lblDeliveryDate;
        protected System.Web.UI.WebControls.Label lblDayLeadTime;
        public const string FORM_ID = "FormID";
        private int c_FormID = 0;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
                SetFormParameter();
                BindForm();
            }
            else {
                c_FormID = Convert.ToInt32(ViewState[FORM_ID]);
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
            this.imgBtnFormList.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnFormList_Click);

        }
        #endregion

        protected void SetFormParameter() {
            if (Request[FORM_ID] != null) {
                c_FormID = Convert.ToInt32(Request[FORM_ID].ToString());
            }
            else {
                c_FormID = 0;
            }
            ViewState[FORM_ID] = c_FormID;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public void BindForm() {
            QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
            FormTable dTblForm = formSys.SelectOne(c_FormID);

            DataRow row = dTblForm.Rows[0];
            string FormName = row[FormTable.FLD_FORM_NAME].ToString();
            string FormCode = row[FormTable.FLD_FORM_CODE].ToString();

            string message = "The Form " + FormCode + " - " + FormName + ", has been saved sucessfully.";
            lblMessageConfirmation.Text = message;
        }

        private void imgBtnFormList_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //Response.Redirect(this.Page.GetPageToGo(QSPForm.Business.AppItem.Form_List));
            Response.Redirect("~/FormList.aspx");
        }
    }
}