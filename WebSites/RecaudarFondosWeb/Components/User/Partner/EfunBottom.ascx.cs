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
	///		Summary description for EfunBottom.
	/// </summary>
	public class EfunBottom : System.Web.UI.UserControl
	{
		protected System.Web.UI.HtmlControls.HtmlAnchor EfrLink;
		protected  string addPartner = "?partner=";
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(this.Page.Request.QueryString["partner"] != null && this.Page.Request.QueryString["partner"]!= string.Empty)
			{
				
				Partner partner = Partner.GetPartnerInfoByFolder(Request.QueryString["partner"].ToString());
				if(partner != null)
				{
					EfrLink.HRef = EfrLink.HRef + addPartner + Request.QueryString["partner"].ToString();
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
