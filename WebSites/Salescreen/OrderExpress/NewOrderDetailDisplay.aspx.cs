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
using QSPForm.Common.DataDef;
using QSPForm;
using dataDef = QSPForm.Common.DataDef.OrderData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    public partial class NewOrderDetailDisplay : BaseWebForm {
        private int c_OrderID;
        private string mode;

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);
            OrderDetailControl.ReadOnlyStatusChanged += new EventHandler<ReadOnlyStatusChangedEventArgs>(OrderDetailControl_ReadOnlyStatusChanged);
        }

        protected void Page_Load(object sender, EventArgs e) {
            SetFormParameter();
            OrderDetailControl.OrderID = c_OrderID;
        }

        protected void SetFormParameter() {
            if (Request["OrderID"] != null) {
                c_OrderID = Convert.ToInt32(Request["OrderID"].ToString());
            }
            else {
                // Sample RNKORDER
                c_OrderID = 0;
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "Please verify Account and Order information below and click on Edit button to access edit fields and modify data.";
            this.Header.IconImage = "~/images/icon/icon_order.gif";
            this.Header.SectionText = "Order:";
            this.Header.PageText = "Order Detail";
        }

        private void SetHeaderEdit() {
            this.Header.InstructionText = "Edit Sponsor Information, Postal Address, Phone Numbers Email Addresses and/or Products.  'Bill To' Information can easily be copied over to 'Ship To' Information.";
            this.Header.IconImage = "~/images/icon/icon_order.gif";
            this.Header.SectionText = "Order:";
            this.Header.PageText = "Order Detail";
            this.Master.ValSummaryVisibility = false;
        }

        private void OrderDetailControl_ReadOnlyStatusChanged(object sender, ReadOnlyStatusChangedEventArgs e) {
            if (e.IsReadOnly) {
                SetHeader();
            }
            else {
                SetHeaderEdit();
            }
        }
    } 
}