//===========================================================================
// This file was generated as part of an ASP.NET 2.0 Web project conversion.
// This code file 'App_Code\Migrated\Stub_MenuBar_ascx_cs.cs' was created and contains an abstract class 
// used as a base class for the class 'Migrated_MenuBar' in file 'MenuBar.ascx.cs'.
// This allows the the base class to be referenced by all code files in your project.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================

namespace QSP.OrderExpress.Web.Code {
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Text;
    using QSPForm.Common.DataDef;

    abstract public class BaseMenuBar : BaseWebUserControl {
        public event System.ComponentModel.CancelEventHandler MenuChange;

        abstract public QSPForm.Business.AppItem MenuItem {
            get;
            set;
        }

        protected virtual void OnMenuChange(System.ComponentModel.CancelEventArgs e) {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            System.ComponentModel.CancelEventHandler handler = MenuChange;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}