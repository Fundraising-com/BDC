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
    public partial class ProgramAgreementInformationView : System.Web.UI.UserControl
    {
        private ProgramAgreementData ProgramAgreement;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetValueForProgramAgreement(ProgramAgreementData programAgreement)
        {
            this.ProgramAgreement = programAgreement;
        }
        public void SetValuesToForm()
        {
            if (this.ProgramAgreement != null)
            {
                this.lblQspId.Text = this.ProgramAgreement.Id.ToString();
                this.lblEdsId.Text = this.ProgramAgreement.EdsId;

                ColorConverter colConvert = new ColorConverter();
                this.lblStatusBox.BackColor = (Color)colConvert.ConvertFromString(this.ProgramAgreement.StatusColor);
                this.lblStatus.Text = this.ProgramAgreement.StatusName;
            }

        }
    }
}