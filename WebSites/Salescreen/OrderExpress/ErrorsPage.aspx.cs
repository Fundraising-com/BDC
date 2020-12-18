//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_ErrorsPage_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'ErrorsPage.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Mail;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    /// <summary>
    /// Summary description for ErrorsPage.
    /// </summary>
    public partial class ErrorsPage : BaseErrorsPage {
        protected System.Web.UI.WebControls.Label lblModeLogin;
        //protected MenuBar QSPFormMenuBar;

        protected void Page_Load(object sender, System.EventArgs e) {
            this.AppItem = QSPForm.Business.AppItem.ErrorPage;

            if (Request[PARAM_MESSAGE] != null)
            {
                
                lblMessage.Text = Request[PARAM_MESSAGE].ToString();
            }
            else
                lblMessage.Text = "Please try again ";

            if (Request[PARAM_DESCRIPTION] != null)
                lblDescription.Text = Request[PARAM_DESCRIPTION].ToString();
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion

        protected override void InitControl() {
            this.AppItem = QSPForm.Business.AppItem.ErrorPage;
            base.LabelInstruction = lblInstruction;
            base.LabelMessage = lblMessage;
            base.InitControl();
        }
    }
}