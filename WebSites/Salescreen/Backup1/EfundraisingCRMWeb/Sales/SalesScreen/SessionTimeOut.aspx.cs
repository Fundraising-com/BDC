using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EfundraisingCRM
{
    public partial class SessionTimeOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            efundraising.Diagnostics.Logger.LogError("Sales Screen: SESSION TIME OUT");
              
        }
    }
}
