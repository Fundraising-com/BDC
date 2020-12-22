using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for BaseWebFormInfoControl.
    /// </summary>
    public class BaseWebFormInfoControl : BaseWebUserControl {
        private bool isLoadDataSource = false;

        public BaseWebFormInfoControl() {
        }

        public new BaseWebForm Page {
            get {
                return (BaseWebForm)base.Page;
            }
        }

        //Virtual prperty and method to override in child class
        protected virtual void BindForm() {
            try {
                if (IsLoadDataSource) {
                    LoadData();
                }
                BindForm();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected virtual void LoadData() {
        }

        protected override void OnLoad(EventArgs e) {
            if (!IsPostBack) {

            }
            base.OnLoad(e);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion

        public bool IsLoadDataSource {
            get {
                return isLoadDataSource;
            }
            set {
                isLoadDataSource = value;
            }
        }
    }
}