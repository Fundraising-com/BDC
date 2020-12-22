using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using QSPForm.Common.DataDef;
using System.Data;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for BaseWebFormStep.
    /// </summary>
    public class BaseWebFormStep : BaseWebUserControl {
        public event System.EventHandler GoToPreviousStep;
        public event System.EventHandler GoToNextStep;
        private System.Web.UI.WebControls.ImageButton imgBtnNext;
        private System.Web.UI.WebControls.ImageButton imgBtnBack;
        QSPForm.Business.AppItem previousAppItem;
        QSPForm.Business.AppItem nextAppItem;
        QSPForm.Business.AppItem stepItem;

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            if (this.imgBtnNext != null)
                this.imgBtnNext.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnNext_Click);
            if (this.imgBtnBack != null)
                this.imgBtnBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnBack_Click);

        }
        #endregion

        private void imgBtnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            OnGoToNextStep(EventArgs.Empty);
        }

        protected virtual void OnGoToNextStep(System.EventArgs e) {
            if (GoToNextStep != null) {
                // Invokes the delegates. 
                GoToNextStep(this, e);
            }
        }

        private void imgBtnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            OnGoToPreviousStep(EventArgs.Empty);
        }

        protected virtual void OnGoToPreviousStep(System.EventArgs e) {
            if (GoToPreviousStep != null) {
                // Invokes the delegates. 
                GoToPreviousStep(this, e);
            }
        }

        public QSPForm.Business.AppItem PreviousAppItem {
            get {
                return previousAppItem;
            }
            set {
                previousAppItem = value;
            }
        }

        public QSPForm.Business.AppItem StepItem {
            get {
                return stepItem;
            }
            set {
                stepItem = value;
            }
        }

        public QSPForm.Business.AppItem NextAppItem {
            get {
                return nextAppItem;
            }
            set {
                nextAppItem = value;
            }
        }

        public ImageButton ImageButtonBack {
            get {
                return imgBtnBack;
            }
            set {
                imgBtnBack = value;
            }
        }

        public ImageButton ImageButtonNext {
            get {
                return imgBtnNext;
            }
            set {
                imgBtnNext = value;
            }
        }

        public virtual bool Update() {
            return true;
        }

        public virtual void BindForm() {
        }

        public virtual bool ValidateForm() {
            return true;
        }

        public bool IsFirstLoad {
            get {
                if (ViewState["IsFirstLoad"] != null) {
                    return Convert.ToBoolean(ViewState["IsFirstLoad"]);
                }
                else {
                    return true;
                }
            }
            set {
                ViewState["IsFirstLoad"] = value;
            }
        }
    }
}