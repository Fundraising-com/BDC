using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using QSPForm.Common.DataDef;
using System.Data;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for BaseBusinessFormStep.
    /// </summary>
    public class BaseBusinessFormStep : BaseWebFormStep {
        QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();
        
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

        }
        #endregion

        public new BaseBusinessForm Page {
            get {
                return (BaseBusinessForm)base.Page;
            }
        }
    }
}