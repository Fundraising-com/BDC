using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for BaseWebUserControl.
    /// </summary>
    public class BaseWebFormControl : BaseWebUserControl {
        public BaseWebFormControl() {

        }
        public new BaseWebForm Page {
            get {
                return (BaseWebForm)base.Page;
            }
        }

        //Virtual prperty and method to override in child class
        public virtual void BindForm() {
            try {
                LoadData();
                //Prepare the DataSource of DropDownList when 
                //databind the grid 

            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected virtual void LoadData() {
        }

        protected virtual bool Update() {
            return true;
        }

        protected virtual bool Delete() {
            return true;
        }

        protected override void OnLoad(EventArgs e) {
            //			if (!IsPostBack)
            //				BindForm();
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
            try {
                this.Page.MenuChange += new System.ComponentModel.CancelEventHandler(Page_MenuChange);
                this.Page.GoToStep += new System.ComponentModel.CancelEventHandler(Page_GoToStep);
            }
            catch {
            }
        }
        #endregion

        private void Page_MenuChange(object sender, System.ComponentModel.CancelEventArgs e) {
            OnMenuChange(e);
        }

        protected virtual void OnMenuChange(System.ComponentModel.CancelEventArgs e) {
            try {
                if (!e.Cancel) {
                    bool blnValid = false;

                    if (Page.IsFormChange) {
                        Page.Validate();
                        if (Page.IsValid)
                            blnValid = Update();
                        e.Cancel = !blnValid;
                    }
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
                e.Cancel = true;
            }
        }

        private void Page_GoToStep(object sender, System.ComponentModel.CancelEventArgs e) {
            OnGoToStep(e);
        }

        protected virtual void OnGoToStep(System.ComponentModel.CancelEventArgs e) {

            try {
                if (!e.Cancel) {
                    bool blnValid = false;

                    if (Page.IsFormChange) {
                        Page.Validate();
                        if (Page.IsValid)
                            blnValid = Update();
                        e.Cancel = !blnValid;
                    }
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
                e.Cancel = true;
            }
        }

        protected int GetDropDownListSelectedIndex(string ValueToFound, DataView DV, string ColumnNameToSearch) {
            int iIndex = 0;
            int iCount = 0;
            foreach (DataRowView drwv in DV) {
                if (drwv[ColumnNameToSearch].ToString() == ValueToFound) {
                    iIndex = iCount;
                    break;
                }
                iCount++;
            }
            //because of the null value
            return iIndex;
        }
    }
}