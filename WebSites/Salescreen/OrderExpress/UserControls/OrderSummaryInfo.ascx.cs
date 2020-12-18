using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataRef = QSPForm.Common.DataDef.OrderData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderSubList.
    /// </summary>
    public partial class OrderSummaryInfo : BaseWebUserControl {
        protected QSP.WebControl.DataGridControl.SortedDataGrid dtgOrder;
        protected System.Web.UI.WebControls.Label lblTotal;
        protected dataRef dtsOrder = new dataRef();
        private CommonUtility clsUtil = new CommonUtility();
        protected System.Web.UI.WebControls.Label Label13;
        protected System.Web.UI.WebControls.Label Label35;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            InitControl();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }

        private void InitControl() {


        }

        #endregion

        public dataRef DataSource {
            get {
                return dtsOrder;
            }
            set {
                dtsOrder = value;
            }
        }

        public void BindForm() {
            //Order Summary
            QSPForm.Business.OrderSystem orderSys = new QSPForm.Business.OrderSystem();
            orderSys.CalculateOrder(dtsOrder);
            DataRow ordHeader = dtsOrder.OrderHeader.Rows[0];

            lblSubTotal.Text = "0";
            lblTaxRate.Text = "0";
            lblShippingCharges.Text = "0";
            lblTaxRate.Text = "0";
            lblTaxRate.Text = "0";

            if (ordHeader[OrderHeaderTable.FLD_TOTAL_AMOUNT] != DBNull.Value) {
                lblSubTotal.Text = Convert.ToDecimal(ordHeader[OrderHeaderTable.FLD_TOTAL_AMOUNT]).ToString("C");
            }

            if (ordHeader[OrderHeaderTable.FLD_TAX_RATE] != DBNull.Value) {
                lblTaxRate.Text = Convert.ToDecimal(ordHeader[OrderHeaderTable.FLD_TAX_RATE]).ToString("P");
            }
            if (ordHeader[OrderHeaderTable.FLD_TOTAL_TAX_AMOUNT] != DBNull.Value) {
                lblTaxAmount.Text = Convert.ToDecimal(ordHeader[OrderHeaderTable.FLD_TOTAL_TAX_AMOUNT]).ToString("C");
            }
            if (ordHeader[OrderHeaderTable.FLD_TOTAL_SHIP_FEES] != DBNull.Value) {
                lblShippingCharges.Text = Convert.ToDecimal(ordHeader[OrderHeaderTable.FLD_TOTAL_SHIP_FEES]).ToString("C");
            }
            if (ordHeader[OrderHeaderTable.FLD_GRAND_TOTAL] != DBNull.Value) {
                lblGrandTotal.Text = Convert.ToDecimal(ordHeader[OrderHeaderTable.FLD_GRAND_TOTAL]).ToString("C");
            }
        }
    }
}