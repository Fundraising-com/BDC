using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    public partial class CreditApplicationDetail : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "It is agreed that your organization is purchasing chocolate and/or food products from QSP for the purpose of reselling them under your own name and for your own benefit. Your organization further agrees to pay for these items with your own funds, either by check or credit card. The individual responsible for payment must sign this application and be 18 years or older.  Credit is pre-approved for those organizations/responsible parties who have maintained a good credit standing with QSP within the past 2 years.  Invoice payment terms are net 30 days. A $25 fee will be charged for payments received with insufficient funds";
            this.Header.IconImage = "~/images/icon/icon_credit_app.gif";
            this.Header.SectionText = "Account:";
            this.Header.PageText = "Credit Application";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}