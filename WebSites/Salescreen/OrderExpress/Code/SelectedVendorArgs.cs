using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for SelectedVendorArgs
    /// </summary>
    /// 
    public delegate void SelectedVendorHandler(object sender, SelectedVendorArgs e);
    public class SelectedVendorArgs : EventArgs {
        private int vendorID;
        public SelectedVendorArgs(int VendorID) {
            this.vendorID = VendorID;
        }
        public int VendorID {
            get {
                return vendorID;
            }
        }
    }

    //public class SelectedVendorHandler 
    //{
    //    private object sender;
    //    private SelectedVendorArgs e;

    //    public SelectedVendorHandler(object sender, SelectedVendorArgs e)
    //    {
    //        this.sender = sender;
    //        this.e = e;
    //    }
    //} 
}