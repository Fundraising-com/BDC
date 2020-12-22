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
using GA.BDC.Core.efundraisingCore;
using GA.BDC.Core.efundraisingCore.DataAccess;

namespace GA.BDC.WEB.ScratchcardWeb
{
	/// <summary>
	/// Summary description for PageNotFound.
	/// </summary>
	public class PageNotFound : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			eFundEnv env = new eFundEnv();
			Partner partner = null;				
			
			// get the url that caused the 404 error
			string badurl = Request.Url.AbsoluteUri;
			badurl = badurl.Substring(badurl.IndexOf("404;") + 4);
			Promotion promotion = new Promotion();
							
			
			int start = 0;
			if (badurl.IndexOf(":80") > 0)
			{
				start = badurl.IndexOf(Request.Url.Host) + Request.Url.Host.Length + 4;
			}
			else
			{
				start = badurl.IndexOf(Request.Url.Host) + Request.Url.Host.Length + 1;
			}
			string subFolder = badurl.Substring(start);
			subFolder = HttpUtility.UrlDecode(subFolder);
			subFolder = subFolder.Replace(" ", "+");
			
			if (subFolder.IndexOf("/") > 0)
			{
				subFolder = subFolder.Substring(0, subFolder.IndexOf("/"));
			}
			
			
			EFundDatabase dbi = new EFundDatabase();
			promotion = Promotion.GetPromotion(subFolder);
			
			
			if (promotion != null)
			{
				partner = dbi.GetPartnerInfoByID(promotion.PartnerId);
				partner.Save(Session);
				env.PartnerInfo = partner;
				env.PromotionID = promotion.PromotionId;
				env.Save();
				
				// Get promotion destination
				Destination dest = Destination.GetDestination(promotion.DestinationId);

				Response.Status = "301 Moved Permanently";
				Response.AddHeader("Location","Default.aspx");

				// check if we need to redirect to a specific url attached to the active promotion
				if (dest != null && dest.Url != null)
				{
					if (dest.Url.IndexOf("?") > 0)
					{
						Response.Redirect(dest.Url + "&promotion_id=" + promotion.PromotionId);
					}
					else
					{
						Response.Redirect(dest.Url + "?promotion_id=" + promotion.PromotionId);
					}
				}
				else
				{
					Response.Redirect("~/Default.aspx");
				}
			}
			else
			{
				Response.Redirect("~/Default.aspx");		
			}

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
