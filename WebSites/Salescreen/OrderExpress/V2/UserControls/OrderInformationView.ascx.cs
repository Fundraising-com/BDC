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
    public partial class OrderInformationView : System.Web.UI.UserControl
    {
        private OrderData Order;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetValueForOrder(OrderData order)
        {
            this.Order = order;
        }
        public void SetValuesToForm()
        {
            if (this.Order != null)
            {
                this.lblQspId.Text = this.Order.Id.ToString();
                this.lblEdsId.Text = this.Order.EdsId;

                ColorConverter colConvert = new ColorConverter();
                this.lblStatusBox.BackColor = (Color)colConvert.ConvertFromString(this.Order.StatusColor);
                this.lblStatus.Text = this.Order.StatusName;
                this.lblFSM.Text = string.Format("{0} - {1}, {2}", this.Order.FsmId, this.Order.FsmLastName, this.Order.FsmFirstName);

                this.lblOrderDate.Text = this.Order.OrderDate.ToLongDateString() + " " + this.Order.OrderDate.ToLongTimeString();
                if (this.Order.FormId.HasValue)
                {
                    this.lblOrderForm.Text = this.Order.FormId.Value + " - " + this.Order.FormName;
                }
                this.lblOrderType.Text = this.Order.OrderTypeName;
                if (this.Order.DeliveryMethodId.HasValue)
                {
                    this.lblDeliveryMethod.Text = this.Order.DeliveryMethodName;
                }

                this.lblRequestedDeliveryDate.Text = this.Order.ReqestedDeliveryDateText;
                if (this.Order.RequestedLeadTimeBusinessDays > 0)
                {
                    this.lblRequestedLeadTime.Text = this.Order.RequestedLeadTimeBusinessDays + " business days";
                    this.trRequestedLeadTime.Visible = true;
                }
                else
                {
                    this.lblRequestedLeadTime.Text = "";
                    this.trRequestedLeadTime.Visible = false;
                }
                if (this.Order.DeliveryWarehouseId.HasValue)
                {
                    this.lblRequestedDeliveryWarehouse.Text = this.Order.DeliveryWarehouseName;
                    this.trRequestedDeliveryWarehouse.Visible = true;
                }
                else
                {
                    this.lblRequestedDeliveryWarehouse.Text = "";
                    this.trRequestedDeliveryWarehouse.Visible = false;
                }

                if (this.Order.ShippingDate.HasValue)
                {
                    this.lblShippingDate.Text = this.Order.ShippingDate.Value.ToLongDateString();
                    this.trShippingDate.Visible = true;
                }
                else
                {
                    this.lblShippingDate.Text = "";
                    this.trShippingDate.Visible = false;
                }

                if (this.Order.CustomerPONumber != null)
                {
                    this.lblCustomerPO.Text = this.Order.CustomerPONumber.Trim();
                    this.trCustomerPO.Visible = true;
                }
                else
                {
                    this.lblCustomerPO.Text = "";
                    this.trCustomerPO.Visible = false;
                }

                if (this.Order.Comments != null)
                {
                    this.lblComments.Text = this.Order.Comments.Trim();
                    this.trComments.Visible = true;
                }
                else
                {
                    this.lblComments.Text = "";
                    this.trComments.Visible = false;
                }
            }
        }
    }
}