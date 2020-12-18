//===========================================================================
// This file was generated as part of an ASP.NET 2.0 Web project conversion.
// This code file 'App_Code\Migrated\Stub_LogoDetail_ascx_cs.cs' was created and contains an abstract class 
// used as a base class for the class 'Migrated_LogoDetail' in file 'LogoDetail.ascx.cs'.
// This allows the the base class to be referenced by all code files in your project.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.LogoData;

namespace QSP.OrderExpress.Web.Code {
    abstract public class BaseLogoDetail : BaseWebFormControl {
        public const string LOGO_ID = "Logo_id";

        abstract public int LogoID {
            get;
            set;
        }
    }
}