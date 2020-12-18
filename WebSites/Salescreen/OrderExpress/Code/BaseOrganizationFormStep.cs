using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using QSPForm.Common.DataDef;
using System.Data;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for BaseOrganizationFormStep.
    /// </summary>
    public class BaseOrganizationFormStep : BaseWebFormStep {

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

        public new BaseOrganizationForm Page {
            get {
                return (BaseOrganizationForm)base.Page;
            }
        }

        public void SetDefaultInformation() {
            QSPForm.Business.OrganizationSystem orgSys = new QSPForm.Business.OrganizationSystem();
            orgSys.SetDefaultInformation(this.Page.DataSource, this.Page.UserID);
        }
    }
}