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
using QSP.OrderExpress.Web.Code;


namespace QSP.OrderExpress.Web.V2.UserControls
{
    public partial class DocumentInformationEdit : System.Web.UI.UserControl
    {
        private DocumentData Document;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }

            this.SetValuesToForm();
        }

        private void SetValuesToForm()
        {
            if (this.Document == null)
            {
                // try to get from viewstate
                //this.Organization = (OrganizationData)ViewState["OrganizationData"];
            }

            if (this.Document != null)
            {
                this.lblQspId.Text = this.Document.Id.ToString();
                this.lblType.Text = this.Document.Type.Name;
                this.lblName.Text = this.Document.Name;
                this.cbApproved.Checked = this.Document.IsApproved;


                if (this.Document.ExemptionNumber != null)
                {
                    this.tbExeptionNumber.Text = this.Document.ExemptionNumber.Trim();
                }

                if (this.Document.ExemptionStartDate.HasValue)
                {
                    this.tbExeptionStartDate.Text = this.Document.ExemptionStartDate.Value.ToShortDateString();
                }
                else
                {
                    this.tbExeptionStartDate.Text = "";
                }

                if (this.Document.ExemptionEndDate.HasValue)
                {
                    this.tbExeptionEndDate.Text = this.Document.ExemptionEndDate.Value.ToShortDateString();
                }
                else
                {
                    this.tbExeptionEndDate.Text = "";
                }
            }
        }
        private void GetValuesFromForm()
        {
            this.Document.IsApproved = this.cbApproved.Checked;
            this.Document.ExemptionNumber = this.tbExeptionNumber.Text.Trim();

            DateTime startDate;
            bool startDateExists = DateTime.TryParse(this.tbExeptionStartDate.Text, out startDate);
            if (startDateExists)
            {
                this.Document.ExemptionStartDate = startDate;
            }
            else
            {
                this.Document.ExemptionStartDate = null;
            }

            DateTime endDate;
            bool endDateExists = DateTime.TryParse(this.tbExeptionEndDate.Text, out endDate);
            if (endDateExists)
            {
                this.Document.ExemptionEndDate = endDate;
            }
            else
            {
                this.Document.ExemptionEndDate = null;
            }
        }

        public void SetValueForDocument(DocumentData document)
        {
            this.Document = document;
            //ViewState["OrganizationData"] = organization;
        }
        public DocumentData GetValueFromDocument()
        {
            if (this.Document == null)
            {
                this.Document = new DocumentData();
            }

            this.GetValuesFromForm();
            //ViewState["OrganizationData"] = this.Organization;

            return this.Document;
        }
    }
}