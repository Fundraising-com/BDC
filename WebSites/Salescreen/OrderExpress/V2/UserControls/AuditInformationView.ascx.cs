using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QSPForm.Business;
using QSP.OrderExpress.Business;
using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;

namespace QSP.OrderExpress.Web.V2.UserControls
{
    public partial class AuditInformationView : System.Web.UI.UserControl
    {
        private AuditInformationData AuditInformation;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetValueForAuditInformation(AuditInformationData auditInformation)
        {
            this.AuditInformation = auditInformation;
        }
        public void SetValuesToForm()
        {
            if (this.AuditInformation != null)
            {
                this.lblCreateDate.Text = string.Format("{0} {1}", this.AuditInformation.CreateDate.ToShortDateString(), this.AuditInformation.CreateDate.ToShortTimeString());
                this.lblCreatedBy.Text = string.Format("{0}, {1}", this.AuditInformation.CreateUserLastName, this.AuditInformation.CreateUserFirstName);

                if (this.AuditInformation.UpdateDate.HasValue)
                {
                    this.lblUpdateDate.Text = string.Format("{0} {1}", this.AuditInformation.UpdateDate.Value.ToShortDateString(), this.AuditInformation.UpdateDate.Value.ToShortTimeString());
                }
                if (this.AuditInformation.UpdateUserFirstName != null && this.AuditInformation.UpdateUserLastName != null)
                {
                    this.lblUpdatedBy.Text = string.Format("{0}, {1}", this.AuditInformation.UpdateUserLastName, this.AuditInformation.UpdateUserFirstName);
                }
            }
        }
    }
}