using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataRef = QSPForm.Common.DataDef.OrderData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class OrderStep_OrderInformation : BaseOrderFormStep {
        //protected OrderData dtsOrder;
        protected AccountData dtsAccount;
        private CommonUtility util = new CommonUtility();
        protected System.Web.UI.WebControls.DropDownList ddlOrderForm;

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            InitControl();
            this.PreRender += new EventHandler(OrderStep_PreRender);
            base.OnInit(e);
        }

        void OrderStep_PreRender(object sender, EventArgs e) {
            ////override the one from basepage
            //string instruction;
            //instruction = "<p>Complete the required Order Information fields below and if necessary, enter a brief Comment, pertaining to this order, in the Comment field.<p>"; 
            //instruction += "<p><b><font color=\"blue\"> Note: To avoid delaying this order, only use an appropriate delivery-related comment in this field.</font></b></p>";

            //this.Page.LabelInstruction.Text = instruction;
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnBack_Click);
            this.imgBtnNext.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnNext_Click);
            this.imgBtnSkip.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSkip_Click);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
                if (this.Page.DataOperation == QSPForm.Common.DataOperation.UPDATE) {
                    imgBtnBack.Visible = false;
                }
            }
            //			if (this.DataSource != null)
            //				HeaderDetail.DataSource = this.DataSource;

            trQCAPOrderIntimation.Visible = this.Page.QCAPOrderID != 0;
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }

        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.OrderForm_Step4;
            this.StepItem = QSPForm.Business.AppItem.OrderForm_Step5;
            this.NextAppItem = QSPForm.Business.AppItem.OrderForm_Step7;
            this.ImageButtonBack = imgBtnBack;
            this.ImageButtonNext = imgBtnNext;
        }

        protected override void SetInnerControlDataSource() {
            HeaderDetail.DataSource = this.DataSource;
            HeaderDetail.InitializeControls();
        }

        public override bool ValidateForm() {
            return HeaderDetail.ValidateForm();
        }

        public override void BindForm() {
            if (IsFirstLoad) {
            }
            //Pass the datasource to the control
            HeaderDetail.DataSource = this.DataSource;
            HeaderDetail.BindForm();
        }

        public override bool Update() {
            //Bill To address
            HeaderDetail.UpdateDataSource();
            return true;
        }

        private void imgBtnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e) {

        }

        private void imgBtnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            HeaderDetail.ResetStatus();
        }

        private void imgBtnSkip_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            this.Page.GoNextByAppItem(QSPForm.Business.AppItem.OrderForm_Step7);
        }

        protected override void OnGoToNextStep(System.EventArgs e) {
            try {
                //if (HeaderDetail.DeliveryDate < DateTime.Today)
                //{

                //}
                //else
                base.OnGoToNextStep(e);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}