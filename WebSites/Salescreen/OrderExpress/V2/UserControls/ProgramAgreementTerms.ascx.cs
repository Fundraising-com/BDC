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
    public partial class ProgramAgreementTerms : System.Web.UI.UserControl
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
                this.lblStartDate.Text = this.ProgramAgreement.StartDate.ToShortDateString();
                this.lblEndDate.Text = this.ProgramAgreement.EndDate.ToShortDateString();

                if (this.ProgramAgreement.HolidayStartDate.HasValue)
                {
                    this.lblHolidayStartDate.Text = this.ProgramAgreement.HolidayStartDate.Value.ToShortDateString();
                }
                if (this.ProgramAgreement.HolidayEndDate.HasValue)
                {
                    this.lblHolidayEndDate.Text = this.ProgramAgreement.HolidayEndDate.Value.ToShortDateString();
                }

                this.lblGoal.Text = this.ProgramAgreement.GoalEstimatedGross.ToString("C");
                this.lblParticipants.Text = this.ProgramAgreement.Enrollment.ToString();
                this.lblAccountProfit.Text = (this.ProgramAgreement.AccountProfitRate * 100).ToString("0.00") + "%";
            }
        }
    }
}