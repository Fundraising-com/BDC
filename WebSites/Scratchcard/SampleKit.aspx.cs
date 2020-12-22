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
using System.Text.RegularExpressions;

namespace GA.BDC.WEB.ScratchcardWeb
{
	/// <summary>
	/// Summary description for SampleKit1.
	/// </summary>
	public class SampleKit : ScratchcardWebBase
	{
		protected GA.BDC.Core.Web.UI.MasterPages.Content Content1;
		protected GA.BDC.Core.Web.UI.MasterPages.MasterPage MasterPage1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			ScratchcardOmnitureTracking.SetPageNameAndCategory("Public", "Free Info Kit");
			ScratchcardOmnitureTracking.AddEVar_Custom (5, "Free Kit Form");
			ScratchcardOmnitureTracking.AddEvent_Custom (4);
			
			Partner partner = Partner.Create(Session);
			
			if (partner.PartnerID == 0)
			{
				partner.PartnerID = 500;
				partner.Save(Session);
			}
			
			
			
			/*  
			// Do AB Splitting only for web browsers, non robots clients
			if (Regex.Match(Request.UserAgent, @"^mozilla/\d\.\d", RegexOptions.IgnoreCase).Success)
			{
						
				if (partner.PartnerID == 500)
					Response.Redirect("http://absplitting.efundraising.com/spliting.aspx?page_code=sckit");
				else if (partner.PartnerID == 98)
					Response.Redirect("http://absplitting.efundraising.com/spliting.aspx?page_code=brlkit");
				else
					Response.Redirect("http://absplitting.efundraising.com/spliting.aspx?page_code=sckit");
			}
			*/
			
			
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
