using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.OrderData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class OrderStep_DetailSupplyItem : BaseOrderFormStep {
        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here	
            if (!IsPostBack) {
            }
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
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion


        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.OrderForm_Step5;
            this.StepItem = QSPForm.Business.AppItem.OrderForm_Step6;
            this.NextAppItem = QSPForm.Business.AppItem.OrderForm_Step7;
            this.ImageButtonBack = imgBtnBack;
            this.ImageButtonNext = imgBtnNext;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            SetJScriptForNextButton();
        }

        protected override void SetInnerControlDataSource() {
            SupplyForm.DataSource = this.DataSource;
        }

        public override void BindForm() {
            DataRow shipRow = this.DataSource.ShipmentGroup.Rows[0];
            if (IsFirstLoad) {
                if (this.Page.DataOperation == QSPForm.Common.DataOperation.INSERT) {
                    base.SetDefaultFormSupply();
                    shipRow[ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE] = shipRow[ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE];
                    shipRow[ShipmentGroupTable.FLD_SUPPLY_DELIVERY_NLT] = shipRow[ShipmentGroupTable.FLD_DELIVERY_NLT];
                    shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO] = 2; //Default to same address
                }
                else {
                    if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIP_SUPPLY_TO)) {
                        shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO] = 2; //Default to same address
                    }
                }
            }

            SupplyForm.BindForm();
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }

        public override bool ValidateForm() {
            bool IsSuccess = false;

            SupplyForm.DataSource = this.DataSource;
            IsSuccess = SupplyForm.ValidateForm();

            return IsSuccess;
        }

        public override bool Update() {
            bool IsSuccess = false;

            SupplyForm.DataSource = this.DataSource;
            IsSuccess = SupplyForm.UpdateDataSource();

            return IsSuccess;
        }

        private void SetJScriptForNextButton() {
            imgBtnNext.Attributes.Add("onclick", " return WarnEmptySupply();");

            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("<script language=javascript>\n");
            strBuild.Append("<!--			\n");
            strBuild.Append("	function WarnEmptySupply() {	\n");
            strBuild.Append("		var ctlTotSupplyQty = document.getElementById('" + SupplyForm.LabelTotalQuantityClientID + "');   \n");
            strBuild.Append("	    //alert('ctlTotSupplyQty=' + ctlTotSupplyQty); \n");
            strBuild.Append("		var totSupplyQty  = ctlTotSupplyQty.innerHTML; \n");
            strBuild.Append("	    //alert('totSupplyQty=' + totSupplyQty); \n");
            strBuild.Append("	    if (totSupplyQty < 1) \n");
            strBuild.Append("	        return confirm('There are No Supplies entered. Are you sure you want to go to the next step ?'); \n");
            strBuild.Append("}\n");
            strBuild.Append("//-->\n");
            strBuild.Append("</script>");
            this.Page.RegisterClientScriptBlock("WarnEmptySupply", strBuild.ToString());
        }
    }
}