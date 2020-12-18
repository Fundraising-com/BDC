using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for ReadOnlyStatusChangedEventArgs
    /// </summary>
    public class ReadOnlyStatusChangedEventArgs : EventArgs {
        private bool isReadOnly = false;

        public ReadOnlyStatusChangedEventArgs() : base() { }

        public ReadOnlyStatusChangedEventArgs(bool isReadOnly) {
            this.isReadOnly = isReadOnly;
        }

        public bool IsReadOnly {
            get {
                return isReadOnly;
            }
            set {
                isReadOnly = value;
            }
        }
    }
}