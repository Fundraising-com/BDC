using System;
using System.Collections.Generic;
using System.Drawing;
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
    public partial class DocumentInformationView : System.Web.UI.UserControl
    {
        private DocumentData Document;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetValueForDocument(DocumentData document)
        {
            this.Document = document;
        }
        public void SetValuesToForm()
        {
            if (this.Document != null)
            {
                this.lblQspId.Text = this.Document.Id.ToString();
                this.lblType.Text = this.Document.Type.Name;
                this.lblName.Text = this.Document.Name;
                this.cbApproved.Checked = this.Document.IsApproved;
                this.lblApprovedBy.Text = this.Document.DisplayApprovedByName;
                this.lblApprovedAt.Text = this.Document.DisplayApprovedDate;
                this.lblExeptionNumber.Text = this.Document.ExemptionNumber;
                this.lblExeptionStartDate.Text = this.Document.DisplayExemptionStartDate;
                this.lblExeptionEndDate.Text = this.Document.DisplayExemptionEndDate;

                if (this.Document.IsApproved)
                {
                    this.trApprovedBy.Visible = true;
                    this.trApprovedAt.Visible = true;
                }
                else
                {
                    this.trApprovedBy.Visible = false;
                    this.trApprovedAt.Visible = false;
                }

                if (this.Document.ExemptionNumber != null)
                {
                    this.trExeptionNumber.Visible = true;
                }
                else
                {
                    this.trExeptionNumber.Visible = false;
                }

                if (this.Document.ExemptionStartDate.HasValue || this.Document.ExemptionEndDate.HasValue)
                {
                    this.trExeptionDates.Visible = true;
                }
                else
                {
                    this.trExeptionDates.Visible = false;
                }
            }
        }
    }
}