using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using QSPForm.Common.DataDef;
using System.Data;

namespace QSP.OrderExpress.Web.Code {
    [Serializable]
    public enum ProgramAgreementStep {
        PAInformation,
        DetailSupplyItem,
        Validation
    }

    /// <summary>
    /// Summary description for BaseProgramAgreementFormStep.
    /// </summary>
    public abstract class BaseProgramAgreementFormStep : BaseWebFormStep {
        QSPForm.Business.ProgramAgreementSystem prgSys = new QSPForm.Business.ProgramAgreementSystem();
        private ProgramAgreementData dtsProgramAgreement;

        public event EventHandler Saving;

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

        public new BaseProgramAgreementForm Page {
            get {
                return (BaseProgramAgreementForm)base.Page;
            }
        }

        public abstract string InstructionText {
            get;
        }

        public abstract string SectionText {
            get;
        }

        public abstract string PageText {
            get;
        }

        public abstract string IconImage {
            get;
        }

        public abstract bool IconImageVisibility {
            get;
        }

        public ProgramAgreementData DataSource {
            get 
            {
                return dtsProgramAgreement;
            }
            set 
            {
                dtsProgramAgreement = value;
                SetInnerControlDataSource();
            }
        }

        protected virtual void OnSaving(EventArgs args) 
        {
            if (Saving != null) 
            {
                Saving(this, args);
            }
        }

        protected virtual void SetInnerControlDataSource() {

        }

        public void SetDefaultBillingInformation() {
            //orderSys.SetDefaultBillingPostalAddress(this.Page.DataSource, this.Page.UserID);
            //orderSys.SetDefaultBillingPhoneNumber(this.Page.DataSource, this.Page.UserID);
            //orderSys.SetDefaultBillingEmailAddress(this.Page.DataSource, this.Page.UserID);
        }

        public void SetDefaultShippingInformation() {
            //orderSys.SetDefaultShippingPostalAddress(this.Page.DataSource, this.Page.UserID);
            //orderSys.SetDefaultShippingPhoneNumber(this.Page.DataSource, this.Page.UserID);
            //orderSys.SetDefaultShippingEmailAddress(this.Page.DataSource, this.Page.UserID);
        }

        public void SetDefaultFormProduct() {
            //			if (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_FM)
            //				orderSys.SetDefaultFormProduct(this.Page.DataSource, this.Page.UserID, false);
            //			else
            //orderSys.SetDefaultFormProduct(this.Page.DataSource, this.Page.UserID, true);
        }

        public void SetDefaultFormSupply() {
            //			if (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_FM)
            //				orderSys.SetDefaultFormSupply(this.Page.DataSource, this.Page.UserID, false);
            //			else
            //prgSys.SetDefaultFormSupply(this.Page.DataSource, this.Page.UserID, true);
        }

        public void SetDefaultShippingSupplyInformation() {
            //orderSys.SetDefaultShippingSupplyPostalAddress(this.Page.DataSource, this.Page.UserID);
            //orderSys.SetDefaultShippingSupplyPhoneNumber(this.Page.DataSource, this.Page.UserID);
            //orderSys.SetDefaultShippingSupplyEmailAddress(this.Page.DataSource, this.Page.UserID);
        }

        public void SetFMShippingSupplyInformation() {
            //orderSys.SetFMShippingSupplyPostalAddress(this.Page.DataSource, this.Page.UserID);
        }

        public void CalculateTax() {
            //orderSys.CalculateTax(this.Page.DataSource, this.Page.UserID);
        }
    }
}