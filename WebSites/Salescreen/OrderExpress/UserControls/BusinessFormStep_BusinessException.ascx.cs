using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.FormData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class BusinessFormStep_BusinessException : BaseBusinessFormStep {
        private CommonUtility util = new CommonUtility();
        protected BusinessExceptionTable dTblBizException;
        private dataRef dtsForm;
        protected System.Web.UI.WebControls.Label Label4;
        protected System.Web.UI.WebControls.Label Label2;

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
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.BusinessForm_Step3;
            this.StepItem = QSPForm.Business.AppItem.BusinessForm_Step4;
            this.NextAppItem = QSPForm.Business.AppItem.BusinessForm_Step5;
            this.ImageButtonBack = imgBtnBack;
            this.ImageButtonNext = imgBtnNext;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {

        }

        public FormData DataSource {
            get {
                return dtsForm;
            }
            set {
                dtsForm = value;
                dTblBizException = dtsForm.BusinessException;
                BusinessExceptionFormStep.DataSource = dTblBizException;
            }
        }

        public override void BindForm() {
            if (IsFirstLoad) {

            }
            BusinessExceptionFormStep.DataSource = dTblBizException;
            BusinessExceptionFormStep.BindForm();
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            //BindForm();
        }

        public override bool Update() {
            bool IsSucess = false;

            BusinessExceptionFormStep.DataSource = dTblBizException;
            IsSucess = BusinessExceptionFormStep.UpdateDataSource();

            return IsSucess;
        }

        public override bool ValidateForm() {
            return BusinessExceptionFormStep.IsValid();
        }
    }
}