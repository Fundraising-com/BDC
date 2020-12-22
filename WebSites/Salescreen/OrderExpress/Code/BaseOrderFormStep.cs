using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using QSPForm.Common.DataDef;
using System.Data;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for BaseOrderFormStep.
    /// </summary>
    public class BaseOrderFormStep : BaseWebFormStep {
        QSPForm.Business.OrderSystem orderSys = new QSPForm.Business.OrderSystem();
        private OrderData dtsOrder;

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

        public new BaseOrderForm Page {
            get {
                return (BaseOrderForm)base.Page;
            }
        }

        public OrderData DataSource {
            get {
                return dtsOrder;
            }
            set {
                dtsOrder = value;
                SetInnerControlDataSource();
            }
        }

        protected virtual void SetInnerControlDataSource() {

        }

        public void SetDefaultBillingInformation() {
            orderSys.SetDefaultBillingPostalAddress(this.Page.DataSource, this.Page.UserID);
            orderSys.SetDefaultBillingPhoneNumber(this.Page.DataSource, this.Page.UserID);
            orderSys.SetDefaultBillingEmailAddress(this.Page.DataSource, this.Page.UserID);
        }

        public void SetDefaultShippingInformation() {
            orderSys.SetDefaultShippingPostalAddress(this.Page.DataSource, this.Page.UserID);
            orderSys.SetDefaultShippingPhoneNumber(this.Page.DataSource, this.Page.UserID);
            orderSys.SetDefaultShippingEmailAddress(this.Page.DataSource, this.Page.UserID);
        }

        public void SetDefaultFormProduct() {
            //			if (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_FM)
            //				orderSys.SetDefaultFormProduct(this.Page.DataSource, this.Page.UserID, false);
            //			else
            orderSys.SetDefaultFormProduct(this.Page.DataSource, this.Page.UserID, this.Page.QCAPOrderID, true);
        }

        public void SetDefaultFormSupply() {
            //			if (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_FM)
            //				orderSys.SetDefaultFormSupply(this.Page.DataSource, this.Page.UserID, false);
            //			else
            orderSys.SetDefaultFormSupply(this.Page.DataSource, this.Page.UserID, true);
        }

        public void SetDefaultShippingSupplyInformation() {
            orderSys.SetDefaultShippingSupplyPostalAddress(this.Page.DataSource, this.Page.UserID);
            orderSys.SetDefaultShippingSupplyPhoneNumber(this.Page.DataSource, this.Page.UserID);
            orderSys.SetDefaultShippingSupplyEmailAddress(this.Page.DataSource, this.Page.UserID);
        }

        public void SetFMShippingSupplyInformation() {
            orderSys.SetFMShippingSupplyPostalAddress(this.Page.DataSource, this.Page.UserID);
        }

        public void CalculateTax() {
            orderSys.CalculateTax(this.Page.DataSource, this.Page.UserID);
        }
    }
}