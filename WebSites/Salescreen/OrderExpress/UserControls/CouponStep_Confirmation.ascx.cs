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
using QSP.OrderExpress.Web;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    public partial class CouponStep_Confirmation : BaseWebFormControl {
        protected void Page_Load(object sender, EventArgs e) {
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e) {//agreement list
            CommonUtility cu = new CommonUtility();
            // Response.Redirect(cu.GetPageUrl(QSPForm.Business.AppItem.Promo_CouponList));
            Response.Redirect("~/Promo_CouponList.aspx");
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e) {//add new
            CommonUtility cu = new CommonUtility();
            //Response.Redirect(cu.GetPageUrl(QSPForm.Business.AppItem.CouponStep_1));
            Response.Redirect("~/CouponStep_Selection.aspx");
        }
    }
}