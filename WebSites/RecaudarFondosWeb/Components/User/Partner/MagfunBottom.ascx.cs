namespace efundraising.RecaudarFondosWeb.Components.User.Partner
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using GA.BDC.Core.efundraisingCore;

	/// <summary>
	///		Summary description for MagfunBottom.
	/// </summary>
	public class MagfunBottom : System.Web.UI.UserControl
	{
		protected System.Web.UI.HtmlControls.HtmlAnchor MagFun;
		protected string addPromotion = "&pr_id=";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(this.Page.Request.QueryString["partner"] != null && this.Page.Request.QueryString["partner"]!= string.Empty)
			{
				Partner partner = Partner.GetPartnerInfoByFolder(Request.QueryString["partner"]);
				
				
				if(partner != null)
				{
 		  			
					string token1 = MagFun.HRef.Substring(0,MagFun.HRef.LastIndexOf("?gid=") +5);
					string token2 = MagFun.HRef.Substring(MagFun.HRef.IndexOf("&",MagFun.HRef.LastIndexOf("?gid=")));
					if(Request.QueryString["promotion_id"] != null && Request.QueryString["promotion_id"] != "")
					{
						token2 = addPromotion + Request.QueryString["promotion_id"].ToString();
					}
					MagFun.HRef = token1 + partner.GUID.ToString() + token2;
				}

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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
