namespace GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using efundraising.ScratchcardWeb;
    using GA.BDC.Core.Database.Scratchcard.DataAccess;

	/// <summary>
	///		Summary description for DidYouKnow.
	/// </summary>
	public class DidYouKnow : System.Web.UI.UserControl
	{
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected System.Web.UI.WebControls.Label lblStory;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// GLOBALIZATION
			GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage gbp =
				(GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage)this.Page;
			gbp.Globalize(PagePanelControl1, this);
			
			
			// get a random success story from the database
			Story didYouKnowStory = new Story(2, groupNametoID());
			lblStory.Text = didYouKnowStory.StoryString;
		}
		
		private int groupNametoID()
		{
			int groupID = 0;
			
			// association the group name with an ID to get a story
			// related to the correct group type from the database			
			if (Request.QueryString["type"] == "softball")
			{
				groupID = 1;
			}
			else if (Request.QueryString["type"] == "hockey")
			{
				groupID = 2;
			}
			else if (Request.QueryString["type"] == "bowling")
			{
				groupID = 3;
			}
			else if (Request.QueryString["type"] == "trackandfield")
			{
				groupID = 4;
			}
			else if (Request.QueryString["type"] == "basket")
			{
				groupID = 5;
			}
			else if (Request.QueryString["type"] == "gym")
			{
				groupID = 6;
			}
			else if (Request.QueryString["type"] == "cheer")
			{
				groupID = 7;
			}
			else if (Request.QueryString["type"] == "foot")
			{
				groupID = 8;
			}
			else if (Request.QueryString["type"] == "swimming")
			{
				groupID = 9;
			}
			else if (Request.QueryString["type"] == "volley")
			{
				groupID = 10;
			}
			else if (Request.QueryString["type"] == "wrestling")
			{
				groupID = 11;
			}
			else if (Request.QueryString["type"] == "lacrosse")
			{
				groupID = 12;
			}
			else if (Request.QueryString["type"] == "soccer")
			{
				groupID = 13;
			}
			else if (Request.QueryString["type"] == "baseball")
			{
				groupID = 14;
			}
			else if (Request.QueryString["type"] == "music")
			{
				groupID = 15;
			}
			else if (Request.QueryString["type"] == "flag")
			{
				groupID = 16;
			}
			else if (Request.QueryString["type"] == "nature")
			{
				groupID = 17;
			}
			else if (Request.QueryString["type"] == "university")
			{
				groupID = 18;
			}
			else if (Request.QueryString["type"] == "church_nonc")
			{
				groupID = 20;
			}
			else if (Request.QueryString["type"] == "bands")
			{
				groupID = 21;
			}
			else if (Request.QueryString["type"] == "scouts")
			{
				groupID = 23;
			}
			else if (Request.QueryString["type"] == "highschool")
			{
				groupID = 24;
			}
			else if (Request.QueryString["type"] == "elementary")
			{
				groupID = 26;
			}
			else if (Request.QueryString["type"] == "canada")
			{
				groupID = 27;
			}
			else if (Request.QueryString["type"] == "winter")
			{
				groupID = 28;
			}
			else if (Request.QueryString["type"] == "chrismas")
			{
				groupID = 29;
			}
			// some group types don't have any stories so we just use a random one from any type
			else
			{
				groupID = 0;
			}
			
			
			return groupID;
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
