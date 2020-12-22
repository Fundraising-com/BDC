//===========================================================================
// This file was generated as part of an ASP.NET 2.0 Web project conversion.
// This code file 'App_Code\Migrated\Stub_ToolBar_ascx_cs.cs' was created and contains an abstract class 
// used as a base class for the class 'Migrated_ToolBar' in file 'ToolBar.ascx.cs'.
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

    abstract public class BaseToolBar : BaseWebUserControl {
        public const int DISPLAY_READ = 1;
        public const int DISPLAY_EDIT = 2;
        public const int DISPLAY_CLOSE = 3;

        abstract public ImageButton DeleteButton {
            get;
        }

        abstract public ImageButton EditButton {
            get;
        }

        abstract public HyperLink CancelButton {
            get;
        }

        abstract public int DisplayMode {
            get;
            set;
        }

        abstract public bool ShowDeleteButton {
            set;
        }
    }
}