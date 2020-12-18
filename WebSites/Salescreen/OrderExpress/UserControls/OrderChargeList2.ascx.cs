using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QSPForm.Business;
using QSPForm.Common.DataDef;
using QSP.Business.Fulfillment.View;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    public partial class OrderChargeList2 : System.Web.UI.UserControl {
        private int orderId;
        private List<QSP.Business.Fulfillment.View.OrderChargeList> orderChargeList = null;

        /// <summary>
        /// Gets or sets id of the order for which to load the charges from the database.
        /// </summary>
        public int OrderId {
            get {
                return this.orderId;
            }
            set {
                this.orderId = value;
            }
        }

        /// <summary>
        /// The data that will be displayed in the list
        /// </summary>
        public List<QSP.Business.Fulfillment.View.OrderChargeList> ChargeList {
            get {
                return this.orderChargeList;
            }
            set {
                this.orderChargeList = value;
            }
        }

        /// <summary>
        /// Returns the number of charges for the order
        /// </summary>
        public int TotalChargeCount {
            get {
                int totalChargeCount = 0;

                totalChargeCount = this.orderChargeList.Count;

                return totalChargeCount;
            }
        }

        /// <summary>
        /// Returns the total amount of the charges for the order
        /// </summary>
        public decimal TotalChargeAmount {
            get {
                decimal totalChargeAmount = 0;

                foreach (QSP.Business.Fulfillment.View.OrderChargeList charge in this.orderChargeList) {
                    if (charge.Amount != null) {
                        totalChargeAmount += Convert.ToDecimal(charge.Amount);
                    }
                }

                return totalChargeAmount;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
        }

        protected void dtgOrderChargeList_ItemCreated(object sender, DataGridItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Footer) {
                e.Item.Cells[3].Text = this.TotalChargeAmount.ToString("C");
            }
        }

        /// <summary>
        /// Loads the order charge data from the database
        /// </summary>
        public void LoadData() {
            this.orderChargeList = QSP.Business.Fulfillment.View.OrderChargeList.GetOrderChargeListListByOrder(orderId);
        }

        /// <summary>
        /// Loads the contents of the OrderChargeList property to the list
        /// </summary>
        public void BindList() {
            this.dtgOrderChargeList.DataSource = this.orderChargeList;
            this.dtgOrderChargeList.DataBind();
        }
    } 
}