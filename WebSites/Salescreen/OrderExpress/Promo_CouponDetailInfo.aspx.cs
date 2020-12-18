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
    public partial class Promo_CouponDetailInfo : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "Eneter direction here";
            this.Header.SectionText = "Promotion";
            this.Header.PageText = "Coupon Agreement Detail";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}