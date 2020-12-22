using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for BaseWebUserControl.
    /// </summary>
    public class BaseWebUserControl : System.Web.UI.UserControl {
        public BaseWebUserControl() {
        }

        public new BasePage Page {
            get {
                return (BasePage)base.Page;
            }
        }

        public virtual bool IsValid() {
            Boolean blnValid = true;
            //Control ctl;
            foreach (Control ctl in this.Controls) {
                if (ctl is BaseValidator) {
                    BaseValidator val;
                    val = (BaseValidator)ctl;
                    if (val.Enabled) {
                        val.Validate();
                        if (!val.IsValid) {
                            //Page.SetErrorText(val.ErrorMessage)
                            blnValid = false;
                            break;
                        }
                    }
                }
                else {
                    if (ctl.HasControls()) {
                        blnValid = IsValid(ctl.Controls);
                        if (!blnValid)
                            break;
                    }
                }
            }

            return blnValid;
        }

        public virtual bool IsValid(ControlCollection ctls) {
            Boolean blnValid = true;
            //Control ctl;
            foreach (Control ctl in ctls) {
                if (ctl is BaseValidator) {
                    BaseValidator val;
                    val = (BaseValidator)ctl;
                    if (val.Enabled) {
                        val.Validate();
                        if (!val.IsValid) {
                            //Page.SetErrorText(val.ErrorMessage)
                            blnValid = false;
                            break;
                        }
                    }
                }
                else {
                    if (ctl.HasControls()) {
                        blnValid = IsValid(ctl.Controls);
                        if (!blnValid)
                            break;
                    }
                }
            }

            return blnValid;
        }

        protected int getSelectedIndex(DataView dv, String sValue) {
            int iIndex = -1;
            try {
                if (sValue != "") {
                    int iCount = 0;
                    foreach (DataRowView dvRow in dv) {
                        if (sValue == dvRow[0].ToString()) {
                            iIndex = iCount;
                            break;
                        }
                        iCount++;
                    }
                }

            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
            return iIndex;
        }

        protected int getSelectedIndex(DataTable dt, String sValue) {
            int iIndex = -1;
            try {
                if (sValue != "") {
                    int iCount = 0;
                    foreach (DataRow row in dt.Rows) {
                        if (row[0].ToString() == sValue) {
                            iIndex = iCount;
                            break;
                        }
                        iCount++;
                    }
                }

            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
            return iIndex;
        }

        public PageMode Mode {
            get {
                if (this.ViewState["Mode"] != null) {
                    return (PageMode)this.ViewState["Mode"];
                }
                else {
                    this.ViewState["Mode"] = PageMode.ReadOnly;
                    return PageMode.ReadOnly;
                }
            }
            set { this.ViewState["Mode"] = value; }
        }
    }
}