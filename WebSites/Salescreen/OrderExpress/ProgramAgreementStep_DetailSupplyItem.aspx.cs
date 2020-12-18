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
    public partial class ProgramAgreementStep_DetailSupplyItem : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "Use the Tab key to enter Supplies in Unit Quantities.";
            this.Header.SectionText = "Add New Program Agreement";
            this.Header.PageText = "STEP 4 - Supply";
        }
    } 
}