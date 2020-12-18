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
    public partial class ProgramInformationView : System.Web.UI.UserControl
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
                this.lblCatalogType.Text = this.ProgramAgreement.IsPriced ? "Priced" : "Unpriced";


                var catalogs = (from c in this.ProgramAgreement.Catalogs
                                select new {
                                    c.Code, 
                                    c.Name
                                }).Distinct();

                this.lblCatalogSelection.Text = "";
                foreach (var catalogData in catalogs)
                {
                    this.lblCatalogSelection.Text += string.Format("{1} ({0}) <br />", catalogData.Code, catalogData.Name);
                }
            }
        }
    }
}