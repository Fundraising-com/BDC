using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;

namespace EFundraisingCRMWeb
{
    public class EFundraisingCRMSalesBasePage : EFundraisingCRMWebBasePage {
    
    

    public virtual void Search(string querySearch) {
			Redirect("../../IT/Partners/Default.aspx?" + Components.Server.UrlParam.UrlKeySearchQuery
				+ "=" + System.Web.HttpUtility.UrlEncode(querySearch));
		}

		public virtual void Create(string redirection) {
			Redirect(redirection);
		}
}}
