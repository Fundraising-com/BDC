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
    public partial class BusinessFormStep_BusinessTask : BaseBusinessFormStep {
        private CommonUtility util = new CommonUtility();
        protected BusinessTaskTable dTblBizTask;
        private dataRef dtsForm;

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
            this.imgBtnConfirm.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnConfirm_Click);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion

        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.BusinessForm_Step4;
            this.StepItem = QSPForm.Business.AppItem.BusinessForm_Step5;
            this.NextAppItem = QSPForm.Business.AppItem.BusinessForm_Step6;
            this.ImageButtonBack = imgBtnBack;
            //this.ImageButtonNext = imgBtnNext;			
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public FormData DataSource {
            get {
                return dtsForm;
            }
            set {
                dtsForm = value;
                dTblBizTask = dtsForm.BusinessTask;
                BusinessTaskFormStep.DataSource = dTblBizTask;
            }
        }

        public override void BindForm() {
            if (IsFirstLoad) {
            }
            BusinessTaskFormStep.DataSource = dTblBizTask;
            BusinessTaskFormStep.BindForm();
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            //BindForm();
        }

        public override bool Update() {
            bool IsSucess = false;

            BusinessTaskFormStep.DataSource = dTblBizTask;
            IsSucess = BusinessTaskFormStep.UpdateDataSource();

            return IsSucess;
        }

        public override bool ValidateForm() {
            bool IsValid = false;
            IsValid = BusinessTaskFormStep.IsValid();
            if (!IsValid)
                this.Page.MaintainScrollPositionOnPostBack = false;
            return IsValid;
        }

        private void imgBtnConfirm_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            bool IsValid = false;
            if (ValidateForm()) {
                IsValid = Update();
                if (IsValid) {
                    IsValid = this.Page.SaveDataSource();
                    if (IsValid) {
                        DataRow frmRow = this.Page.DataSource.Form.Rows[0];
                        string sFormID = frmRow[FormTable.FLD_PKID].ToString();
                        //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.BusinessForm_Step6, BusinessFormStep_Confirmation.FORM_ID, sFormID);
                        //Response.Redirect(url);
                        string url = "~/BusinessFormStep_Confirmation.aspx?&" + BusinessFormStep_Confirmation.FORM_ID + "=" + sFormID;
                        Response.Redirect(url);
                    }
                }
            }
        }
    }
}