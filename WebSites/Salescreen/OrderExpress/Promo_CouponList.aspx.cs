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
    public partial class Promo_CouponList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
            SetSearchCriteria();
        }

        private void SetHeader() {
            this.Header.InstructionText = "To locate a Coupon Agreement, use the Search features and click on Refresh button.";
            this.Header.SectionText = "Promotion";
            this.Header.PageText = "Coupon Agreement";
            this.Header.IconImageVisiblilty = false;
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlPromo_CouponList.SearchCriteria.Items.Clear();
            this.ctrlPromo_CouponList.SearchCriteria.Items.Add(new ListItem("Promo Text Code", "5"));
            this.ctrlPromo_CouponList.SearchCriteria.Items.Add(new ListItem("FSM ID", "2"));
            this.ctrlPromo_CouponList.SearchCriteria.Items.Add(new ListItem("FSM Name", "3"));
            this.ctrlPromo_CouponList.SearchCriteria.Items.Add(new ListItem("Vendor Name", "6", true));
            this.ctrlPromo_CouponList.SearchCriteria.Items.Add(new ListItem("Coupon Text", "1"));
            this.ctrlPromo_CouponList.SearchCriteria.Items.Add(new ListItem("Logo Code", "4"));
        }
    } 
}