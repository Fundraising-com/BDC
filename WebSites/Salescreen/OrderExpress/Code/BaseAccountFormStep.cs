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
    public class BaseAccountFormStep : BaseWebFormStep {

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

        public new BaseAccountForm Page {
            get {
                return (BaseAccountForm)base.Page;
            }
        }

        public void SetDefaultInformation() {
            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
            accSys.SetDefaultInformation(this.Page.DataSource, this.Page.UserID);
        }

        //		public void SetCampaignDefaultInformation()
        //		{
        //			QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();	
        //			accSys.SetCampaignDefaultInformation(this.Page.DataSource, this.Page.UserID);
        //		}

        public void SetOrganizationDefaultInformation(string MDRPID) {
            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
            accSys.SetOrganizationDefaultInformation(this.Page.DataSource, this.Page.UserID, this.Page.FMID, MDRPID);
        }

        public void CopyInformationToEntity(int CopyToEntityType) {
            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
            accSys.CopyInformationToEntity(this.Page.DataSource, this.Page.UserID, CopyToEntityType);
        }
    }
}