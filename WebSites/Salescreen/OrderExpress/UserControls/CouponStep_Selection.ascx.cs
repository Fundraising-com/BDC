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
    public partial class CouponStep_Selection : BaseWebFormControl {
        protected void Page_Load(object sender, EventArgs e) {
            this.ctrlVendorSelector.ShowButton = false;
            this.ctrlVendorSelector.CloseAfterSelect = false;
        }

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);

            this.ctrlVendorSelector.SearchAppItem = QSPForm.Business.AppItem.VendorSelector;
            this.ctrlVendorSelector.OnSelectedVendor += new SelectedVendorHandler(ctrlVendorSelector_OnSelectedVendor);
        }

        void ctrlVendorSelector_OnSelectedVendor(object sender, SelectedVendorArgs e) {
            CommonUtility c = new CommonUtility();
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.CouponStep_2);
            string url = "~/CouponStep_Detail.aspx?";
            Response.Redirect(url + "&v=" + e.VendorID.ToString());
        }
    }

    #region stuff
    //public delegate void SelectedVendorHandler(object sender, SelectedVendorArgs e);
    //public event SelectedVendorHandler OnSelectedVendor;
    //public class SelectedVendorArgs : EventArgs
    //{
    //    private int vendorID;
    //    public SelectedVendorArgs(int VendorID)
    //    {
    //        this.vendorID = VendorID;
    //    }
    //    public int VendorID
    //    {
    //        get
    //        {
    //            return vendorID;
    //        }
    //    }
    //}
    #endregion    
}